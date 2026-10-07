using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NUnit.Framework;
using NUnit.Framework.Legacy;

using Handelabra.Sentinels.Engine.Model;
using Handelabra.Sentinels.Engine.Controller;
using Handelabra.Sentinels.UnitTest;
using Handelabra.Sentinels.Engine.Controller.VoidGuardMainstay;
using Handelabra;
using System.Diagnostics;

namespace Jp.SOTMUtilities.UnitTest
{
    [TestFixture()]
    public class IsResponsibleTests : BaseTest
    {
        // Runs 'action', calling 'check' on each DestroyCardAction performed while it runs. Fails if no card was
        // destroyed, so a test can't pass just because 'check' never ran.
        private void CheckDestructions(Action action, Action<DestroyCardAction> check)
        {
            int destructions = 0;
            Func<GameAction, IEnumerator> observeDestruction = (ga) => {
                if (ga is DestroyCardAction dca)
                {
                    destructions++;
                    check(dca);
                }

                return DoNothing();
            };

            GameController.OnDidPerformAction += observeDestruction.Invoke;
            try
            {
                action();
            }
            finally
            {
                GameController.OnDidPerformAction -= observeDestruction.Invoke;
            }

            ClassicAssert.Greater(destructions, 0, "Expected a card to be destroyed");
        }

        [Test()]
        public void TestDestroyWithDamage()
        {
            SetupGameController("BaronBlade", "Legacy", "Tempest", "Megalopolis");

            StartGame();

            RemoveVillainTriggers();
            RemoveVillainCards();

            var platform = PlayCard("MobileDefensePlatform");
            SetHitPoints(platform, 1);

            DecisionSelectTarget = platform;

            CheckDestructions(() => PlayCard("BackFistStrike"), dca =>
            {
                ClassicAssert.AreEqual(dca.WasCardDestroyed, true);
                ClassicAssert.AreEqual(dca.CardToDestroy.Card, platform);
                ClassicAssert.AreEqual(legacy.TurnTaker.IsResponsible(dca), true);
                ClassicAssert.AreEqual(tempest.TurnTaker.IsResponsible(dca), false);
            });
        }

        [Test()]
        public void TestDestroyWithDestroyEffect()
        {
            SetupGameController("BaronBlade", "Tachyon", "Tempest", "Megalopolis");

            StartGame();

            RemoveVillainTriggers();
            RemoveVillainCards();

            var forcefield = PlayCard("LivingForceField");
            DecisionSelectCard = forcefield;

            CheckDestructions(() => PlayCard("BlindingSpeed"), dca =>
            {
                ClassicAssert.AreEqual(true, dca.WasCardDestroyed);
                ClassicAssert.AreEqual(forcefield, dca.CardToDestroy.Card);
                ClassicAssert.AreEqual(true, tachyon.TurnTaker.IsResponsible(dca));
                ClassicAssert.AreEqual(false, tempest.TurnTaker.IsResponsible(dca));
            });
        }

        [Test()]
        public void TestDestroyWithTurnTakerDamage()
        {
            SetupGameController("BaronBlade", "Tachyon", "Tempest", "Megalopolis");

            StartGame();

            RemoveVillainTriggers();
            RemoveVillainCards();

            var platform = PlayCard("MobileDefensePlatform");
            SetHitPoints(platform, 1);

            CheckDestructions(
                () => RunCoroutine(GameController.DealDamageToTarget(
                    new DamageSource(GameController, tachyon.TurnTaker),
                    platform,
                    10,
                    DamageType.Infernal,
                    cardSource: tachyon.CharacterCardController.GetCardSource()
                )),
                dca =>
                {
                    ClassicAssert.AreEqual(true, dca.WasCardDestroyed);
                    ClassicAssert.AreEqual(platform, dca.CardToDestroy.Card);
                    ClassicAssert.AreEqual(true, tachyon.TurnTaker.IsResponsible(dca));
                    ClassicAssert.AreEqual(false, tempest.TurnTaker.IsResponsible(dca));
                }
            );
        }

