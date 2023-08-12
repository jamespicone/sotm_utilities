using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NUnit.Framework;

using Handelabra.Sentinels.Engine.Model;
using Handelabra.Sentinels.UnitTest;

namespace Jp.SOTMUtilities.UnitTest
{
    [TestFixture()]
    public class AlignmentTestVillainTests : BaseTest
    {
        [Test()]
        public void TestVillainTurnTaker()
        {
            SetupGameController("BaronBlade", "Legacy", "Jp.SOTMUtilities.TestMod.AlignmentTestVillain");

            var tt = baron.TurnTaker;
            var controller = GetCardController(legacy.CharacterCard);

            Assert.IsFalse(tt.Is().Environment());
            Assert.IsFalse(tt.Is().Environment().Card());
            Assert.IsFalse(tt.Is().Environment().Target());
            Assert.IsFalse(tt.Is().Environment().NonTarget());

            Assert.IsFalse(tt.Is().Hero().AccordingTo(controller));
            Assert.IsFalse(tt.Is().Hero().Card().AccordingTo(controller));
            Assert.IsFalse(tt.Is().Hero().Target().AccordingTo(controller));
            Assert.IsFalse(tt.Is().Hero().NonTarget().AccordingTo(controller));

            Assert.IsTrue(tt.Is().Villain().AccordingTo(controller));
            Assert.IsFalse(tt.Is().Villain().AccordingTo(controller).Card());
            Assert.IsFalse(tt.Is().Villain().Target().AccordingTo(controller));
            Assert.IsTrue(tt.Is().Villain().NonTarget().AccordingTo(controller));

            Assert.IsTrue(tt.Is().NonEnvironment());
            Assert.IsFalse(tt.Is().NonEnvironment().Card());
            Assert.IsFalse(tt.Is().NonEnvironment().Target());
            Assert.IsTrue(tt.Is().NonEnvironment().NonTarget());

            Assert.IsTrue(tt.Is().NonHero().AccordingTo(controller));
            Assert.IsFalse(tt.Is().NonHero().Card().AccordingTo(controller));
            Assert.IsFalse(tt.Is().NonHero().Target().AccordingTo(controller));
            Assert.IsTrue(tt.Is().NonHero().NonTarget().AccordingTo(controller));

            Assert.IsFalse(tt.Is().NonVillain().AccordingTo(controller));
            Assert.IsFalse(tt.Is().NonVillain().AccordingTo(controller).Card());
            Assert.IsFalse(tt.Is().NonVillain().Target().AccordingTo(controller));
            Assert.IsFalse(tt.Is().NonVillain().NonTarget().AccordingTo(controller));
        }

        [Test()]
        public void TestEnvCard()
        {
            SetupGameController("BaronBlade", "Legacy", "Jp.SOTMUtilities.TestMod.AlignmentTestVillain");

            var card = GetCard("VillainOwnedEnvCard");
            var controller = GetCardController(card);
            
            Assert.IsTrue(card.Is().Environment());
            Assert.IsTrue(card.Is().Environment().Card());
            Assert.IsFalse(card.Is().Environment().Target());
            Assert.IsTrue(card.Is().Environment().NonTarget());

            Assert.IsFalse(card.Is().Hero().AccordingTo(controller));
            Assert.IsFalse(card.Is().Hero().AccordingTo(controller).Card());
            Assert.IsFalse(card.Is().Hero().AccordingTo(controller).Target());
            Assert.IsFalse(card.Is().Hero().AccordingTo(controller).NonTarget());

            Assert.IsFalse(card.Is().Villain().AccordingTo(controller));
            Assert.IsFalse(card.Is().Villain().AccordingTo(controller).Card());
            Assert.IsFalse(card.Is().Villain().Target().AccordingTo(controller));
            Assert.IsFalse(card.Is().Villain().NonTarget().AccordingTo(controller));

            Assert.IsFalse(card.Is().NonEnvironment());
            Assert.IsFalse(card.Is().NonEnvironment().Card());
            Assert.IsFalse(card.Is().NonEnvironment().Target());
            Assert.IsFalse(card.Is().NonEnvironment().NonTarget());

            Assert.IsTrue(card.Is().NonHero().AccordingTo(controller));
            Assert.IsTrue(card.Is().NonHero().AccordingTo(controller).Card());
            Assert.IsFalse(card.Is().NonHero().AccordingTo(controller).Target());
            Assert.IsTrue(card.Is().NonHero().AccordingTo(controller).NonTarget());

            Assert.IsTrue(card.Is().NonVillain().AccordingTo(controller));
            Assert.IsTrue(card.Is().NonVillain().AccordingTo(controller).Card());
            Assert.IsFalse(card.Is().NonVillain().Target().AccordingTo(controller));
            Assert.IsTrue(card.Is().NonVillain().NonTarget().AccordingTo(controller));
        }

