using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NUnit.Framework;
using NUnit.Framework.Legacy;

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

            ClassicAssert.IsFalse(tt.Is().Environment());
            ClassicAssert.IsFalse(tt.Is().Environment().Card());
            ClassicAssert.IsFalse(tt.Is().Environment().Target());
            ClassicAssert.IsFalse(tt.Is().Environment().NonTarget());

            ClassicAssert.IsFalse(tt.Is().Hero().AccordingTo(controller));
            ClassicAssert.IsFalse(tt.Is().Hero().Card().AccordingTo(controller));
            ClassicAssert.IsFalse(tt.Is().Hero().Target().AccordingTo(controller));
            ClassicAssert.IsFalse(tt.Is().Hero().NonTarget().AccordingTo(controller));

            ClassicAssert.IsTrue(tt.Is().Villain().AccordingTo(controller));
            ClassicAssert.IsFalse(tt.Is().Villain().AccordingTo(controller).Card());
            ClassicAssert.IsFalse(tt.Is().Villain().Target().AccordingTo(controller));
            ClassicAssert.IsTrue(tt.Is().Villain().NonTarget().AccordingTo(controller));

            ClassicAssert.IsTrue(tt.Is().NonEnvironment());
            ClassicAssert.IsFalse(tt.Is().NonEnvironment().Card());
            ClassicAssert.IsFalse(tt.Is().NonEnvironment().Target());
            ClassicAssert.IsTrue(tt.Is().NonEnvironment().NonTarget());

            ClassicAssert.IsTrue(tt.Is().NonHero().AccordingTo(controller));
            ClassicAssert.IsFalse(tt.Is().NonHero().Card().AccordingTo(controller));
            ClassicAssert.IsFalse(tt.Is().NonHero().Target().AccordingTo(controller));
            ClassicAssert.IsTrue(tt.Is().NonHero().NonTarget().AccordingTo(controller));

            ClassicAssert.IsFalse(tt.Is().NonVillain().AccordingTo(controller));
            ClassicAssert.IsFalse(tt.Is().NonVillain().AccordingTo(controller).Card());
            ClassicAssert.IsFalse(tt.Is().NonVillain().Target().AccordingTo(controller));
            ClassicAssert.IsFalse(tt.Is().NonVillain().NonTarget().AccordingTo(controller));
        }

        [Test()]
        public void TestEnvCard()
        {
            SetupGameController("BaronBlade", "Legacy", "Jp.SOTMUtilities.TestMod.AlignmentTestVillain");

            var card = GetCard("VillainOwnedEnvCard");
            var controller = GetCardController(card);
            
            ClassicAssert.IsTrue(card.Is().Environment());
            ClassicAssert.IsTrue(card.Is().Environment().Card());
            ClassicAssert.IsFalse(card.Is().Environment().Target());
            ClassicAssert.IsTrue(card.Is().Environment().NonTarget());

            ClassicAssert.IsFalse(card.Is().Hero().AccordingTo(controller));
            ClassicAssert.IsFalse(card.Is().Hero().AccordingTo(controller).Card());
            ClassicAssert.IsFalse(card.Is().Hero().AccordingTo(controller).Target());
            ClassicAssert.IsFalse(card.Is().Hero().AccordingTo(controller).NonTarget());

            ClassicAssert.IsFalse(card.Is().Villain().AccordingTo(controller));
            ClassicAssert.IsFalse(card.Is().Villain().AccordingTo(controller).Card());
            ClassicAssert.IsFalse(card.Is().Villain().Target().AccordingTo(controller));
            ClassicAssert.IsFalse(card.Is().Villain().NonTarget().AccordingTo(controller));

            ClassicAssert.IsFalse(card.Is().NonEnvironment());
            ClassicAssert.IsFalse(card.Is().NonEnvironment().Card());
            ClassicAssert.IsFalse(card.Is().NonEnvironment().Target());
            ClassicAssert.IsFalse(card.Is().NonEnvironment().NonTarget());

            ClassicAssert.IsTrue(card.Is().NonHero().AccordingTo(controller));
            ClassicAssert.IsTrue(card.Is().NonHero().AccordingTo(controller).Card());
            ClassicAssert.IsFalse(card.Is().NonHero().AccordingTo(controller).Target());
            ClassicAssert.IsTrue(card.Is().NonHero().AccordingTo(controller).NonTarget());

            ClassicAssert.IsTrue(card.Is().NonVillain().AccordingTo(controller));
            ClassicAssert.IsTrue(card.Is().NonVillain().AccordingTo(controller).Card());
            ClassicAssert.IsFalse(card.Is().NonVillain().Target().AccordingTo(controller));
            ClassicAssert.IsTrue(card.Is().NonVillain().NonTarget().AccordingTo(controller));
        }

        [Test()]
        public void TestHeroCard()
        {
            SetupGameController("BaronBlade", "Legacy", "Jp.SOTMUtilities.TestMod.AlignmentTestVillain");

            var card = GetCard("VillainOwnedHeroCard");
            var controller = GetCardController(card);

            ClassicAssert.IsFalse(card.Is().Environment());
            ClassicAssert.IsFalse(card.Is().Environment().Card());
            ClassicAssert.IsFalse(card.Is().Environment().Target());
            ClassicAssert.IsFalse(card.Is().Environment().NonTarget());

            ClassicAssert.IsTrue(card.Is().Hero().AccordingTo(controller));
            ClassicAssert.IsTrue(card.Is().Hero().AccordingTo(controller).Card());
            ClassicAssert.IsFalse(card.Is().Hero().AccordingTo(controller).Target());
            ClassicAssert.IsTrue(card.Is().Hero().AccordingTo(controller).NonTarget());

            ClassicAssert.IsFalse(card.Is().Villain().AccordingTo(controller));
            ClassicAssert.IsFalse(card.Is().Villain().AccordingTo(controller).Card());
            ClassicAssert.IsFalse(card.Is().Villain().Target().AccordingTo(controller));
            ClassicAssert.IsFalse(card.Is().Villain().NonTarget().AccordingTo(controller));

            ClassicAssert.IsTrue(card.Is().NonEnvironment());
            ClassicAssert.IsTrue(card.Is().NonEnvironment().Card());
            ClassicAssert.IsFalse(card.Is().NonEnvironment().Target());
            ClassicAssert.IsTrue(card.Is().NonEnvironment().NonTarget());

            ClassicAssert.IsFalse(card.Is().NonHero().AccordingTo(controller));
            ClassicAssert.IsFalse(card.Is().NonHero().AccordingTo(controller).Card());
            ClassicAssert.IsFalse(card.Is().NonHero().AccordingTo(controller).Target());
            ClassicAssert.IsFalse(card.Is().NonHero().AccordingTo(controller).NonTarget());

            ClassicAssert.IsTrue(card.Is().NonVillain().AccordingTo(controller));
            ClassicAssert.IsTrue(card.Is().NonVillain().AccordingTo(controller).Card());
            ClassicAssert.IsFalse(card.Is().NonVillain().Target().AccordingTo(controller));
            ClassicAssert.IsTrue(card.Is().NonVillain().NonTarget().AccordingTo(controller));
        }

        [Test()]
        public void TestVillainCard()
        {
            SetupGameController("BaronBlade", "Legacy", "Jp.SOTMUtilities.TestMod.AlignmentTestVillain");

            var card = GetCard("VillainOwnedVillainCard");
            var controller = GetCardController(card);

            ClassicAssert.IsFalse(card.Is().Environment());
            ClassicAssert.IsFalse(card.Is().Environment().Card());
            ClassicAssert.IsFalse(card.Is().Environment().Target());
            ClassicAssert.IsFalse(card.Is().Environment().NonTarget());

            ClassicAssert.IsFalse(card.Is().Hero().AccordingTo(controller));
            ClassicAssert.IsFalse(card.Is().Hero().AccordingTo(controller).Card());
            ClassicAssert.IsFalse(card.Is().Hero().AccordingTo(controller).Target());
            ClassicAssert.IsFalse(card.Is().Hero().AccordingTo(controller).NonTarget());

            ClassicAssert.IsTrue(card.Is().Villain().AccordingTo(controller));
            ClassicAssert.IsTrue(card.Is().Villain().AccordingTo(controller).Card());
            ClassicAssert.IsFalse(card.Is().Villain().Target().AccordingTo(controller));
            ClassicAssert.IsTrue(card.Is().Villain().NonTarget().AccordingTo(controller));

            ClassicAssert.IsTrue(card.Is().NonEnvironment());
            ClassicAssert.IsTrue(card.Is().NonEnvironment().Card());
            ClassicAssert.IsFalse(card.Is().NonEnvironment().Target());
            ClassicAssert.IsTrue(card.Is().NonEnvironment().NonTarget());

            ClassicAssert.IsTrue(card.Is().NonHero().AccordingTo(controller));
            ClassicAssert.IsTrue(card.Is().NonHero().AccordingTo(controller).Card());
            ClassicAssert.IsFalse(card.Is().NonHero().AccordingTo(controller).Target());
            ClassicAssert.IsTrue(card.Is().NonHero().AccordingTo(controller).NonTarget());

            ClassicAssert.IsFalse(card.Is().NonVillain().AccordingTo(controller));
            ClassicAssert.IsFalse(card.Is().NonVillain().AccordingTo(controller).Card());
            ClassicAssert.IsFalse(card.Is().NonVillain().Target().AccordingTo(controller));
            ClassicAssert.IsFalse(card.Is().NonVillain().NonTarget().AccordingTo(controller));
        }

        [Test()]
        public void TestEnvTarget()
        {
            SetupGameController("BaronBlade", "Legacy", "Jp.SOTMUtilities.TestMod.AlignmentTestVillain");

            var card = GetCard("VillainOwnedEnvTarget");
            var controller = GetCardController(card);

            ClassicAssert.IsTrue(card.Is().Environment());
            ClassicAssert.IsTrue(card.Is().Environment().Card());
            ClassicAssert.IsTrue(card.Is().Environment().Target());
            ClassicAssert.IsFalse(card.Is().Environment().NonTarget());

            ClassicAssert.IsFalse(card.Is().Hero().AccordingTo(controller));
            ClassicAssert.IsFalse(card.Is().Hero().AccordingTo(controller).Card());
            ClassicAssert.IsFalse(card.Is().Hero().AccordingTo(controller).Target());
            ClassicAssert.IsFalse(card.Is().Hero().AccordingTo(controller).NonTarget());

            ClassicAssert.IsFalse(card.Is().Villain().AccordingTo(controller));
            ClassicAssert.IsFalse(card.Is().Villain().AccordingTo(controller).Card());
            ClassicAssert.IsFalse(card.Is().Villain().Target().AccordingTo(controller));
            ClassicAssert.IsFalse(card.Is().Villain().NonTarget().AccordingTo(controller));

            ClassicAssert.IsFalse(card.Is().NonEnvironment());
            ClassicAssert.IsFalse(card.Is().NonEnvironment().Card());
            ClassicAssert.IsFalse(card.Is().NonEnvironment().Target());
            ClassicAssert.IsFalse(card.Is().NonEnvironment().NonTarget());

            ClassicAssert.IsTrue(card.Is().NonHero().AccordingTo(controller));
            ClassicAssert.IsTrue(card.Is().NonHero().AccordingTo(controller).Card());
            ClassicAssert.IsTrue(card.Is().NonHero().AccordingTo(controller).Target());
            ClassicAssert.IsFalse(card.Is().NonHero().AccordingTo(controller).NonTarget());

            ClassicAssert.IsTrue(card.Is().NonVillain().AccordingTo(controller));
            ClassicAssert.IsTrue(card.Is().NonVillain().AccordingTo(controller).Card());
            ClassicAssert.IsTrue(card.Is().NonVillain().Target().AccordingTo(controller));
            ClassicAssert.IsFalse(card.Is().NonVillain().NonTarget().AccordingTo(controller));
        }

        [Test()]
        public void TestHeroTarget()
        {
            SetupGameController("BaronBlade", "Legacy", "Jp.SOTMUtilities.TestMod.AlignmentTestVillain");

            var card = GetCard("VillainOwnedHeroTarget");
            var controller = GetCardController(card);

            ClassicAssert.IsFalse(card.Is().Environment());
            ClassicAssert.IsFalse(card.Is().Environment().Card());
            ClassicAssert.IsFalse(card.Is().Environment().Target());
            ClassicAssert.IsFalse(card.Is().Environment().NonTarget());

            ClassicAssert.IsTrue(card.Is().Hero().AccordingTo(controller));
            ClassicAssert.IsTrue(card.Is().Hero().AccordingTo(controller).Card());
            ClassicAssert.IsTrue(card.Is().Hero().AccordingTo(controller).Target());
            ClassicAssert.IsFalse(card.Is().Hero().AccordingTo(controller).NonTarget());

            ClassicAssert.IsFalse(card.Is().Villain().AccordingTo(controller));
            ClassicAssert.IsFalse(card.Is().Villain().AccordingTo(controller).Card());
            ClassicAssert.IsFalse(card.Is().Villain().Target().AccordingTo(controller));
            ClassicAssert.IsFalse(card.Is().Villain().NonTarget().AccordingTo(controller));

            ClassicAssert.IsTrue(card.Is().NonEnvironment());
            ClassicAssert.IsTrue(card.Is().NonEnvironment().Card());
            ClassicAssert.IsTrue(card.Is().NonEnvironment().Target());
            ClassicAssert.IsFalse(card.Is().NonEnvironment().NonTarget());

            ClassicAssert.IsFalse(card.Is().NonHero().AccordingTo(controller));
            ClassicAssert.IsFalse(card.Is().NonHero().AccordingTo(controller).Card());
            ClassicAssert.IsFalse(card.Is().NonHero().AccordingTo(controller).Target());
            ClassicAssert.IsFalse(card.Is().NonHero().AccordingTo(controller).NonTarget());

            ClassicAssert.IsTrue(card.Is().NonVillain().AccordingTo(controller));
            ClassicAssert.IsTrue(card.Is().NonVillain().AccordingTo(controller).Card());
            ClassicAssert.IsTrue(card.Is().NonVillain().Target().AccordingTo(controller));
            ClassicAssert.IsFalse(card.Is().NonVillain().NonTarget().AccordingTo(controller));
        }

        [Test()]
        public void TestVillainTarget()
        {
            SetupGameController("BaronBlade", "Legacy", "Jp.SOTMUtilities.TestMod.AlignmentTestVillain");

            var card = GetCard("VillainOwnedVillainTarget");
            var controller = GetCardController(card);

            ClassicAssert.IsFalse(card.Is().Environment());
            ClassicAssert.IsFalse(card.Is().Environment().Card());
            ClassicAssert.IsFalse(card.Is().Environment().Target());
            ClassicAssert.IsFalse(card.Is().Environment().NonTarget());

            ClassicAssert.IsFalse(card.Is().Hero().AccordingTo(controller));
            ClassicAssert.IsFalse(card.Is().Hero().AccordingTo(controller).Card());
            ClassicAssert.IsFalse(card.Is().Hero().AccordingTo(controller).Target());
            ClassicAssert.IsFalse(card.Is().Hero().AccordingTo(controller).NonTarget());

            ClassicAssert.IsTrue(card.Is().Villain().AccordingTo(controller));
            ClassicAssert.IsTrue(card.Is().Villain().AccordingTo(controller).Card());
            ClassicAssert.IsTrue(card.Is().Villain().Target().AccordingTo(controller));
            ClassicAssert.IsFalse(card.Is().Villain().NonTarget().AccordingTo(controller));

            ClassicAssert.IsTrue(card.Is().NonEnvironment());
            ClassicAssert.IsTrue(card.Is().NonEnvironment().Card());
            ClassicAssert.IsTrue(card.Is().NonEnvironment().Target());
            ClassicAssert.IsFalse(card.Is().NonEnvironment().NonTarget());

            ClassicAssert.IsTrue(card.Is().NonHero().AccordingTo(controller));
            ClassicAssert.IsTrue(card.Is().NonHero().AccordingTo(controller).Card());
            ClassicAssert.IsTrue(card.Is().NonHero().AccordingTo(controller).Target());
            ClassicAssert.IsFalse(card.Is().NonHero().AccordingTo(controller).NonTarget());

            ClassicAssert.IsFalse(card.Is().NonVillain().AccordingTo(controller));
            ClassicAssert.IsFalse(card.Is().NonVillain().AccordingTo(controller).Card());
            ClassicAssert.IsFalse(card.Is().NonVillain().Target().AccordingTo(controller));
            ClassicAssert.IsFalse(card.Is().NonVillain().NonTarget().AccordingTo(controller));
        }

        [Test()]
        public void TestEnvTargetVillainCard()
        {
            SetupGameController("BaronBlade", "Legacy", "Jp.SOTMUtilities.TestMod.AlignmentTestVillain");

            var card = GetCard("VillainOwnedEnvTargetVillainCard");
            var controller = GetCardController(card);

            ClassicAssert.IsFalse(card.Is().Environment());
            ClassicAssert.IsFalse(card.Is().Environment().Card());
            ClassicAssert.IsTrue(card.Is().Environment().Target());
            ClassicAssert.IsFalse(card.Is().Environment().NonTarget());

            ClassicAssert.IsFalse(card.Is().Hero().AccordingTo(controller));
            ClassicAssert.IsFalse(card.Is().Hero().AccordingTo(controller).Card());
            ClassicAssert.IsFalse(card.Is().Hero().AccordingTo(controller).Target());
            ClassicAssert.IsFalse(card.Is().Hero().AccordingTo(controller).NonTarget());

            ClassicAssert.IsTrue(card.Is().Villain().AccordingTo(controller));
            ClassicAssert.IsTrue(card.Is().Villain().AccordingTo(controller).Card());
            ClassicAssert.IsFalse(card.Is().Villain().Target().AccordingTo(controller));
            ClassicAssert.IsFalse(card.Is().Villain().NonTarget().AccordingTo(controller));

            ClassicAssert.IsTrue(card.Is().NonEnvironment());
            ClassicAssert.IsTrue(card.Is().NonEnvironment().Card());
            ClassicAssert.IsFalse(card.Is().NonEnvironment().Target());
            ClassicAssert.IsFalse(card.Is().NonEnvironment().NonTarget());

            ClassicAssert.IsTrue(card.Is().NonHero().AccordingTo(controller));
            ClassicAssert.IsTrue(card.Is().NonHero().AccordingTo(controller).Card());
            ClassicAssert.IsTrue(card.Is().NonHero().AccordingTo(controller).Target());
            ClassicAssert.IsFalse(card.Is().NonHero().AccordingTo(controller).NonTarget());

            ClassicAssert.IsFalse(card.Is().NonVillain().AccordingTo(controller));
            ClassicAssert.IsFalse(card.Is().NonVillain().AccordingTo(controller).Card());
            ClassicAssert.IsTrue(card.Is().NonVillain().Target().AccordingTo(controller));
            ClassicAssert.IsFalse(card.Is().NonVillain().NonTarget().AccordingTo(controller));
        }

        [Test()]
        public void TestHeroTargetVillainCard()
        {
            SetupGameController("BaronBlade", "Legacy", "Jp.SOTMUtilities.TestMod.AlignmentTestVillain");

            var card = GetCard("VillainOwnedHeroTargetVillainCard");
            var controller = GetCardController(card);

            ClassicAssert.IsFalse(card.Is().Environment());
            ClassicAssert.IsFalse(card.Is().Environment().Card());
            ClassicAssert.IsFalse(card.Is().Environment().Target());
            ClassicAssert.IsFalse(card.Is().Environment().NonTarget());

            ClassicAssert.IsFalse(card.Is().Hero().AccordingTo(controller));
            ClassicAssert.IsFalse(card.Is().Hero().AccordingTo(controller).Card());
            ClassicAssert.IsTrue(card.Is().Hero().AccordingTo(controller).Target());
            ClassicAssert.IsFalse(card.Is().Hero().AccordingTo(controller).NonTarget());

            ClassicAssert.IsTrue(card.Is().Villain().AccordingTo(controller));
            ClassicAssert.IsTrue(card.Is().Villain().AccordingTo(controller).Card());
            ClassicAssert.IsFalse(card.Is().Villain().Target().AccordingTo(controller));
            ClassicAssert.IsFalse(card.Is().Villain().NonTarget().AccordingTo(controller));

            ClassicAssert.IsTrue(card.Is().NonEnvironment());
            ClassicAssert.IsTrue(card.Is().NonEnvironment().Card());
            ClassicAssert.IsTrue(card.Is().NonEnvironment().Target());
            ClassicAssert.IsFalse(card.Is().NonEnvironment().NonTarget());

            ClassicAssert.IsTrue(card.Is().NonHero().AccordingTo(controller));
            ClassicAssert.IsTrue(card.Is().NonHero().AccordingTo(controller).Card());
            ClassicAssert.IsFalse(card.Is().NonHero().AccordingTo(controller).Target());
            ClassicAssert.IsFalse(card.Is().NonHero().AccordingTo(controller).NonTarget());

            ClassicAssert.IsFalse(card.Is().NonVillain().AccordingTo(controller));
            ClassicAssert.IsFalse(card.Is().NonVillain().AccordingTo(controller).Card());
            ClassicAssert.IsTrue(card.Is().NonVillain().Target().AccordingTo(controller));
            ClassicAssert.IsFalse(card.Is().NonVillain().NonTarget().AccordingTo(controller));
        }

        [Test()]
        public void TestVillainTargetVillainCard()
        {
            SetupGameController("BaronBlade", "Legacy", "Jp.SOTMUtilities.TestMod.AlignmentTestVillain");

            var card = GetCard("VillainOwnedVillainTargetVillainCard");
            var controller = GetCardController(card);

            ClassicAssert.IsFalse(card.Is().Environment());
            ClassicAssert.IsFalse(card.Is().Environment().Card());
            ClassicAssert.IsFalse(card.Is().Environment().Target());
            ClassicAssert.IsFalse(card.Is().Environment().NonTarget());

            ClassicAssert.IsFalse(card.Is().Hero().AccordingTo(controller));
            ClassicAssert.IsFalse(card.Is().Hero().AccordingTo(controller).Card());
            ClassicAssert.IsFalse(card.Is().Hero().AccordingTo(controller).Target());
            ClassicAssert.IsFalse(card.Is().Hero().AccordingTo(controller).NonTarget());

            ClassicAssert.IsTrue(card.Is().Villain().AccordingTo(controller));
            ClassicAssert.IsTrue(card.Is().Villain().AccordingTo(controller).Card());
            ClassicAssert.IsTrue(card.Is().Villain().Target().AccordingTo(controller));
            ClassicAssert.IsFalse(card.Is().Villain().NonTarget().AccordingTo(controller));

            ClassicAssert.IsTrue(card.Is().NonEnvironment());
            ClassicAssert.IsTrue(card.Is().NonEnvironment().Card());
            ClassicAssert.IsTrue(card.Is().NonEnvironment().Target());
            ClassicAssert.IsFalse(card.Is().NonEnvironment().NonTarget());

            ClassicAssert.IsTrue(card.Is().NonHero().AccordingTo(controller));
            ClassicAssert.IsTrue(card.Is().NonHero().AccordingTo(controller).Card());
            ClassicAssert.IsTrue(card.Is().NonHero().AccordingTo(controller).Target());
            ClassicAssert.IsFalse(card.Is().NonHero().AccordingTo(controller).NonTarget());

            ClassicAssert.IsFalse(card.Is().NonVillain().AccordingTo(controller));
            ClassicAssert.IsFalse(card.Is().NonVillain().AccordingTo(controller).Card());
            ClassicAssert.IsFalse(card.Is().NonVillain().Target().AccordingTo(controller));
            ClassicAssert.IsFalse(card.Is().NonVillain().NonTarget().AccordingTo(controller));
        }

        [Test()]
        public void TestModifiedDeckKind()
        {
            SetupGameController("BaronBlade", "Legacy", "Jp.SOTMUtilities.TestMod.AlignmentTestVillain");

            var card = GetCard("VillainCardInsistsItsHero");
            var controller = GetCardController(card);
            PlayCard(card); // can't be asked if its hero or not if its not in play

            ClassicAssert.IsFalse(card.Is().Environment());
            ClassicAssert.IsFalse(card.Is().Environment().Card());
            ClassicAssert.IsFalse(card.Is().Environment().Target());
            ClassicAssert.IsFalse(card.Is().Environment().NonTarget());

            ClassicAssert.IsTrue(card.Is().Hero().AccordingTo(controller));
            ClassicAssert.IsTrue(card.Is().Hero().AccordingTo(controller).Card());
            ClassicAssert.IsFalse(card.Is().Hero().AccordingTo(controller).Target());
            ClassicAssert.IsTrue(card.Is().Hero().AccordingTo(controller).NonTarget());

            ClassicAssert.IsFalse(card.Is().Villain().AccordingTo(controller));
            ClassicAssert.IsFalse(card.Is().Villain().AccordingTo(controller).Card());
            ClassicAssert.IsFalse(card.Is().Villain().Target().AccordingTo(controller));
            ClassicAssert.IsFalse(card.Is().Villain().NonTarget().AccordingTo(controller));

            ClassicAssert.IsTrue(card.Is().NonEnvironment());
            ClassicAssert.IsTrue(card.Is().NonEnvironment().Card());
            ClassicAssert.IsFalse(card.Is().NonEnvironment().Target());
            ClassicAssert.IsTrue(card.Is().NonEnvironment().NonTarget());

            ClassicAssert.IsFalse(card.Is().NonHero().AccordingTo(controller));
            ClassicAssert.IsFalse(card.Is().NonHero().AccordingTo(controller).Card());
            ClassicAssert.IsFalse(card.Is().NonHero().AccordingTo(controller).Target());
            ClassicAssert.IsFalse(card.Is().NonHero().AccordingTo(controller).NonTarget());

            ClassicAssert.IsTrue(card.Is().NonVillain().AccordingTo(controller));
            ClassicAssert.IsTrue(card.Is().NonVillain().AccordingTo(controller).Card());
            ClassicAssert.IsFalse(card.Is().NonVillain().Target().AccordingTo(controller));
            ClassicAssert.IsTrue(card.Is().NonVillain().NonTarget().AccordingTo(controller));
        }

        [Test()]
        public void TestModifiedDeckKindTarget()
        {
            SetupGameController("BaronBlade", "Legacy", "Jp.SOTMUtilities.TestMod.AlignmentTestVillain");

            var card = GetCard("VillainTargetInsistsItsHeroTarget");
            var controller = GetCardController(card);
            PlayCard(card); // can't be asked if its hero or not if its not in play

            ClassicAssert.IsFalse(card.Is().Environment());
            ClassicAssert.IsFalse(card.Is().Environment().Card());
            ClassicAssert.IsFalse(card.Is().Environment().Target());
            ClassicAssert.IsFalse(card.Is().Environment().NonTarget());

            ClassicAssert.IsTrue(card.Is().Hero().AccordingTo(controller));
            ClassicAssert.IsTrue(card.Is().Hero().AccordingTo(controller).Card());
            ClassicAssert.IsTrue(card.Is().Hero().AccordingTo(controller).Target());
            ClassicAssert.IsFalse(card.Is().Hero().AccordingTo(controller).NonTarget());

            ClassicAssert.IsFalse(card.Is().Villain().AccordingTo(controller));
            ClassicAssert.IsFalse(card.Is().Villain().AccordingTo(controller).Card());
            ClassicAssert.IsFalse(card.Is().Villain().Target().AccordingTo(controller));
            ClassicAssert.IsFalse(card.Is().Villain().NonTarget().AccordingTo(controller));

            ClassicAssert.IsTrue(card.Is().NonEnvironment());
            ClassicAssert.IsTrue(card.Is().NonEnvironment().Card());
            ClassicAssert.IsTrue(card.Is().NonEnvironment().Target());
            ClassicAssert.IsFalse(card.Is().NonEnvironment().NonTarget());

            ClassicAssert.IsFalse(card.Is().NonHero().AccordingTo(controller));
            ClassicAssert.IsFalse(card.Is().NonHero().AccordingTo(controller).Card());
            ClassicAssert.IsFalse(card.Is().NonHero().AccordingTo(controller).Target());
            ClassicAssert.IsFalse(card.Is().NonHero().AccordingTo(controller).NonTarget());

            ClassicAssert.IsTrue(card.Is().NonVillain().AccordingTo(controller));
            ClassicAssert.IsTrue(card.Is().NonVillain().AccordingTo(controller).Card());
            ClassicAssert.IsTrue(card.Is().NonVillain().Target().AccordingTo(controller));
            ClassicAssert.IsFalse(card.Is().NonVillain().NonTarget().AccordingTo(controller));
        }

        [Test()]
        public void TestModifiedDeckKindCard()
        {
            SetupGameController("BaronBlade", "Legacy", "Jp.SOTMUtilities.TestMod.AlignmentTestVillain");

            var card = GetCard("VillainTargetInsistsItsHeroCard");
            var controller = GetCardController(card);
            PlayCard(card); // can't be asked if its hero or not if its not in play

            ClassicAssert.IsFalse(card.Is().Environment());
            ClassicAssert.IsFalse(card.Is().Environment().Card());
            ClassicAssert.IsFalse(card.Is().Environment().Target());
            ClassicAssert.IsFalse(card.Is().Environment().NonTarget());

            ClassicAssert.IsTrue(card.Is().Hero().AccordingTo(controller));
            ClassicAssert.IsTrue(card.Is().Hero().AccordingTo(controller).Card());
            ClassicAssert.IsFalse(card.Is().Hero().AccordingTo(controller).Target());
            ClassicAssert.IsFalse(card.Is().Hero().AccordingTo(controller).NonTarget());

            ClassicAssert.IsFalse(card.Is().Villain().AccordingTo(controller));
            ClassicAssert.IsFalse(card.Is().Villain().AccordingTo(controller).Card());
            ClassicAssert.IsTrue(card.Is().Villain().Target().AccordingTo(controller));
            ClassicAssert.IsFalse(card.Is().Villain().NonTarget().AccordingTo(controller));

            ClassicAssert.IsTrue(card.Is().NonEnvironment());
            ClassicAssert.IsTrue(card.Is().NonEnvironment().Card());
            ClassicAssert.IsTrue(card.Is().NonEnvironment().Target());
            ClassicAssert.IsFalse(card.Is().NonEnvironment().NonTarget());

            ClassicAssert.IsFalse(card.Is().NonHero().AccordingTo(controller));
            ClassicAssert.IsFalse(card.Is().NonHero().AccordingTo(controller).Card());
            ClassicAssert.IsTrue(card.Is().NonHero().AccordingTo(controller).Target());
            ClassicAssert.IsFalse(card.Is().NonHero().AccordingTo(controller).NonTarget());

            ClassicAssert.IsTrue(card.Is().NonVillain().AccordingTo(controller));
            ClassicAssert.IsTrue(card.Is().NonVillain().AccordingTo(controller).Card());
            ClassicAssert.IsFalse(card.Is().NonVillain().Target().AccordingTo(controller));
            ClassicAssert.IsFalse(card.Is().NonVillain().NonTarget().AccordingTo(controller));
        }
    }
}
