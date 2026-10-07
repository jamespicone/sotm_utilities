using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NUnit.Framework;

using Handelabra.Sentinels.Engine.Model;
using Handelabra.Sentinels.Engine.Controller;
using Handelabra.Sentinels.UnitTest;

namespace Jp.SOTMUtilities.UnitTest
{
    [TestFixture()]
    public class DelayedDamageStatusEffectTests : BaseTest
    {
        // The Powerless Test Hero's character card has the HandleDelayedDamage method the effects call, and deals
        // the damage.
        private Card Powerless { get { return GetCard("PowerlessTestHeroCharacter"); } }

        private void SetupDelayedDamageGame()
        {
            SetupGameController("BaronBlade", "Jp.SOTMUtilities.TestMod.PowerlessTestHero", "Legacy", "Megalopolis");
            StartGame();

            RemoveVillainCards();
            RemoveVillainTriggers();
        }

        private void AddDelayedDamage(TurnTaker player, Card target, int amount)
        {
            var effect = new DelayedDamageStatusEffect(Powerless, "HandleDelayedDamage", "Delayed damage", Powerless);
            effect.DealDamageToTargetAtStartOfNextTurn(player, target, amount, DamageType.Fire);
            RunCoroutine(GameController.AddStatusEffect(effect, true, GetCardController(Powerless).GetCardSource()));
        }

        [Test()]
        public void TestDamageAtStartOfTurn()
        {
            SetupDelayedDamageGame();

            AddDelayedDamage(legacy.TurnTaker, legacy.CharacterCard, 3);
            AssertNumberOfStatusEffectsInPlay(1);

            QuickHPStorage(legacy);

            // Nothing happens at the start of the Powerless Test Hero's turn...
            GoToStartOfTurn(GameController.FindTurnTakerController(Powerless.Owner));
            QuickHPCheck(0);
            AssertNumberOfStatusEffectsInPlay(1);

            // ...only at the start of Legacy's.
            GoToStartOfTurn(legacy);
            QuickHPCheck(-3);
            AssertNumberOfStatusEffectsInPlay(0);
        }

        [Test()]
        public void TestAddedDuringStartPhaseWaitsForNextTurn()
        {
            SetupDelayedDamageGame();

            GoToStartOfTurn(legacy);
            AddDelayedDamage(legacy.TurnTaker, legacy.CharacterCard, 3);

            QuickHPStorage(legacy);
            GoToEndOfTurn(legacy);
            QuickHPCheck(0);

            GoToStartOfTurn(legacy);
            QuickHPCheck(-3);
            AssertNumberOfStatusEffectsInPlay(0);
        }

        [Test()]
        public void TestEffectsStack()
        {
            SetupDelayedDamageGame();

            AddDelayedDamage(legacy.TurnTaker, legacy.CharacterCard, 3);
            AddDelayedDamage(legacy.TurnTaker, legacy.CharacterCard, 2);
            AssertNumberOfStatusEffectsInPlay(2);

            QuickHPStorage(legacy);
            GoToStartOfTurn(legacy);
            QuickHPCheck(-5);
            AssertNumberOfStatusEffectsInPlay(0);
        }

        [Test()]
        public void TestExpiresWhenTargetLeavesPlay()
        {
            SetupDelayedDamageGame();

            var platform = PlayCard("MobileDefensePlatform");
            AddDelayedDamage(legacy.TurnTaker, platform, 3);
            AssertNumberOfStatusEffectsInPlay(1);

            DestroyCard(platform);
            AssertNumberOfStatusEffectsInPlay(0);

            GoToStartOfTurn(legacy);
        }

        [Test()]
        public void TestNoDamageIfSourceIncapacitated()
        {
            SetupDelayedDamageGame();

            AddDelayedDamage(legacy.TurnTaker, legacy.CharacterCard, 3);
            DealDamage(baron, Powerless, 99, DamageType.Infernal);
            AssertIncapacitated((HeroTurnTakerController)GameController.FindTurnTakerController(Powerless.Owner));

            QuickHPStorage(legacy);
            GoToStartOfTurn(legacy);
            QuickHPCheck(0);
            AssertNumberOfStatusEffectsInPlay(0);
        }

        [Test()]
        public void TestSurvivesSaveAndLoad()
        {
            SetupDelayedDamageGame();

            AddDelayedDamage(legacy.TurnTaker, legacy.CharacterCard, 3);
            SaveAndLoad();
            AssertNumberOfStatusEffectsInPlay(1);

            QuickHPStorage(legacy);
            GoToStartOfTurn(legacy);
            QuickHPCheck(-3);
            AssertNumberOfStatusEffectsInPlay(0);
        }
    }
}