        [Test()]
        public void TestHeroCard()
        {
            SetupGameController("BaronBlade", "Legacy", "Jp.SOTMUtilities.TestMod.AlignmentTestVillain");

            var card = GetCard("VillainOwnedHeroCard");
            var controller = GetCardController(card);

            Assert.IsFalse(card.Is().Environment());
            Assert.IsFalse(card.Is().Environment().Card());
            Assert.IsFalse(card.Is().Environment().Target());
            Assert.IsFalse(card.Is().Environment().NonTarget());

            Assert.IsTrue(card.Is().Hero().AccordingTo(controller));
            Assert.IsTrue(card.Is().Hero().AccordingTo(controller).Card());
            Assert.IsFalse(card.Is().Hero().AccordingTo(controller).Target());
            Assert.IsTrue(card.Is().Hero().AccordingTo(controller).NonTarget());

            Assert.IsFalse(card.Is().Villain().AccordingTo(controller));
            Assert.IsFalse(card.Is().Villain().AccordingTo(controller).Card());
            Assert.IsFalse(card.Is().Villain().Target().AccordingTo(controller));
            Assert.IsFalse(card.Is().Villain().NonTarget().AccordingTo(controller));

            Assert.IsTrue(card.Is().NonEnvironment());
            Assert.IsTrue(card.Is().NonEnvironment().Card());
            Assert.IsFalse(card.Is().NonEnvironment().Target());
            Assert.IsTrue(card.Is().NonEnvironment().NonTarget());

            Assert.IsFalse(card.Is().NonHero().AccordingTo(controller));
            Assert.IsFalse(card.Is().NonHero().AccordingTo(controller).Card());
            Assert.IsFalse(card.Is().NonHero().AccordingTo(controller).Target());
            Assert.IsFalse(card.Is().NonHero().AccordingTo(controller).NonTarget());

            Assert.IsTrue(card.Is().NonVillain().AccordingTo(controller));
            Assert.IsTrue(card.Is().NonVillain().AccordingTo(controller).Card());
            Assert.IsFalse(card.Is().NonVillain().Target().AccordingTo(controller));
            Assert.IsTrue(card.Is().NonVillain().NonTarget().AccordingTo(controller));
        }

        [Test()]
        public void TestVillainCard()
        {
            SetupGameController("BaronBlade", "Legacy", "Jp.SOTMUtilities.TestMod.AlignmentTestVillain");

            var card = GetCard("VillainOwnedVillainCard");
            var controller = GetCardController(card);

            Assert.IsFalse(card.Is().Environment());
            Assert.IsFalse(card.Is().Environment().Card());
            Assert.IsFalse(card.Is().Environment().Target());
            Assert.IsFalse(card.Is().Environment().NonTarget());

            Assert.IsFalse(card.Is().Hero().AccordingTo(controller));
            Assert.IsFalse(card.Is().Hero().AccordingTo(controller).Card());
            Assert.IsFalse(card.Is().Hero().AccordingTo(controller).Target());
            Assert.IsFalse(card.Is().Hero().AccordingTo(controller).NonTarget());

            Assert.IsTrue(card.Is().Villain().AccordingTo(controller));
            Assert.IsTrue(card.Is().Villain().AccordingTo(controller).Card());
            Assert.IsFalse(card.Is().Villain().Target().AccordingTo(controller));
            Assert.IsTrue(card.Is().Villain().NonTarget().AccordingTo(controller));

            Assert.IsTrue(card.Is().NonEnvironment());
            Assert.IsTrue(card.Is().NonEnvironment().Card());
            Assert.IsFalse(card.Is().NonEnvironment().Target());
            Assert.IsTrue(card.Is().NonEnvironment().NonTarget());

            Assert.IsTrue(card.Is().NonHero().AccordingTo(controller));
            Assert.IsTrue(card.Is().NonHero().AccordingTo(controller).Card());
            Assert.IsFalse(card.Is().NonHero().AccordingTo(controller).Target());
            Assert.IsTrue(card.Is().NonHero().AccordingTo(controller).NonTarget());

            Assert.IsFalse(card.Is().NonVillain().AccordingTo(controller));
            Assert.IsFalse(card.Is().NonVillain().AccordingTo(controller).Card());
            Assert.IsFalse(card.Is().NonVillain().Target().AccordingTo(controller));
            Assert.IsFalse(card.Is().NonVillain().NonTarget().AccordingTo(controller));
        }

