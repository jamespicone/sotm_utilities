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
    }
}
