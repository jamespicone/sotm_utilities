using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Handelabra.Sentinels.Engine.Controller;
using Handelabra.Sentinels.Engine.Model;

namespace Jp.SOTMUtilities
{
    public static class ExtensionMethods
    {
        // Helper for checking card properties.
        // You can pass a CardController to use when checking card properties such as villain-ness
        // or card keywords.
        public static CardAlignmentHelper Is(this Card c, CardController controller = null)
        {
            return new CardAlignmentHelper(c, controller);
        }

        // Helper for checking card properties.
        // You can pass a CardController to use when checking card properties such as villain-ness
        // or card keywords.
        public static CardAlignmentHelper Is(this CardController c, CardController controller = null)
        {
            return new CardAlignmentHelper(c.Card, controller);
        }

        // Helper for checking TurnTaker properties.
        // You can pass a CardController to use when checking villain-ness
        public static CardAlignmentHelper Is(this TurnTaker t, CardController controller = null)
        {
            return new CardAlignmentHelper(t, controller);
        }

        // Helper for checking TurnTaker properties.
        // You can pass a CardController to use when checking villain-ness
        public static CardAlignmentHelper Is(this TurnTakerController t, CardController controller = null)
        {
            return new CardAlignmentHelper(t.TurnTaker, controller);
        }

        // Helper for checking card/turntaker properties.
        // You can pass a CardController to use when checking card properties such as villain-ness
        // or card keywords.
        //
        // This will work with Card or TurnTaker damage sources.
        public static CardAlignmentHelper Is(this DamageSource source, CardController controller = null)
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

        public static IEnumerator ShowVersionMessage(this CardController co)
        {
            return co.GameController.SendMessageAction(
                "Version 1",
                Priority.Critical,
                co.GetCardSource()
            );
        }

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
                var selectable = c.GameController.FindCardsWhere(card => card.IsInPlay && damageDealerCriteria(card), true, c.GetCardSource()).Except(alreadySelected);
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
    }
}