        [Test()]
        public void TestEnvTarget()
        {
            SetupGameController("BaronBlade", "Legacy", "Jp.SOTMUtilities.TestMod.AlignmentTestVillain");

            var card = GetCard("VillainOwnedEnvTarget");
            var controller = GetCardController(card);

            Assert.IsTrue(card.Is().Environment());
            Assert.IsTrue(card.Is().Environment().Card());
            Assert.IsTrue(card.Is().Environment().Target());
            Assert.IsFalse(card.Is().Environment().NonTarget());

            Assert.IsFalse(card.Is().Hero().AccordingTo(controller));
            Assert.IsFalse(card.Is().Hero().AccordingTo(controller).Card());
            Assert.IsFalse(card.Is().Hero().AccordingTo(controller).Target());
            Assert.IsFalse(card.Is().Hero().AccordingTo(controller).NonTarget());

            Assert.IsFalse(card.Is().Villain().AccordingTo(controller));
            Assert.IsFalse(card.Is().Villain().AccordingTo(controller).Card());
            Assert.IsFalse(card.Is().Villain().Target().AccordingTo(controller));
            Assert.IsFalse(card.Is().Villain().NonTarget().AccordingTo(controller));

            Assert.IsFalse(card.Is().NonEnvironment());
            Assert.IsFalse(card.Is().NonEnvironment().Card());
            Assert.IsFalse(card.Is().NonEnvironment().Target());
            Assert.IsFalse(card.Is().NonEnvironment().NonTarget());

            Assert.IsTrue(card.Is().NonHero().AccordingTo(controller));
            Assert.IsTrue(card.Is().NonHero().AccordingTo(controller).Card());
            Assert.IsTrue(card.Is().NonHero().AccordingTo(controller).Target());
            Assert.IsFalse(card.Is().NonHero().AccordingTo(controller).NonTarget());

            Assert.IsTrue(card.Is().NonVillain().AccordingTo(controller));
            Assert.IsTrue(card.Is().NonVillain().AccordingTo(controller).Card());
            Assert.IsTrue(card.Is().NonVillain().Target().AccordingTo(controller));
            Assert.IsFalse(card.Is().NonVillain().NonTarget().AccordingTo(controller));
        }

        [Test()]
        public void TestHeroTarget()
        {
            SetupGameController("BaronBlade", "Legacy", "Jp.SOTMUtilities.TestMod.AlignmentTestVillain");

            var card = GetCard("VillainOwnedHeroTarget");
            var controller = GetCardController(card);

            Assert.IsFalse(card.Is().Environment());
            Assert.IsFalse(card.Is().Environment().Card());
            Assert.IsFalse(card.Is().Environment().Target());
            Assert.IsFalse(card.Is().Environment().NonTarget());

            Assert.IsTrue(card.Is().Hero().AccordingTo(controller));
            Assert.IsTrue(card.Is().Hero().AccordingTo(controller).Card());
            Assert.IsTrue(card.Is().Hero().AccordingTo(controller).Target());
            Assert.IsFalse(card.Is().Hero().AccordingTo(controller).NonTarget());

            Assert.IsFalse(card.Is().Villain().AccordingTo(controller));
            Assert.IsFalse(card.Is().Villain().AccordingTo(controller).Card());
            Assert.IsFalse(card.Is().Villain().Target().AccordingTo(controller));
            Assert.IsFalse(card.Is().Villain().NonTarget().AccordingTo(controller));

            Assert.IsTrue(card.Is().NonEnvironment());
            Assert.IsTrue(card.Is().NonEnvironment().Card());
            Assert.IsTrue(card.Is().NonEnvironment().Target());
            Assert.IsFalse(card.Is().NonEnvironment().NonTarget());

            Assert.IsFalse(card.Is().NonHero().AccordingTo(controller));
            Assert.IsFalse(card.Is().NonHero().AccordingTo(controller).Card());
            Assert.IsFalse(card.Is().NonHero().AccordingTo(controller).Target());
            Assert.IsFalse(card.Is().NonHero().AccordingTo(controller).NonTarget());

            Assert.IsTrue(card.Is().NonVillain().AccordingTo(controller));
            Assert.IsTrue(card.Is().NonVillain().AccordingTo(controller).Card());
            Assert.IsTrue(card.Is().NonVillain().Target().AccordingTo(controller));
            Assert.IsFalse(card.Is().NonVillain().NonTarget().AccordingTo(controller));
        }

