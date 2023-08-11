using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

using Handelabra.Sentinels.Engine.Controller;
using Handelabra.Sentinels.Engine.Model;
using System.Runtime.Remoting.Messaging;

namespace Jp.SOTMUtilities
{
    public static class ExtensionMethods
    {
        // Shows a critical message with the version of the utility library in it.
        public static IEnumerator ShowVersionMessage(this CardController co)
        {
            var assembly = typeof(CardAlignmentHelperImpl).Assembly;
            return co.GameController.SendMessageAction(
                $"JpSOTMUtilities version {assembly.GetName().Version}",
                Priority.Critical,
                co.GetCardSource()
            );
        }

        // Helper for checking card properties.
        public static CardAlignmentHelperImpl Is(this Card c)
        {
            if (c == null)
                throw new NullReferenceException("Is() called on null card");

            return new CardAlignmentHelperImpl(c);
        }

        // Helper for checking card properties.
        public static CardAlignmentHelperWithController Is(this Card c, CardController controller)
        {
            if (c == null)
                throw new NullReferenceException("Is() called on null card");

            return new CardAlignmentHelperWithController(c, controller);
        }

        // Helper for checking card properties.
        public static CardAlignmentHelperImpl Is(this CardController c)
        {
            if (c == null)
                throw new NullReferenceException("Is() called on null cardcontroller");

            return new CardAlignmentHelperImpl(c.Card);
        }

        // Helper for checking card properties.
        public static CardAlignmentHelperWithController Is(this CardController c, CardController controller)
        {
            if (c == null)
                throw new NullReferenceException("Is() called on null cardcontroller");

            return new CardAlignmentHelperWithController(c.Card, controller);
        }

        // Helper for checking TurnTaker properties.
        public static CardAlignmentHelperImpl Is(this TurnTaker t)
        {
            if (t == null)
                throw new NullReferenceException("Is() called on null turntaker");

            return new CardAlignmentHelperImpl(t);
        }

        // Helper for checking TurnTaker properties.
        public static CardAlignmentHelperWithController Is(this TurnTaker t, CardController controller)
        {
            if (t == null)
                throw new NullReferenceException("Is() called on null turntaker");

            return new CardAlignmentHelperWithController(t, controller);
        }

        // Helper for checking TurnTaker properties.
        public static CardAlignmentHelperImpl Is(this TurnTakerController t)
        {
            if (t == null)
                throw new NullReferenceException("Is() called on null turntakercontroller");

            return new CardAlignmentHelperImpl(t.TurnTaker);
        }

        // Helper for checking TurnTaker properties.
        public static CardAlignmentHelperWithController Is(this TurnTakerController t, CardController controller)
        {
            if (t == null)
                throw new NullReferenceException("Is() called on null turntakercontroller");

            return new CardAlignmentHelperWithController(t.TurnTaker, controller);
        }

        // Helper for checking card/turntaker properties.
        //
        // This will work with Card or TurnTaker damage sources.
        public static CardAlignmentHelperImpl Is(this DamageSource source)
        {
            if (source.IsCard)
            {
                return source.Card.Is();
            }
            else
            {
                return source.TurnTaker.Is();
            }
        }

        // Helper for checking card/turntaker properties.
        //
        // This will work with Card or TurnTaker damage sources.
        public static CardAlignmentHelperWithController Is(this DamageSource source, CardController controller)
        {
            if (source.IsCard)
            {
                return source.Card.Is(controller);
            }
            else
            {
                return source.TurnTaker.Is(controller);
            }
        }

        // Helper to have every target in play that meets criteria 'damageDealerCriteria' deal 'amount'
        // damage of type 'damageType' to one target meeting criteria 'targetCriteria'.
        //
        // 'decisionMaker' is the player making decisions about ordering and which target will be damaged,
        // or null if all players make the decision jointly.
        //
        // New potential damageDealers entering play after this coroutine has started (for example, targets
        // that entered play in response to damage dealt because of this coroutine) will also deal damage.
        //
        // 'damageDealerCriteria' has an implicit "in play", but NOT an implicit "is a target". 'targetCriteria'
        // has an implicit "in play" and "is target". Note that non-target cards can deal damage (for example, 
        // Primordial Plant Life in Insula Primalis).
        public static IEnumerator SelectTargetsToDealDamageToTarget(
            this CardController c,
            HeroTurnTakerController decisionMaker,
            Func<Card, bool> damageDealerCriteria,
            Func<Card, bool> targetCriteria,
            int amount,
            DamageType damageType
        )
        {
            return c.SelectTargetsToDealDamageToTarget(
                decisionMaker,
                damageDealerCriteria,
                damageDealer => c.GameController.SelectTargetsAndDealDamage(
                    decisionMaker,
                    new DamageSource(c.GameController, damageDealer),
                    amount,
                    damageType,
                    1,
                    optional: false,
                    requiredTargets: 0,
                    additionalCriteria: card => targetCriteria(card),
                    cardSource: c.GetCardSource()
                )
            );
        }

