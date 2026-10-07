using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;

using Handelabra.Sentinels.Engine.Controller;
using Handelabra.Sentinels.Engine.Model;

using Jp.SOTMUtilities;

namespace Jp.SOTMUtilities.TestMod.PowerlessTestHero
{
    // A hero character card with no printed powers
    public class PowerlessTestHeroCharacterCardController : HeroCharacterCardController
    {
        public PowerlessTestHeroCharacterCardController(Card card, TurnTakerController controller) : base(card, controller)
        { }

        // Called by the DelayedDamageStatusEffects in DelayedDamageStatusEffectTests.
        public IEnumerator HandleDelayedDamage(PhaseChangeAction unused, OnPhaseChangeStatusEffect sourceEffect)
        {
            return this.DoDelayedDamage(sourceEffect);
        }
    }
}