        [Test()]
        public void TestVillainTarget()
        {
            SetupGameController("BaronBlade", "Legacy", "Jp.SOTMUtilities.TestMod.AlignmentTestVillain");

            var card = GetCard("VillainOwnedVillainTarget");
            var controller = GetCardController(card);

            Assert.IsFalse(card.Is().Environment());
            Assert.IsFalse(card.Is().Environment().Card());
            Assert.IsFalse(card.Is().Environment().Target());
            Assert.IsFalse(card.Is().Environment().NonTarget());

            Assert.IsFalse(card.Is().Hero().AccordingTo(controller));
            Assert.IsFalse(card.Is().Hero().AccordingTo(controller).Card());
            Assert.IsFalse(card.Is().Hero().AccordingTo(controller).Target());
            Assert.IsFalse(card.Is().Hero().AccordingTo(controller).NonTarget());

            Assert.IsTrue(card.Is().Villain().AccordingTo(controller));
            Assert.IsTrue(card.Is().Villain().AccordingTo(controller).Card());
            Assert.IsTrue(card.Is().Villain().Target().AccordingTo(controller));
            Assert.IsFalse(card.Is().Villain().NonTarget().AccordingTo(controller));

            Assert.IsTrue(card.Is().NonEnvironment());
            Assert.IsTrue(card.Is().NonEnvironment().Card());
            Assert.IsTrue(card.Is().NonEnvironment().Target());
            Assert.IsFalse(card.Is().NonEnvironment().NonTarget());

            Assert.IsTrue(card.Is().NonHero().AccordingTo(controller));
            Assert.IsTrue(card.Is().NonHero().AccordingTo(controller).Card());
            Assert.IsTrue(card.Is().NonHero().AccordingTo(controller).Target());
            Assert.IsFalse(card.Is().NonHero().AccordingTo(controller).NonTarget());

            Assert.IsFalse(card.Is().NonVillain().AccordingTo(controller));
            Assert.IsFalse(card.Is().NonVillain().AccordingTo(controller).Card());
            Assert.IsFalse(card.Is().NonVillain().Target().AccordingTo(controller));
            Assert.IsFalse(card.Is().NonVillain().NonTarget().AccordingTo(controller));
        }

