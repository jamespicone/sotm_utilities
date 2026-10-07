using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NUnit.Framework;
using NUnit.Framework.Legacy;

using Handelabra.Sentinels.Engine.Model;
using Handelabra.Sentinels.UnitTest;
using Handelabra;

namespace Jp.SOTMUtilities.UnitTest
{
    [TestFixture()]
    public class AlignmentTestHeroTests : BaseTest
    {
        [Test()]
        public void TestHeroTurnTaker()
        {
            SetupGameController("BaronBlade", "Legacy", "Jp.SOTMUtilities.TestMod.AlignmentTestHero");

            var tt = legacy.TurnTaker;
            var controller = GetCardController(legacy.CharacterCard);

            ClassicAssert.IsFalse(tt.Is().Environment());
            ClassicAssert.IsFalse(tt.Is().Environment().Card());
            ClassicAssert.IsFalse(tt.Is().Environment().Target());
            ClassicAssert.IsFalse(tt.Is().Environment().NonTarget());

            ClassicAssert.IsTrue(tt.Is().Hero().AccordingTo(controller));
            ClassicAssert.IsFalse(tt.Is().Hero().Card().AccordingTo(controller));
            ClassicAssert.IsFalse(tt.Is().Hero().Target().AccordingTo(controller));
            ClassicAssert.IsTrue(tt.Is().Hero().NonTarget().AccordingTo(controller));

            ClassicAssert.IsFalse(tt.Is().Villain().AccordingTo(controller));
            ClassicAssert.IsFalse(tt.Is().Villain().AccordingTo(controller).Card());
            ClassicAssert.IsFalse(tt.Is().Villain().Target().AccordingTo(controller));
            ClassicAssert.IsFalse(tt.Is().Villain().NonTarget().AccordingTo(controller));

            ClassicAssert.IsTrue(tt.Is().NonEnvironment());
            ClassicAssert.IsFalse(tt.Is().NonEnvironment().Card());
            ClassicAssert.IsFalse(tt.Is().NonEnvironment().Target());
            ClassicAssert.IsTrue(tt.Is().NonEnvironment().NonTarget());

            ClassicAssert.IsFalse(tt.Is().NonHero().AccordingTo(controller));
            ClassicAssert.IsFalse(tt.Is().NonHero().Card().AccordingTo(controller));
            ClassicAssert.IsFalse(tt.Is().NonHero().Target().AccordingTo(controller));
            ClassicAssert.IsFalse(tt.Is().NonHero().NonTarget().AccordingTo(controller));

            ClassicAssert.IsTrue(tt.Is().NonVillain().AccordingTo(controller));
            ClassicAssert.IsFalse(tt.Is().NonVillain().AccordingTo(controller).Card());
            ClassicAssert.IsFalse(tt.Is().NonVillain().Target().AccordingTo(controller));
            ClassicAssert.IsTrue(tt.Is().NonVillain().NonTarget().AccordingTo(controller));
        }

