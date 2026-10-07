using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NUnit.Framework;
using NUnit.Framework.Legacy;

using Handelabra;
using Handelabra.Sentinels.Engine.Model;
using Handelabra.Sentinels.Engine.Controller;
using Handelabra.Sentinels.UnitTest;

namespace Jp.SOTMUtilities.UnitTest
{
    [TestFixture()]
    public class SelectTargetsToDealDamageToTargetTests : BaseTest
    {
        [Test()]
        public void TestRegularUsage()
        {
            SetupGameController("BaronBlade", "Legacy", "Tempest", "Megalopolis");

            StartGame();

            DecisionSelectCards = new Card[]
            {
                legacy.CharacterCard,
                GetMobileDefensePlatform().Card,
                GetMobileDefensePlatform().Card
            };

            QuickHPStorage(new Card[] {
                baron.CharacterCard,
                legacy.CharacterCard,
                tempest.CharacterCard,
                GetMobileDefensePlatform().Card
            });

            RunCoroutine(
                legacy.CharacterCardController.SelectTargetsToDealDamageToTarget(
                    legacy,
                    c => c.Is().Hero().Target().AccordingTo(legacy.CharacterCardController),
                    c => c.Is().Villain().Target().AccordingTo(legacy.CharacterCardController),
                    2,
                    DamageType.Infernal
                )
            );

            // Legacy and Tempest both hit the platform.
            QuickHPCheck(0, 0, 0, -4);
        }

        // Runs SelectTargetsToDealDamageToTarget with every hero target as a damage dealer, returning the dealers in
        // the order they acted. 'onAct' is run for each dealer as it acts.
        private List<Card> RunWithHeroTargets(HeroTurnTakerController decisionMaker, Func<Card, IEnumerator> onAct = null)
        {
            var acted = new List<Card>();
            var controller = decisionMaker.CharacterCardController;

            RunCoroutine(
                controller.SelectTargetsToDealDamageToTarget(
                    decisionMaker,
                    c => c.Is().Hero().Target().AccordingTo(controller),
                    c =>
                    {
                        acted.Add(c);
                        return onAct == null ? DoNothing() : onAct(c);
                    }
                )
            );

            return acted;
        }

        [Test()]
        public void TestPlayerChoosesOrder()
        {
            SetupGameController("BaronBlade", "Legacy", "Tempest", "Haka", "Megalopolis");

            StartGame();

            DecisionSelectCards = new Card[] { haka.CharacterCard, tempest.CharacterCard, legacy.CharacterCard };

            var acted = RunWithHeroTargets(legacy);

            CollectionAssert.AreEqual(new Card[] { haka.CharacterCard, tempest.CharacterCard, legacy.CharacterCard }, acted);
        }

        [Test()]
        public void TestAutoDecide()
        {
            SetupGameController("BaronBlade", "Legacy", "Tempest", "Haka", "Megalopolis");

            StartGame();

            DecisionAutoDecide = SelectionType.CardToDealDamage;
            var decisionsBefore = GameController.Game.Journal.DecisionAnswerEntries(e => true).Count();

            var acted = RunWithHeroTargets(legacy);

            // Every hero acts, but the player is only asked once.
            CollectionAssert.AreEquivalent(new Card[] { legacy.CharacterCard, tempest.CharacterCard, haka.CharacterCard }, acted);
            ClassicAssert.AreEqual(1, GameController.Game.Journal.DecisionAnswerEntries(e => true).Count() - decisionsBefore);
        }

        [Test()]
        public void TestDealerEnteringPlayActs()
        {
            SetupGameController("BaronBlade", "Unity", "Tempest", "Megalopolis");

            StartGame();

            var bot = GetCard("TurretBot");
            DecisionSelectCards = new Card[] { unity.CharacterCard, tempest.CharacterCard, bot };

            // Unity puts a bot into play when she acts; it should act too.
            var acted = RunWithHeroTargets(
                unity,
                c => c == unity.CharacterCard ?
                    GameController.PlayCard(unity, bot, isPutIntoPlay: true, cardSource: unity.CharacterCardController.GetCardSource()) :
                    DoNothing()
            );

            CollectionAssert.AreEqual(new Card[] { unity.CharacterCard, tempest.CharacterCard, bot }, acted);
        }

        [Test()]
        public void TestDealerLeavingPlayDoesNotAct()
        {
            SetupGameController("BaronBlade", "Unity", "Tempest", "Megalopolis");

            StartGame();

            var bot = PlayCard("TurretBot");
            DecisionSelectCards = new Card[] { unity.CharacterCard, tempest.CharacterCard };

            // Unity destroys the bot when she acts, so it shouldn't get to act.
            var acted = RunWithHeroTargets(
                unity,
                c => c == unity.CharacterCard ?
                    GameController.DestroyCard(unity, bot, cardSource: unity.CharacterCardController.GetCardSource()) :
                    DoNothing()
            );

            CollectionAssert.AreEqual(new Card[] { unity.CharacterCard, tempest.CharacterCard }, acted);
        }

        [Test()]
        public void TestNoLegalTarget()
        {
            SetupGameController("BaronBlade", "Legacy", "Tempest", "Megalopolis");

            StartGame();

            DecisionSelectCards = new Card[] { legacy.CharacterCard };

            QuickHPStorage(new Card[] {
                baron.CharacterCard,
                legacy.CharacterCard,
                tempest.CharacterCard,
                GetMobileDefensePlatform().Card
            });

            // There are no environment targets in play.
            RunCoroutine(
                legacy.CharacterCardController.SelectTargetsToDealDamageToTarget(
                    legacy,
                    c => c.Is().Hero().Target().AccordingTo(legacy.CharacterCardController),
                    c => c.Is().Environment().Target(),
                    2,
                    DamageType.Infernal
                )
            );

            QuickHPCheck(0, 0, 0, 0);
        }
    }
}