        [Test()]
        public void TestEnvTargetVillainCard()
        {
            SetupGameController("BaronBlade", "Legacy", "Jp.SOTMUtilities.TestMod.AlignmentTestVillain");

            var card = GetCard("VillainOwnedEnvTargetVillainCard");
            var controller = GetCardController(card);

            Assert.IsFalse(card.Is().Environment());
            Assert.IsFalse(card.Is().Environment().Card());
            Assert.IsTrue(card.Is().Environment().Target());
            Assert.IsFalse(card.Is().Environment().NonTarget());

            Assert.IsFalse(card.Is().Hero().AccordingTo(controller));
            Assert.IsFalse(card.Is().Hero().AccordingTo(controller).Card());
            Assert.IsFalse(card.Is().Hero().AccordingTo(controller).Target());
            Assert.IsFalse(card.Is().Hero().AccordingTo(controller).NonTarget());

            Assert.IsTrue(card.Is().Villain().AccordingTo(controller));
            Assert.IsTrue(card.Is().Villain().AccordingTo(controller).Card());
            Assert.IsFalse(card.Is().Villain().Target().AccordingTo(controller));
            Assert.IsFalse(card.Is().Villain().NonTarget().AccordingTo(controller));

            Assert.IsTrue(card.Is().NonEnvironment());
            Assert.IsTrue(card.Is().NonEnvironment().Card());
            Assert.IsFalse(card.Is().NonEnvironment().Target());
            Assert.IsFalse(card.Is().NonEnvironment().NonTarget());

            Assert.IsTrue(card.Is().NonHero().AccordingTo(controller));
            Assert.IsTrue(card.Is().NonHero().AccordingTo(controller).Card());
            Assert.IsTrue(card.Is().NonHero().AccordingTo(controller).Target());
            Assert.IsFalse(card.Is().NonHero().AccordingTo(controller).NonTarget());

            Assert.IsFalse(card.Is().NonVillain().AccordingTo(controller));
            Assert.IsFalse(card.Is().NonVillain().AccordingTo(controller).Card());
            Assert.IsTrue(card.Is().NonVillain().Target().AccordingTo(controller));
            Assert.IsFalse(card.Is().NonVillain().NonTarget().AccordingTo(controller));
        }

        [Test()]
        public void TestHeroTargetVillainCard()
        {
            SetupGameController("BaronBlade", "Legacy", "Jp.SOTMUtilities.TestMod.AlignmentTestVillain");

            var card = GetCard("VillainOwnedHeroTargetVillainCard");
            var controller = GetCardController(card);

            Assert.IsFalse(card.Is().Environment());
            Assert.IsFalse(card.Is().Environment().Card());
            Assert.IsFalse(card.Is().Environment().Target());
            Assert.IsFalse(card.Is().Environment().NonTarget());

            Assert.IsFalse(card.Is().Hero().AccordingTo(controller));
            Assert.IsFalse(card.Is().Hero().AccordingTo(controller).Card());
            Assert.IsTrue(card.Is().Hero().AccordingTo(controller).Target());
            Assert.IsFalse(card.Is().Hero().AccordingTo(controller).NonTarget());

            Assert.IsTrue(card.Is().Villain().AccordingTo(controller));
            Assert.IsTrue(card.Is().Villain().AccordingTo(controller).Card());
            Assert.IsFalse(card.Is().Villain().Target().AccordingTo(controller));
            Assert.IsFalse(card.Is().Villain().NonTarget().AccordingTo(controller));

            Assert.IsTrue(card.Is().NonEnvironment());
            Assert.IsTrue(card.Is().NonEnvironment().Card());
            Assert.IsTrue(card.Is().NonEnvironment().Target());
            Assert.IsFalse(card.Is().NonEnvironment().NonTarget());

            Assert.IsTrue(card.Is().NonHero().AccordingTo(controller));
            Assert.IsTrue(card.Is().NonHero().AccordingTo(controller).Card());
            Assert.IsFalse(card.Is().NonHero().AccordingTo(controller).Target());
            Assert.IsFalse(card.Is().NonHero().AccordingTo(controller).NonTarget());

            Assert.IsFalse(card.Is().NonVillain().AccordingTo(controller));
            Assert.IsFalse(card.Is().NonVillain().AccordingTo(controller).Card());
            Assert.IsTrue(card.Is().NonVillain().Target().AccordingTo(controller));
            Assert.IsFalse(card.Is().NonVillain().NonTarget().AccordingTo(controller));
        }