        // Helper to have every target in play that meets criteria 'damageDealerCriteria' do something
        // in a player-determined order, typically do damage to another target (hence the name).
        // 'damageFunc' will be run for each target selected as they're picked.
        //
        // 'decisionMaker' is the player making decisions about ordering and which target will be damaged,
        // or null if all players make the decision jointly.
        //
        // New potential damageDealers entering play after this coroutine has started (for example, targets
        // that entered play in response to damage dealt because of this coroutine) will also run damageFunc.
        //
        // 'damageDealerCriteria' has an implicit "is in play" criteria but no others.
        public static IEnumerator SelectTargetsToDealDamageToTarget(
            this CardController c,
            HeroTurnTakerController decisionMaker,
            Func<Card, bool> damageDealerCriteria,
            Func<Card, IEnumerator> damageFunc
        )
        {
            var alreadySelected = new List<Card>();
            bool autoDecided = false;
            IEnumerator e;

            while (true)
            {
                var selectable = c.GameController.FindCardsWhere(card => card.IsInPlay && damageDealerCriteria(card), true, c.GetCardSource()).Except(alreadySelected, new TargetEqualityComparer());
                if (selectable.Count() <= 0) { break; }

                Card selectedDamageDealer;

                if (autoDecided)
                {
                    selectedDamageDealer = selectable.First();
                }
                else
                {
                    var storedResults = new List<SelectCardDecision>();
                    e = c.GameController.SelectCardAndStoreResults(
                        decisionMaker,
                        SelectionType.CardToDealDamage,
                        new LinqCardCriteria(card => selectable.Contains(card)),
                        storedResults,
                        optional: false,
                        allowAutoDecide: true,
                        cardSource: c.GetCardSource()
                    );
                    if (c.UseUnityCoroutines)
                    {
                        yield return c.GameController.StartCoroutine(e);
                    }
                    else
                    {
                        c.GameController.ExhaustCoroutine(e);
                    }

                    selectedDamageDealer = storedResults.FirstOrDefault()?.SelectedCard;
                    if (selectedDamageDealer == null) { break; }

                    autoDecided = storedResults.FirstOrDefault().AutoDecided;
                }

                alreadySelected.Add(selectedDamageDealer);

                e = damageFunc(selectedDamageDealer);

                if (c.UseUnityCoroutines)
                {
                    yield return c.GameController.StartCoroutine(e);
                }
                else
                {
                    c.GameController.ExhaustCoroutine(e);
                }
            }
        }

        // Returns true if:
        // - The currently active turn taker is `controller` AND,
        // - Either,
        //      - The currently-active phase is `phase` OR
        //      - Phase 'phase' hasn't happened yet this turn.
        //
        // This is useful because effects that grant additional plays or power
        // uses that occur after the relevant phase or on a different turn grant
        // immediate plays/power uses. The base game mechanism for determining this
        // does not take into account unusual turn orders.
        //
        // Note: This function will not work correctly for unusual turn orders in
        // ephemeral turns, e.g. La Comodora with Concordant Helm out hit by Completionist
        // Guise's immediate-turn incap. This is because of limitations of the engine
        // I haven't come up with a good workaround for. The behaviour in that case will
        // be the same as the base game mechanism - if the phase you're in would 'normally'
        // come before the requested phase, we're prior to it, even if we've already performed
        // the requested phase.
        public static bool IsTurnTakersTurnPriorToOrDuringPhase(this TurnTakerController controller, Phase phase)
        {
            if (controller.GameController.ActiveTurnTakerController != controller)
            {
                return false;
            }

            if (controller.GameController.ActiveTurnPhase.Phase == phase)
            {
                return true;
            }

            if (controller.GameController.Game.ActiveTurnPhase.IsEphemeral)
            {
                // Ephemeral phase changes are not recorded in the journal.
                // There's not currently a good way I know of to track what
                // phases have happened in an ephemeral turn. The base game
                // gets it wrong too. Just implementing the simple and mostly
                // correct version here until there's a better solution.
                //
                // If the active turn phase is before the tested phase, then
                // we haven't had the tested phase yet (unless we've got a weird
                // turn order...)
                return controller.GameController.ActiveTurnPhase.Phase < phase;
            }
            else
            {
                // If we've changed phases out of a power phase belonging to controller this turn, then
                // we're after the power phase.
                return controller.GameController.Game.Journal.PhaseChangeEntriesThisTurn().Where(
                    je => je.FromPhase?.Phase == phase && je.FromPhase?.TurnTaker == controller.TurnTaker
                ).Count() == 0;
            }
        }

        public static bool IsResponsible(this Card c, DestroyCardAction dca)
        {
            return c == (dca.ResponsibleCard ?? dca.CardSource.Card);
        }
    }
}