        // Sets up Kaargra Warfang vs Haka with the Death-Caller title under Haka. Its footer reads "Whenever this
        // target reduces another target to 3 or fewer HP, destroy that target."
        private Card SetupDeathCallerUnderHaka(params string[] otherHeroes)
        {
            SetupGameController(new[] { "KaargraWarfang", "Haka" }.Concat(otherHeroes).Concat(new[] { "TheCelestialTribunal" }).ToArray());

            MoveAllCards(warfang, warfang.TurnTaker.FindSubDeck("TitleDeck"), warfang.TurnTaker.OutOfGame);
            StartGame();
            RemoveVillainCards();
            RemoveVillainTriggers();

            var title = PlayCard("TitleDeathCaller");

            // Simple way of getting deathcaller onto haka
            var target = PlayCard("OrrimHiveminded");
            DestroyCard(target, cardSource: haka.CharacterCard);

            AssertAtLocation(title, haka.CharacterCard.BelowLocation);
            return title;
        }

        [Test()]
        public void TestDeathcaller()
        {
            var title = SetupDeathCallerUnderHaka();

            var target2 = PlayCard("OrrimHiveminded");

            // The title destroys the target, but the engine makes Haka (the target it's under) the responsible card.
            CheckDestructions(
                () =>
                {
                    SetHitPoints(target2, 10);
                    DealDamage(haka, target2, 9, DamageType.Infernal);
                },
                dca =>
                {
                    ClassicAssert.AreEqual(true, dca.WasCardDestroyed);
                    ClassicAssert.AreEqual(target2, dca.CardToDestroy.Card);
                    ClassicAssert.AreEqual(true, haka.TurnTaker.IsResponsible(dca));
                    ClassicAssert.AreEqual(false, title.Owner.IsResponsible(dca));
                }
            );
        }

        [Test()]
        public void TestDestroyByCardBelowCharacterCard()
        {
            var title = SetupDeathCallerUnderHaka("Legacy");

            var target = PlayCard("OrrimHiveminded");

            // Destroyed by the title with no responsible card: Haka is responsible because the title is under Haka,
            // and Warfang because they own it.
            CheckDestructions(() => DestroyCard(target, cardSource: title), dca =>
            {
                ClassicAssert.AreEqual(target, dca.CardToDestroy.Card);
                ClassicAssert.AreEqual(true, haka.TurnTaker.IsResponsible(dca));
                ClassicAssert.AreEqual(true, warfang.TurnTaker.IsResponsible(dca));
                ClassicAssert.AreEqual(false, legacy.TurnTaker.IsResponsible(dca));
            });
        }

        [Test()]
        public void TestDestroyByCardNextToCharacterCard()
        {
            SetupGameController("BaronBlade", "Legacy", "Tempest", "Megalopolis");

            StartGame();

            RemoveVillainTriggers();
            RemoveVillainCards();

            var platform = PlayCard("MobileDefensePlatform");
            var field = PlayCard("BacklashField");
            MoveCard(baron, field, legacy.CharacterCard.NextToLocation);
            RemoveVillainTriggers();

            // Destroyed by a villain card next to Legacy: Legacy is responsible because it's next to Legacy, and
            // Baron Blade because he owns it.
            CheckDestructions(() => DestroyCard(platform, cardSource: field), dca =>
            {
                ClassicAssert.AreEqual(platform, dca.CardToDestroy.Card);
                ClassicAssert.AreEqual(true, legacy.TurnTaker.IsResponsible(dca));
                ClassicAssert.AreEqual(true, baron.TurnTaker.IsResponsible(dca));
                ClassicAssert.AreEqual(false, tempest.TurnTaker.IsResponsible(dca));
            });
        }

        [Test()]
        public void TestDestroyWithNoResponsibleCard()
        {
            SetupGameController("BaronBlade", "Legacy", "Tempest", "Megalopolis");

            StartGame();

            RemoveVillainTriggers();
            RemoveVillainCards();

            var platform = PlayCard("MobileDefensePlatform");

            CheckDestructions(() => DestroyCard(platform), dca =>
            {
                ClassicAssert.AreEqual(platform, dca.CardToDestroy.Card);
                ClassicAssert.AreEqual(false, baron.TurnTaker.IsResponsible(dca));
                ClassicAssert.AreEqual(false, legacy.TurnTaker.IsResponsible(dca));
                ClassicAssert.AreEqual(false, tempest.TurnTaker.IsResponsible(dca));
            });
        }