        [Test()]
        public void TestVillainTargetVillainCard()
        {
            SetupGameController("BaronBlade", "Legacy", "Jp.SOTMUtilities.TestMod.AlignmentTestVillain");

            var card = GetCard("VillainOwnedVillainTargetVillainCard");
            var controller = GetCardController(card);

            Assert.IsFalse(card.Is().Environment());
            Assert.IsFalse(card.Is().Environment().Card());
            Assert.IsFalse(card.Is().Environment().Target());
            Assert.IsFalse(card.Is().Environment().NonTarget());

            Assert.IsFalse(card.Is().Hero().AccordingTo(controller));
            Assert.IsFalse(card.Is().Hero().AccordingTo(controller).Card());
            Assert.IsFalse(card.Is().Hero().AccordingTo(controller).Target());
            Assert.IsFalse(card.Is().Hero().AccordingTo(controller).NonTarget());

            Assert.IsTrue(card.Is().Villain().AccordingTo(controller));
            Assert.IsTrue(card.Is().Villain().AccordingTo(controller).Card());
            Assert.IsTrue(card.Is().Villain().Target().AccordingTo(controller));
            Assert.IsFalse(card.Is().Villain().NonTarget().AccordingTo(controller));

            Assert.IsTrue(card.Is().NonEnvironment());
            Assert.IsTrue(card.Is().NonEnvironment().Card());
            Assert.IsTrue(card.Is().NonEnvironment().Target());
            Assert.IsFalse(card.Is().NonEnvironment().NonTarget());

            Assert.IsTrue(card.Is().NonHero().AccordingTo(controller));
            Assert.IsTrue(card.Is().NonHero().AccordingTo(controller).Card());
            Assert.IsTrue(card.Is().NonHero().AccordingTo(controller).Target());
            Assert.IsFalse(card.Is().NonHero().AccordingTo(controller).NonTarget());

            Assert.IsFalse(card.Is().NonVillain().AccordingTo(controller));
            Assert.IsFalse(card.Is().NonVillain().AccordingTo(controller).Card());
            Assert.IsFalse(card.Is().NonVillain().Target().AccordingTo(controller));
            Assert.IsFalse(card.Is().NonVillain().NonTarget().AccordingTo(controller));
        }

        [Test()]
        public void TestModifiedDeckKind()
        {
            SetupGameController("BaronBlade", "Legacy", "Jp.SOTMUtilities.TestMod.AlignmentTestVillain");

            var card = GetCard("VillainCardInsistsItsHero");
            var controller = GetCardController(card);
            PlayCard(card); // can't be asked if its hero or not if its not in play

            Assert.IsFalse(card.Is().Environment());
            Assert.IsFalse(card.Is().Environment().Card());
            Assert.IsFalse(card.Is().Environment().Target());
            Assert.IsFalse(card.Is().Environment().NonTarget());

            Assert.IsTrue(card.Is().Hero().AccordingTo(controller));
            Assert.IsTrue(card.Is().Hero().AccordingTo(controller).Card());
            Assert.IsFalse(card.Is().Hero().AccordingTo(controller).Target());
            Assert.IsTrue(card.Is().Hero().AccordingTo(controller).NonTarget());

            Assert.IsFalse(card.Is().Villain().AccordingTo(controller));
            Assert.IsFalse(card.Is().Villain().AccordingTo(controller).Card());
            Assert.IsFalse(card.Is().Villain().Target().AccordingTo(controller));
            Assert.IsFalse(card.Is().Villain().NonTarget().AccordingTo(controller));

            Assert.IsTrue(card.Is().NonEnvironment());
            Assert.IsTrue(card.Is().NonEnvironment().Card());
            Assert.IsFalse(card.Is().NonEnvironment().Target());
            Assert.IsTrue(card.Is().NonEnvironment().NonTarget());

            Assert.IsFalse(card.Is().NonHero().AccordingTo(controller));
            Assert.IsFalse(card.Is().NonHero().AccordingTo(controller).Card());
            Assert.IsFalse(card.Is().NonHero().AccordingTo(controller).Target());
            Assert.IsFalse(card.Is().NonHero().AccordingTo(controller).NonTarget());

            Assert.IsTrue(card.Is().NonVillain().AccordingTo(controller));
            Assert.IsTrue(card.Is().NonVillain().AccordingTo(controller).Card());
            Assert.IsFalse(card.Is().NonVillain().Target().AccordingTo(controller));
            Assert.IsTrue(card.Is().NonVillain().NonTarget().AccordingTo(controller));
        }

