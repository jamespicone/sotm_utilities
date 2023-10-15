using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NUnit.Framework;

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

            Func<GameAction, IEnumerator> observeDestruction = (ga) => {
                if (ga is DestroyCardAction dca)
                {
                    Assert.AreEqual(dca.WasCardDestroyed, true);
                    Assert.AreEqual(dca.CardToDestroy.Card, platform);
                    Assert.AreEqual(legacy.TurnTaker.IsResponsible(dca), true);
                    Assert.AreEqual(tempest.TurnTaker.IsResponsible(dca), false);
                }

                return DoNothing();
            };

            GameController.OnDidPerformAction += observeDestruction.Invoke;
            PlayCard("BackFistStrike");
            GameController.OnDidPerformAction -= observeDestruction.Invoke;
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

            Func<GameAction, IEnumerator> observeDestruction = (ga) => {
                if (ga is DestroyCardAction dca)
                {
                    Assert.AreEqual(true, dca.WasCardDestroyed);
                    Assert.AreEqual(forcefield, dca.CardToDestroy.Card);
                    Assert.AreEqual(true, tachyon.TurnTaker.IsResponsible(dca));
                    Assert.AreEqual(false, tempest.TurnTaker.IsResponsible(dca));
                }

                return DoNothing();
            };

            GameController.OnDidPerformAction += observeDestruction.Invoke;
            PlayCard("BlindingSpeed");
            GameController.OnDidPerformAction -= observeDestruction.Invoke;
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

            Func<GameAction, IEnumerator> observeDestruction = (ga) => {
                if (ga is DestroyCardAction dca)
                {
                    Assert.AreEqual(true, dca.WasCardDestroyed);
                    Assert.AreEqual(platform, dca.CardToDestroy.Card);
                    Assert.AreEqual(true, tachyon.TurnTaker.IsResponsible(dca));
                    Assert.AreEqual(false, tempest.TurnTaker.IsResponsible(dca));
                }

                return DoNothing();
            };

            GameController.OnDidPerformAction += observeDestruction.Invoke;
            RunCoroutine(GameController.DealDamageToTarget(
                new DamageSource(GameController, tachyon.TurnTaker),
                platform,
                10,
                DamageType.Infernal,
                cardSource: tachyon.CharacterCardController.GetCardSource()
            ));
            GameController.OnDidPerformAction -= observeDestruction.Invoke;
        }

        [Test()]
        public void TestDeathcaller()
        {
            SetupGameController("KaargraWarfang", "Haka", "TheCelestialTribunal");

            MoveAllCards(warfang, warfang.TurnTaker.FindSubDeck("TitleDeck"), warfang.TurnTaker.OutOfGame);
            StartGame();
            RemoveVillainCards();
            RemoveVillainTriggers();

            var title = PlayCard("TitleDeathCaller");

            // Simple way of getting deathcaller onto haka
            var target = PlayCard("OrrimHiveminded");
            DestroyCard(target, cardSource: haka.CharacterCard);

            AssertAtLocation(title, haka.CharacterCard.BelowLocation);

            var target2 = PlayCard("OrrimHiveminded");

            Func<GameAction, IEnumerator> observeDestruction = (ga) => {
                if (ga is DestroyCardAction dca)
                {
                    Assert.AreEqual(true, dca.WasCardDestroyed);
                    Assert.AreEqual(target2, dca.CardToDestroy.Card);
                    Assert.AreEqual(true, haka.TurnTaker.IsResponsible(dca));
                    Assert.AreEqual(false, title.Owner.IsResponsible(dca));
                }

                return DoNothing();
            };
            
            SetHitPoints(target2, 10);
            DealDamage(haka, target2, 9, DamageType.Infernal);
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
            Func<GameAction, IEnumerator> observeDestruction = (ga) => {
                if (ga is DestroyCardAction dca)
                {
                    Assert.AreEqual(dca.WasCardDestroyed, true);
                    Assert.AreEqual(dca.CardToDestroy.Card, platform);
                    Assert.AreEqual(unity.TurnTaker.IsResponsible(dca), false);
                    Assert.AreEqual(tempest.TurnTaker.IsResponsible(dca), false);
                }

                return DoNothing();
            };

            GameController.OnDidPerformAction += observeDestruction.Invoke;
            DealDamage(bot, platform, 10, DamageType.Infernal);
            GameController.OnDidPerformAction -= observeDestruction.Invoke;
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

            Func<GameAction, IEnumerator> observeDestruction = (ga) => {
                if (ga is DestroyCardAction dca)
                {
                    Log.Debug($"dca: {dca}, {dca.ActionSource}, {dca.CardSource}, {dca.DecisionSources}");
                    Assert.AreEqual(true, dca.WasCardDestroyed);
                    Assert.AreEqual(platform, dca.CardToDestroy.Card);
                    Assert.AreEqual(true, ra.TurnTaker.IsResponsible(dca));
                    Assert.AreEqual(false, tempest.TurnTaker.IsResponsible(dca));
                }

                return DoNothing();
            };

            GameController.OnDidPerformAction += observeDestruction.Invoke;
            GoToUsePowerPhase(ra);
            UsePower(gaze);
            GameController.OnDidPerformAction -= observeDestruction.Invoke;
        }
    }
}