        [Test()]
        public void TestDestroyByDeckTarget()
        {
            SetupGameController("BaronBlade", "Unity", "Tempest", "Megalopolis");

            StartGame();

            RemoveVillainTriggers();
            RemoveVillainCards();

            var platform = PlayCard("MobileDefensePlatform");
            var bot = PlayCard("TurretBot");

            // As with damage from a target (TestDestroyWithDamageFromDeckTarget), a target that destroys a card is
            // responsible itself, rather than its owner.
            CheckDestructions(() => DestroyCard(platform, cardSource: bot), dca =>
            {
                ClassicAssert.AreEqual(platform, dca.CardToDestroy.Card);
                ClassicAssert.AreEqual(false, unity.TurnTaker.IsResponsible(dca));
                ClassicAssert.AreEqual(false, tempest.TurnTaker.IsResponsible(dca));
            });
        }

        [Test()]
        public void TestDestroyWithDamageFromMultiCharacterHero()
        {
            SetupGameController("BaronBlade", "TheSentinels", "Tempest", "Megalopolis");

            StartGame();

            RemoveVillainTriggers();
            RemoveVillainCards();

            var platform = PlayCard("MobileDefensePlatform");
            SetHitPoints(platform, 1);

            CheckDestructions(() => DealDamage(writhe, platform, 2, DamageType.Infernal), dca =>
            {
                ClassicAssert.AreEqual(platform, dca.CardToDestroy.Card);
                ClassicAssert.AreEqual(true, sentinels.TurnTaker.IsResponsible(dca));
                ClassicAssert.AreEqual(false, tempest.TurnTaker.IsResponsible(dca));
            });
        }

        [Test()]
        public void TestDestroyWithDamageFromDeckTarget()
        {
            SetupGameController("BaronBlade", "Unity", "Tempest", "Megalopolis");

            StartGame();

            RemoveVillainTriggers();
            RemoveVillainCards();

            var platform = PlayCard("MobileDefensePlatform");
            SetHitPoints(platform, 1);

            var bot = PlayCard("TurretBot");
            CheckDestructions(() => DealDamage(bot, platform, 10, DamageType.Infernal), dca =>
            {
                ClassicAssert.AreEqual(dca.WasCardDestroyed, true);
                ClassicAssert.AreEqual(dca.CardToDestroy.Card, platform);
                ClassicAssert.AreEqual(unity.TurnTaker.IsResponsible(dca), false);
                ClassicAssert.AreEqual(tempest.TurnTaker.IsResponsible(dca), false);
            });
        }

        [Test()]
        public void TestDestroyWithPowerOnTarget()
        {
            SetupGameController("BaronBlade", "Ra", "Tempest", "RealmOfDiscord");

            StartGame();

            RemoveVillainTriggers();
            RemoveVillainCards();

            var gaze = PlayCard("WrathfulGaze");
            PlayCard("ImbuedVitality");

            var platform = PlayCard("MobileDefensePlatform");
            SetHitPoints(platform, 1);

            GoToUsePowerPhase(ra);
            CheckDestructions(() => UsePower(gaze), dca =>
            {
                ClassicAssert.AreEqual(true, dca.WasCardDestroyed);
                ClassicAssert.AreEqual(platform, dca.CardToDestroy.Card);
                ClassicAssert.AreEqual(true, ra.TurnTaker.IsResponsible(dca));
                ClassicAssert.AreEqual(false, tempest.TurnTaker.IsResponsible(dca));
            });
        }

        [Test()]
        public void TestDestroyWithPower()
        {
            SetupGameController("BaronBlade", "Ra", "Tempest", "RealmOfDiscord");

            StartGame();

            RemoveVillainTriggers();
            RemoveVillainCards();

            var gaze = PlayCard("WrathfulGaze");

            var platform = PlayCard("MobileDefensePlatform");
            SetHitPoints(platform, 1);

            GoToUsePowerPhase(ra);
            CheckDestructions(() => UsePower(gaze), dca =>
            {
                ClassicAssert.AreEqual(true, dca.WasCardDestroyed);
                ClassicAssert.AreEqual(platform, dca.CardToDestroy.Card);
                ClassicAssert.AreEqual(true, ra.TurnTaker.IsResponsible(dca));
                ClassicAssert.AreEqual(false, tempest.TurnTaker.IsResponsible(dca));
            });
        }
    }
}