        [Test()]
        public void TestEnvCard()
        {
            SetupGameController("BaronBlade", "Legacy", "Jp.SOTMUtilities.TestMod.AlignmentTestHero");

            var card = GetCard("HeroOwnedEnvCard");
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
            SetupGameController("BaronBlade", "Legacy", "Jp.SOTMUtilities.TestMod.AlignmentTestHero");

            var card = GetCard("HeroOwnedHeroCard");
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
            SetupGameController("BaronBlade", "Legacy", "Jp.SOTMUtilities.TestMod.AlignmentTestHero");

            var card = GetCard("HeroOwnedVillainCard");
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
            SetupGameController("BaronBlade", "Legacy", "Jp.SOTMUtilities.TestMod.AlignmentTestHero");

            var card = GetCard("HeroOwnedEnvTarget");
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
            SetupGameController("BaronBlade", "Legacy", "Jp.SOTMUtilities.TestMod.AlignmentTestHero");

            var card = GetCard("HeroOwnedHeroTarget");
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
            SetupGameController("BaronBlade", "Legacy", "Jp.SOTMUtilities.TestMod.AlignmentTestHero");

            var card = GetCard("HeroOwnedVillainTarget");
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
        public void TestEnvTargetHeroCard()
        {
            SetupGameController("BaronBlade", "Legacy", "Jp.SOTMUtilities.TestMod.AlignmentTestHero");

            var card = GetCard("HeroOwnedEnvTargetHeroCard");
            var controller = GetCardController(card);

            ClassicAssert.IsFalse(card.Is().Environment());
            ClassicAssert.IsFalse(card.Is().Environment().Card());
            ClassicAssert.IsTrue(card.Is().Environment().Target());
            ClassicAssert.IsFalse(card.Is().Environment().NonTarget());

            ClassicAssert.IsTrue(card.Is().Hero().AccordingTo(controller));
            ClassicAssert.IsTrue(card.Is().Hero().AccordingTo(controller).Card());
            ClassicAssert.IsFalse(card.Is().Hero().AccordingTo(controller).Target());
            ClassicAssert.IsFalse(card.Is().Hero().AccordingTo(controller).NonTarget());

            ClassicAssert.IsFalse(card.Is().Villain().AccordingTo(controller));
            ClassicAssert.IsFalse(card.Is().Villain().AccordingTo(controller).Card());
            ClassicAssert.IsFalse(card.Is().Villain().Target().AccordingTo(controller));
            ClassicAssert.IsFalse(card.Is().Villain().NonTarget().AccordingTo(controller));

            ClassicAssert.IsTrue(card.Is().NonEnvironment());
            ClassicAssert.IsTrue(card.Is().NonEnvironment().Card());
            ClassicAssert.IsFalse(card.Is().NonEnvironment().Target());
            ClassicAssert.IsFalse(card.Is().NonEnvironment().NonTarget());

            ClassicAssert.IsFalse(card.Is().NonHero().AccordingTo(controller));
            ClassicAssert.IsFalse(card.Is().NonHero().AccordingTo(controller).Card());
            ClassicAssert.IsTrue(card.Is().NonHero().AccordingTo(controller).Target());
            ClassicAssert.IsFalse(card.Is().NonHero().AccordingTo(controller).NonTarget());

            ClassicAssert.IsTrue(card.Is().NonVillain().AccordingTo(controller));
            ClassicAssert.IsTrue(card.Is().NonVillain().AccordingTo(controller).Card());
            ClassicAssert.IsTrue(card.Is().NonVillain().Target().AccordingTo(controller));
            ClassicAssert.IsFalse(card.Is().NonVillain().NonTarget().AccordingTo(controller));
        }

        [Test()]
        public void TestHeroTargetHeroCard()
        {
            SetupGameController("BaronBlade", "Legacy", "Jp.SOTMUtilities.TestMod.AlignmentTestHero");

            var card = GetCard("HeroOwnedHeroTargetHeroCard");
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
        public void TestVillainTargetHeroCard()
        {
            SetupGameController("BaronBlade", "Legacy", "Jp.SOTMUtilities.TestMod.AlignmentTestHero");

            var card = GetCard("HeroOwnedVillainTargetHeroCard");
            var controller = GetCardController(card);

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

        [Test()]
        public void TestModifiedDeckKind()
        {
            SetupGameController("BaronBlade", "Legacy", "Jp.SOTMUtilities.TestMod.AlignmentTestHero");

            var card = GetCard("HeroCardInsistsItsVillain");
            var controller = GetCardController(card);
            PlayCard(card); // can't be asked if its hero or not if its not in play

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
        public void TestModifiedDeckKindTarget()
        {
            SetupGameController("BaronBlade", "Legacy", "Jp.SOTMUtilities.TestMod.AlignmentTestHero");

            var card = GetCard("HeroTargetInsistsItsVillainTarget");
            var controller = GetCardController(card);
            PlayCard(card); // can't be asked if its hero or not if its not in play

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
        public void TestModifiedDeckKindCard()
        {
            SetupGameController("BaronBlade", "Legacy", "Jp.SOTMUtilities.TestMod.AlignmentTestHero");

            var card = GetCard("HeroTargetInsistsItsVillainCard");
            var controller = GetCardController(card);
            PlayCard(card); // can't be asked if its hero or not if its not in play

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
        public void TestOngoing()
        {
            SetupGameController("BaronBlade", "Legacy", "Jp.SOTMUtilities.TestMod.AlignmentTestHero");

            var card = GetCard("HeroOngoing");
            var controller = GetCardController(card);

            ClassicAssert.IsTrue(card.Is().Ongoing().AccordingTo(controller));
            ClassicAssert.IsTrue(card.Is().Hero().Ongoing().AccordingTo(controller));
            ClassicAssert.IsFalse(card.Is().Villain().Ongoing().AccordingTo(controller));

            ClassicAssert.IsFalse(card.Is().Equipment().AccordingTo(controller));
            ClassicAssert.IsFalse(card.Is().Hero().Equipment().AccordingTo(controller));
            ClassicAssert.IsFalse(card.Is().Villain().Equipment().AccordingTo(controller));
        }

        [Test()]
        public void TestEquipment()
        {
            SetupGameController("BaronBlade", "Legacy", "Jp.SOTMUtilities.TestMod.AlignmentTestHero");

            var card = GetCard("HeroEquipment");
            var controller = GetCardController(card);

            ClassicAssert.IsFalse(card.Is().Ongoing().AccordingTo(controller));
            ClassicAssert.IsFalse(card.Is().Hero().Ongoing().AccordingTo(controller));
            ClassicAssert.IsFalse(card.Is().Villain().Ongoing().AccordingTo(controller));

            ClassicAssert.IsTrue(card.Is().Equipment().AccordingTo(controller));
            ClassicAssert.IsTrue(card.Is().Hero().Equipment().AccordingTo(controller));
            ClassicAssert.IsFalse(card.Is().Villain().Equipment().AccordingTo(controller));
        }

        [Test()]
        public void TestKeywords()
        {
            SetupGameController("BaronBlade", "Legacy", "Jp.SOTMUtilities.TestMod.AlignmentTestHero");

            var card = GetCard("HeroOngoing");
            var controller = GetCardController(card);

            ClassicAssert.IsTrue(card.Is().WithKeyword("ongoing").AccordingTo(controller));
            ClassicAssert.IsFalse(card.Is().WithKeyword("equipment").AccordingTo(controller));
            ClassicAssert.IsFalse(card.Is().WithKeyword("ongoing").WithKeyword("equipment").AccordingTo(controller));

            ClassicAssert.IsFalse(card.Is().WithoutKeyword("ongoing").AccordingTo(controller));
            ClassicAssert.IsTrue(card.Is().WithoutKeyword("equipment").AccordingTo(controller));
            ClassicAssert.IsFalse(card.Is().WithoutKeyword("equipment").WithoutKeyword("ongoing").AccordingTo(controller));
            ClassicAssert.IsTrue(card.Is().Hero().Ongoing().WithoutKeyword("equipment").AccordingTo(controller));

            // TurnTakers don't have keywords, so they're excluded by both WithKeyword and WithoutKeyword.
            ClassicAssert.IsFalse(legacy.TurnTaker.Is().WithKeyword("ongoing").AccordingTo(controller));
            ClassicAssert.IsFalse(legacy.TurnTaker.Is().WithoutKeyword("ongoing").AccordingTo(controller));
        }

        [Test()]
        public void TestModifiedDeckKindOnlyInPlay()
        {
            SetupGameController("BaronBlade", "Legacy", "Jp.SOTMUtilities.TestMod.AlignmentTestHero");

            var card = GetCard("HeroTargetInsistsItsVillainTarget");
            var controller = GetCardController(card);

            ClassicAssert.IsTrue(card.Is().Hero().Target().AccordingTo(controller));
            ClassicAssert.IsFalse(card.Is().Villain().Target().AccordingTo(controller));

            PlayCard(card);

            ClassicAssert.IsFalse(card.Is().Hero().Target().AccordingTo(controller));
            ClassicAssert.IsTrue(card.Is().Villain().Target().AccordingTo(controller));

            DestroyCard(card);

            ClassicAssert.IsTrue(card.Is().Hero().Target().AccordingTo(controller));
            ClassicAssert.IsFalse(card.Is().Villain().Target().AccordingTo(controller));
        }

        [Test()]
        public void TestModifiedDeckKindBlank()
        {
            SetupGameController("BaronBlade", "Legacy", "Jp.SOTMUtilities.TestMod.AlignmentTestHero");

            var card = GetCard("HeroTargetInsistsItsVillainTarget");
            var controller = GetCardController(card);
            PlayCard(card);

            // A blank card has no text, so this is just a hero target again.
            card.SetIsBlank(true);

            ClassicAssert.IsTrue(card.Is().Hero().AccordingTo(controller));
            ClassicAssert.IsTrue(card.Is().Hero().Target().AccordingTo(controller));
            ClassicAssert.IsFalse(card.Is().Villain().AccordingTo(controller));
            ClassicAssert.IsFalse(card.Is().Villain().Target().AccordingTo(controller));
        }
    }
}
