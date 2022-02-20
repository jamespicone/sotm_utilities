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
    public class AlignmentTestHeroTests : BaseTest
    {
        [Test()]
        public void TestEnvCard()
        {
            SetupGameController("BaronBlade", "Legacy", "Jp.SOTMUtilities.TestMod.AlignmentTestHero");

            var card = GetCard("HeroOwnedEnvCard");
            var controller = GetCardController(card);
            
            Assert.IsTrue(card.Is().Environment());
            Assert.IsFalse(card.Is().Environment().Target());
            Assert.IsTrue(card.Is().Environment().NonTarget());

            Assert.IsFalse(card.Is().Hero());
            Assert.IsFalse(card.Is().Hero().Target());
            Assert.IsFalse(card.Is().Hero().NonTarget());

            Assert.IsFalse(card.Is().Villain().AccordingTo(controller));
            Assert.IsFalse(card.Is().Villain().Target().AccordingTo(controller));
            Assert.IsFalse(card.Is().Villain().NonTarget().AccordingTo(controller));

            Assert.IsFalse(card.Is().NonEnvironment());
            Assert.IsFalse(card.Is().NonEnvironment().Target());
            Assert.IsFalse(card.Is().NonEnvironment().NonTarget());

            Assert.IsTrue(card.Is().NonHero());
            Assert.IsFalse(card.Is().NonHero().Target());
            Assert.IsTrue(card.Is().NonHero().NonTarget());

            Assert.IsTrue(card.Is().NonVillain().AccordingTo(controller));
            Assert.IsFalse(card.Is().NonVillain().Target().AccordingTo(controller));
            Assert.IsTrue(card.Is().NonVillain().NonTarget().AccordingTo(controller));
        }

        [Test()]
        public void TestHeroCard()
        {
            SetupGameController("BaronBlade", "Legacy", "Jp.SOTMUtilities.TestMod.AlignmentTestHero");

            var card = GetCard("HeroOwnedHeroCard");
            var controller = GetCardController(card);

            Assert.IsFalse(card.Is().Environment());
            Assert.IsFalse(card.Is().Environment().Target());
            Assert.IsFalse(card.Is().Environment().NonTarget());

            Assert.IsTrue(card.Is().Hero());
            Assert.IsFalse(card.Is().Hero().Target());
            Assert.IsTrue(card.Is().Hero().NonTarget());

            Assert.IsFalse(card.Is().Villain().AccordingTo(controller));
            Assert.IsFalse(card.Is().Villain().Target().AccordingTo(controller));
            Assert.IsFalse(card.Is().Villain().NonTarget().AccordingTo(controller));

            Assert.IsTrue(card.Is().NonEnvironment());
            Assert.IsFalse(card.Is().NonEnvironment().Target());
            Assert.IsTrue(card.Is().NonEnvironment().NonTarget());

            Assert.IsFalse(card.Is().NonHero());
            Assert.IsFalse(card.Is().NonHero().Target());
            Assert.IsFalse(card.Is().NonHero().NonTarget());

            Assert.IsTrue(card.Is().NonVillain().AccordingTo(controller));
            Assert.IsFalse(card.Is().NonVillain().Target().AccordingTo(controller));
            Assert.IsTrue(card.Is().NonVillain().NonTarget().AccordingTo(controller));
        }

        [Test()]
        public void TestVillainCard()
        {
            SetupGameController("BaronBlade", "Legacy", "Jp.SOTMUtilities.TestMod.AlignmentTestHero");

            var card = GetCard("HeroOwnedVillainCard");
            var controller = GetCardController(card);

            Assert.IsFalse(card.Is().Environment());
            Assert.IsFalse(card.Is().Environment().Target());
            Assert.IsFalse(card.Is().Environment().NonTarget());

            Assert.IsFalse(card.Is().Hero());
            Assert.IsFalse(card.Is().Hero().Target());
            Assert.IsFalse(card.Is().Hero().NonTarget());

            Assert.IsTrue(card.Is().Villain().AccordingTo(controller));
            Assert.IsFalse(card.Is().Villain().Target().AccordingTo(controller));
            Assert.IsTrue(card.Is().Villain().NonTarget().AccordingTo(controller));

            Assert.IsTrue(card.Is().NonEnvironment());
            Assert.IsFalse(card.Is().NonEnvironment().Target());
            Assert.IsTrue(card.Is().NonEnvironment().NonTarget());

            Assert.IsTrue(card.Is().NonHero());
            Assert.IsFalse(card.Is().NonHero().Target());
            Assert.IsTrue(card.Is().NonHero().NonTarget());

            Assert.IsFalse(card.Is().NonVillain().AccordingTo(controller));
            Assert.IsFalse(card.Is().NonVillain().Target().AccordingTo(controller));
            Assert.IsFalse(card.Is().NonVillain().NonTarget().AccordingTo(controller));
        }

        [Test()]
        public void TestEnvTarget()
        {
            SetupGameController("BaronBlade", "Legacy", "Jp.SOTMUtilities.TestMod.AlignmentTestHero");

            var card = GetCard("HeroOwnedEnvTarget");
            var controller = GetCardController(card);

            Assert.IsTrue(card.Is().Environment());
            Assert.IsTrue(card.Is().Environment().Target());
            Assert.IsFalse(card.Is().Environment().NonTarget());

            Assert.IsFalse(card.Is().Hero());
            Assert.IsFalse(card.Is().Hero().Target());
            Assert.IsFalse(card.Is().Hero().NonTarget());

            Assert.IsFalse(card.Is().Villain().AccordingTo(controller));
            Assert.IsFalse(card.Is().Villain().Target().AccordingTo(controller));
            Assert.IsFalse(card.Is().Villain().NonTarget().AccordingTo(controller));

            Assert.IsFalse(card.Is().NonEnvironment());
            Assert.IsFalse(card.Is().NonEnvironment().Target());
            Assert.IsFalse(card.Is().NonEnvironment().NonTarget());

            Assert.IsTrue(card.Is().NonHero());
            Assert.IsTrue(card.Is().NonHero().Target());
            Assert.IsFalse(card.Is().NonHero().NonTarget());

            Assert.IsTrue(card.Is().NonVillain().AccordingTo(controller));
            Assert.IsTrue(card.Is().NonVillain().Target().AccordingTo(controller));
            Assert.IsFalse(card.Is().NonVillain().NonTarget().AccordingTo(controller));
        }

        [Test()]
        public void TestHeroTarget()
        {
            SetupGameController("BaronBlade", "Legacy", "Jp.SOTMUtilities.TestMod.AlignmentTestHero");

            var card = GetCard("HeroOwnedHeroTarget");
            var controller = GetCardController(card);

            Assert.IsFalse(card.Is().Environment());
            Assert.IsFalse(card.Is().Environment().Target());
            Assert.IsFalse(card.Is().Environment().NonTarget());

            Assert.IsTrue(card.Is().Hero());
            Assert.IsTrue(card.Is().Hero().Target());
            Assert.IsFalse(card.Is().Hero().NonTarget());

            Assert.IsFalse(card.Is().Villain().AccordingTo(controller));
            Assert.IsFalse(card.Is().Villain().Target().AccordingTo(controller));
            Assert.IsFalse(card.Is().Villain().NonTarget().AccordingTo(controller));

            Assert.IsTrue(card.Is().NonEnvironment());
            Assert.IsTrue(card.Is().NonEnvironment().Target());
            Assert.IsFalse(card.Is().NonEnvironment().NonTarget());

            Assert.IsFalse(card.Is().NonHero());
            Assert.IsFalse(card.Is().NonHero().Target());
            Assert.IsFalse(card.Is().NonHero().NonTarget());

            Assert.IsTrue(card.Is().NonVillain().AccordingTo(controller));
            Assert.IsTrue(card.Is().NonVillain().Target().AccordingTo(controller));
            Assert.IsFalse(card.Is().NonVillain().NonTarget().AccordingTo(controller));
        }

        [Test()]
        public void TestVillainTarget()
        {
            SetupGameController("BaronBlade", "Legacy", "Jp.SOTMUtilities.TestMod.AlignmentTestHero");

            var card = GetCard("HeroOwnedVillainTarget");
            var controller = GetCardController(card);

            Assert.IsFalse(card.Is().Environment());
            Assert.IsFalse(card.Is().Environment().Target());
            Assert.IsFalse(card.Is().Environment().NonTarget());

            Assert.IsFalse(card.Is().Hero());
            Assert.IsFalse(card.Is().Hero().Target());
            Assert.IsFalse(card.Is().Hero().NonTarget());

            Assert.IsTrue(card.Is().Villain().AccordingTo(controller));
            Assert.IsTrue(card.Is().Villain().Target().AccordingTo(controller));
            Assert.IsFalse(card.Is().Villain().NonTarget().AccordingTo(controller));

            Assert.IsTrue(card.Is().NonEnvironment());
            Assert.IsTrue(card.Is().NonEnvironment().Target());
            Assert.IsFalse(card.Is().NonEnvironment().NonTarget());

            Assert.IsTrue(card.Is().NonHero());
            Assert.IsTrue(card.Is().NonHero().Target());
            Assert.IsFalse(card.Is().NonHero().NonTarget());

            Assert.IsFalse(card.Is().NonVillain().AccordingTo(controller));
            Assert.IsFalse(card.Is().NonVillain().Target().AccordingTo(controller));
            Assert.IsFalse(card.Is().NonVillain().NonTarget().AccordingTo(controller));
        }

        [Test()]
        public void TestEnvTargetHeroCard()
        {
            SetupGameController("BaronBlade", "Legacy", "Jp.SOTMUtilities.TestMod.AlignmentTestHero");

            var card = GetCard("HeroOwnedEnvTargetHeroCard");
            var controller = GetCardController(card);

            Assert.IsFalse(card.Is().Environment());
            Assert.IsTrue(card.Is().Environment().Target());
            Assert.IsFalse(card.Is().Environment().NonTarget());

            Assert.IsTrue(card.Is().Hero());
            Assert.IsFalse(card.Is().Hero().Target());
            Assert.IsFalse(card.Is().Hero().NonTarget());

            Assert.IsFalse(card.Is().Villain().AccordingTo(controller));
            Assert.IsFalse(card.Is().Villain().Target().AccordingTo(controller));
            Assert.IsFalse(card.Is().Villain().NonTarget().AccordingTo(controller));

            Assert.IsTrue(card.Is().NonEnvironment());
            Assert.IsFalse(card.Is().NonEnvironment().Target());
            Assert.IsFalse(card.Is().NonEnvironment().NonTarget());

            Assert.IsFalse(card.Is().NonHero());
            Assert.IsTrue(card.Is().NonHero().Target());
            Assert.IsFalse(card.Is().NonHero().NonTarget());

            Assert.IsTrue(card.Is().NonVillain().AccordingTo(controller));
            Assert.IsTrue(card.Is().NonVillain().Target().AccordingTo(controller));
            Assert.IsFalse(card.Is().NonVillain().NonTarget().AccordingTo(controller));
        }

        [Test()]
        public void TestHeroTargetHeroCard()
        {
            SetupGameController("BaronBlade", "Legacy", "Jp.SOTMUtilities.TestMod.AlignmentTestHero");

            var card = GetCard("HeroOwnedHeroTargetHeroCard");
            var controller = GetCardController(card);

            Assert.IsFalse(card.Is().Environment());
            Assert.IsFalse(card.Is().Environment().Target());
            Assert.IsFalse(card.Is().Environment().NonTarget());

            Assert.IsTrue(card.Is().Hero());
            Assert.IsTrue(card.Is().Hero().Target());
            Assert.IsFalse(card.Is().Hero().NonTarget());

            Assert.IsFalse(card.Is().Villain().AccordingTo(controller));
            Assert.IsFalse(card.Is().Villain().Target().AccordingTo(controller));
            Assert.IsFalse(card.Is().Villain().NonTarget().AccordingTo(controller));

            Assert.IsTrue(card.Is().NonEnvironment());
            Assert.IsTrue(card.Is().NonEnvironment().Target());
            Assert.IsFalse(card.Is().NonEnvironment().NonTarget());

            Assert.IsFalse(card.Is().NonHero());
            Assert.IsFalse(card.Is().NonHero().Target());
            Assert.IsFalse(card.Is().NonHero().NonTarget());

            Assert.IsTrue(card.Is().NonVillain().AccordingTo(controller));
            Assert.IsTrue(card.Is().NonVillain().Target().AccordingTo(controller));
            Assert.IsFalse(card.Is().NonVillain().NonTarget().AccordingTo(controller));
        }

        [Test()]
        public void TestVillainTargetHeroCard()
        {
            SetupGameController("BaronBlade", "Legacy", "Jp.SOTMUtilities.TestMod.AlignmentTestHero");

            var card = GetCard("HeroOwnedVillainTargetHeroCard");
            var controller = GetCardController(card);

            Assert.IsFalse(card.Is().Environment());
            Assert.IsFalse(card.Is().Environment().Target());
            Assert.IsFalse(card.Is().Environment().NonTarget());

            Assert.IsTrue(card.Is().Hero());
            Assert.IsFalse(card.Is().Hero().Target());
            Assert.IsFalse(card.Is().Hero().NonTarget());

            Assert.IsFalse(card.Is().Villain().AccordingTo(controller));
            Assert.IsTrue(card.Is().Villain().Target().AccordingTo(controller));
            Assert.IsFalse(card.Is().Villain().NonTarget().AccordingTo(controller));

            Assert.IsTrue(card.Is().NonEnvironment());
            Assert.IsTrue(card.Is().NonEnvironment().Target());
            Assert.IsFalse(card.Is().NonEnvironment().NonTarget());

            Assert.IsFalse(card.Is().NonHero());
            Assert.IsTrue(card.Is().NonHero().Target());
            Assert.IsFalse(card.Is().NonHero().NonTarget());

            Assert.IsTrue(card.Is().NonVillain().AccordingTo(controller));
            Assert.IsFalse(card.Is().NonVillain().Target().AccordingTo(controller));
            Assert.IsFalse(card.Is().NonVillain().NonTarget().AccordingTo(controller));
        }
    }
}
