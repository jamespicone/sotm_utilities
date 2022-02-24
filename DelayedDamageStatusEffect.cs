using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;

using Handelabra.Sentinels.Engine.Controller;
using Handelabra.Sentinels.Engine.Model;

namespace Jp.SOTMUtilities
{
    // A reflection status effect indicating that one card will do damage to a target at a the start of a later phase.
    // Due to engine limitations, you must still provide the method for this reflection status effect to call.
    // That function can simply be:
    //
    // public IEnumerator HandleDelayedDamage(PhaseChangeAction unused, OnPhaseChangeStatusEffect sourceEffect)
    // {
    //     return this.DoDelayedDamage(sourceEffect);
    // }
    //
    // The damage will be cancelled if the card card receiving the damage leaves play. The damage source is not
    // specified in the status effect, so the effect will not necessarily expire if the damage source leaves play.
    // That's up to you.
    // 
    // The damage will not occur until the specific phase it happens in occurs, and damage triggers will fire then.
    // 
    // The only expiry criteria in the status effect is TargetLeavesPlayExpiryCriteria.IsOneOfTheseCards. Additional
    // criteria can be used if you wish to expire on other things.
    //
    // Use the effect by making a new DelayedDamageStatusEffect then calling DealDamageToTargetAtStartOfNextTurn.
    [Serializable]
    public class DelayedDamageStatusEffect : OnPhaseChangeStatusEffect
    {
        public DelayedDamageStatusEffect(Card cardWithMethod, string nameOfMethod, string description, Card cardSource)
            : base(cardWithMethod, nameOfMethod, description, new TriggerType[] { TriggerType.DealDamage }, cardSource)
        {
        }

        // At the start of 'player's next turn, deal 'target' 'damage' damage of type 'damageType',
        // with an as-yet undetermined damage source.
        public void DealDamageToTargetAtStartOfNextTurn(TurnTaker player, Card target, int damage, DamageType damageType)
        {
            DamageToDeal = damage;
            DamageType = damageType;

            UntilTargetLeavesPlay(target);

            TurnTakerCriteria.IsSpecificTurnTaker = player;
            TurnPhaseCriteria.Phase = Phase.Start;
            BeforeOrAfter = BeforeOrAfter.After;
            CanEffectStack = true;
            DoesDealDamage = true;
        }

        public int DamageToDeal { get; private set; }
        public DamageType DamageType { get; private set; }

        public Card Target { get { return TargetLeavesPlayExpiryCriteria.IsOneOfTheseCards.FirstOrDefault(); } }
    }

    public static class DelayedDamageExtension
    {
        // Fires the delayed damage status effect. Source 'damageSource' will do the damage stored in 'effect', with
        // 'co' as the CardSource.
        //
        // If 'effect' isn't a DelayedDamageStatusEffect, does something unspecified. Don't do that.
        //
        // Will add 'co' to CardControllerListType.CanCauseDamageOutOfPlay.
        //
        // If 'damageSource' is null, it defaults to 'co.CharacterCard'.
        //
        // Will expire the status effect.
        public static IEnumerator DoDelayedDamage(this CardController co, OnPhaseChangeStatusEffect effect, DamageSource damageSource = null)
        {
            co.AddThisCardControllerToList(CardControllerListType.CanCauseDamageOutOfPlay);

            if (damageSource == null)
            {
                damageSource = new DamageSource(co.GameController, co.CharacterCard);
            }

            IEnumerator e;
            while (true)
            {
                var delayedDamageEffect = effect as DelayedDamageStatusEffect;
                if (delayedDamageEffect == null) { break; }

                var target = delayedDamageEffect.Target;
                if (target == null) { break; }

                if (damageSource.IsCard && damageSource.Card.IsIncapacitatedOrOutOfGame) { break; }
                if (! target.IsTarget || !target.IsInPlayAndNotUnderCard) { break; }
                if (co.GameController.IsCardVisibleToCardSource(target, co.GetCardSource(delayedDamageEffect)))
                {
                    e = co.GameController.DealDamageToTarget(
                        damageSource,
                        target,
                        delayedDamageEffect.DamageToDeal,
                        delayedDamageEffect.DamageType,
                        cardSource: co.GetCardSource()
                    );
                    if (co.UseUnityCoroutines)
                    {
                        yield return co.GameController.StartCoroutine(e);
                    }
                    else
                    {
                        co.GameController.ExhaustCoroutine(e);
                    }
                }
                else
                {
                    e = co.GameController.SendMessageAction(
                        $"{target.Title} is no longer visible",
                        Priority.Medium,
                        co.GetCardSource(),
                        new[] { target },
                        showCardSource: true
                    );
                    if (co.UseUnityCoroutines)
                    {
                        yield return co.GameController.StartCoroutine(e);
                    }
                    else
                    {
                        co.GameController.ExhaustCoroutine(e);
                    }
                }

                break;
            }

            e = co.GameController.ExpireStatusEffect(effect, co.GetCardSource());
            if (co.UseUnityCoroutines)
            {
                yield return co.GameController.StartCoroutine(e);
            }
            else
            {
                co.GameController.ExhaustCoroutine(e);
            }
        }
    }
}