        [Test()]
        public void TestModifiedDeckKindTarget()
        {
            SetupGameController("BaronBlade", "Legacy", "Jp.SOTMUtilities.TestMod.AlignmentTestVillain");

            var card = GetCard("VillainTargetInsistsItsHeroTarget");
            var controller = GetCardController(card);
            PlayCard(card); // can't be asked if its hero or not if its not in play

            Assert.IsFalse(card.Is().Environment());
            Assert.IsFalse(card.Is().Environment().Card());
            Assert.IsFalse(card.Is().Environment().Target());
            Assert.IsFalse(card.Is().Environment().NonTarget());

            Assert.IsTrue(card.Is().Hero().AccordingTo(controller));
            Assert.IsTrue(card.Is().Hero().AccordingTo(controller).Card());
            Assert.IsTrue(card.Is().Hero().AccordingTo(controller).Target());
            Assert.IsFalse(card.Is().Hero().AccordingTo(controller).NonTarget());

            Assert.IsFalse(card.Is().Villain().AccordingTo(controller));
            Assert.IsFalse(card.Is().Villain().AccordingTo(controller).Card());
            Assert.IsFalse(card.Is().Villain().Target().AccordingTo(controller));
            Assert.IsFalse(card.Is().Villain().NonTarget().AccordingTo(controller));

            Assert.IsTrue(card.Is().NonEnvironment());
            Assert.IsTrue(card.Is().NonEnvironment().Card());
            Assert.IsTrue(card.Is().NonEnvironment().Target());
            Assert.IsFalse(card.Is().NonEnvironment().NonTarget());

            Assert.IsFalse(card.Is().NonHero().AccordingTo(controller));
            Assert.IsFalse(card.Is().NonHero().AccordingTo(controller).Card());
            Assert.IsFalse(card.Is().NonHero().AccordingTo(controller).Target());
            Assert.IsFalse(card.Is().NonHero().AccordingTo(controller).NonTarget());

            Assert.IsTrue(card.Is().NonVillain().AccordingTo(controller));
            Assert.IsTrue(card.Is().NonVillain().AccordingTo(controller).Card());
            Assert.IsTrue(card.Is().NonVillain().Target().AccordingTo(controller));
            Assert.IsFalse(card.Is().NonVillain().NonTarget().AccordingTo(controller));
        }

        [Test()]
        public void TestModifiedDeckKindCard()
        {
            SetupGameController("BaronBlade", "Legacy", "Jp.SOTMUtilities.TestMod.AlignmentTestVillain");

            var card = GetCard("VillainTargetInsistsItsHeroCard");
            var controller = GetCardController(card);
            PlayCard(card); // can't be asked if its hero or not if its not in play

            Assert.IsFalse(card.Is().Environment());
            Assert.IsFalse(card.Is().Environment().Card());
            Assert.IsFalse(card.Is().Environment().Target());
            Assert.IsFalse(card.Is().Environment().NonTarget());

            Assert.IsTrue(card.Is().Hero().AccordingTo(controller));
            Assert.IsTrue(card.Is().Hero().AccordingTo(controller).Card());
            Assert.IsFalse(card.Is().Hero().AccordingTo(controller).Target());
            Assert.IsFalse(card.Is().Hero().AccordingTo(controller).NonTarget());

            Assert.IsFalse(card.Is().Villain().AccordingTo(controller));
            Assert.IsFalse(card.Is().Villain().AccordingTo(controller).Card());
            Assert.IsTrue(card.Is().Villain().Target().AccordingTo(controller));
            Assert.IsFalse(card.Is().Villain().NonTarget().AccordingTo(controller));

            Assert.IsTrue(card.Is().NonEnvironment());
            Assert.IsTrue(card.Is().NonEnvironment().Card());
            Assert.IsTrue(card.Is().NonEnvironment().Target());
            Assert.IsFalse(card.Is().NonEnvironment().NonTarget());

            Assert.IsFalse(card.Is().NonHero().AccordingTo(controller));
            Assert.IsFalse(card.Is().NonHero().AccordingTo(controller).Card());
            Assert.IsTrue(card.Is().NonHero().AccordingTo(controller).Target());
            Assert.IsFalse(card.Is().NonHero().AccordingTo(controller).NonTarget());

            Assert.IsTrue(card.Is().NonVillain().AccordingTo(controller));
            Assert.IsTrue(card.Is().NonVillain().AccordingTo(controller).Card());
            Assert.IsFalse(card.Is().NonVillain().Target().AccordingTo(controller));
            Assert.IsFalse(card.Is().NonVillain().NonTarget().AccordingTo(controller));
        }
    }
}
