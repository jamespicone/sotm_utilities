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
    public class AlignmentTestEnvironmentTests : BaseTest
    {
        [Test()]
        public void TestEnvTurnTaker()
        {
            SetupGameController("BaronBlade", "Legacy", "Jp.SOTMUtilities.TestMod.AlignmentTestEnvironment");

            var tt = FindEnvironment();
            var controller = GetCardController(legacy.CharacterCard);

            Assert.IsTrue(tt.Is().Environment());
            Assert.IsFalse(tt.Is().Environment().Card());
            Assert.IsFalse(tt.Is().Environment().Target());
            Assert.IsTrue(tt.Is().Environment().NonTarget());

            Assert.IsFalse(tt.Is().Hero());
            Assert.IsFalse(tt.Is().Hero().Card());
            Assert.IsFalse(tt.Is().Hero().Target());
            Assert.IsFalse(tt.Is().Hero().NonTarget());

            Assert.IsFalse(tt.Is().Villain().AccordingTo(controller));
            Assert.IsFalse(tt.Is().Villain().Card().AccordingTo(controller));
            Assert.IsFalse(tt.Is().Villain().Target().AccordingTo(controller));
            Assert.IsFalse(tt.Is().Villain().NonTarget().AccordingTo(controller));

            Assert.IsFalse(tt.Is().NonEnvironment());
            Assert.IsFalse(tt.Is().NonEnvironment().Card());
            Assert.IsFalse(tt.Is().NonEnvironment().Target());
            Assert.IsFalse(tt.Is().NonEnvironment().NonTarget());

            Assert.IsTrue(tt.Is().NonHero());
            Assert.IsFalse(tt.Is().NonHero().Card());
            Assert.IsFalse(tt.Is().NonHero().Target());
            Assert.IsTrue(tt.Is().NonHero().NonTarget());

            Assert.IsTrue(tt.Is().NonVillain().AccordingTo(controller));
            Assert.IsFalse(tt.Is().NonVillain().Card().AccordingTo(controller));
            Assert.IsFalse(tt.Is().NonVillain().Target().AccordingTo(controller));
            Assert.IsTrue(tt.Is().NonVillain().NonTarget().AccordingTo(controller));
        }

        [Test()]
        public void TestEnvCard()
        {
            SetupGameController("BaronBlade", "Legacy", "Jp.SOTMUtilities.TestMod.AlignmentTestEnvironment");

            var card = GetCard("EnvOwnedEnvCard");
            var controller = GetCardController(card);
            
            Assert.IsTrue(card.Is().Environment());
            Assert.IsTrue(card.Is().Environment().Card());
            Assert.IsFalse(card.Is().Environment().Target());
            Assert.IsTrue(card.Is().Environment().NonTarget());

            Assert.IsFalse(card.Is().Hero());
            Assert.IsFalse(card.Is().Hero().Card());
            Assert.IsFalse(card.Is().Hero().Target());
            Assert.IsFalse(card.Is().Hero().NonTarget());

            Assert.IsFalse(card.Is().Villain().AccordingTo(controller));
            Assert.IsFalse(card.Is().Villain().Card().AccordingTo(controller));
            Assert.IsFalse(card.Is().Villain().Target().AccordingTo(controller));
            Assert.IsFalse(card.Is().Villain().NonTarget().AccordingTo(controller));

            Assert.IsFalse(card.Is().NonEnvironment());
            Assert.IsFalse(card.Is().NonEnvironment().Card());
            Assert.IsFalse(card.Is().NonEnvironment().Target());
            Assert.IsFalse(card.Is().NonEnvironment().NonTarget());

            Assert.IsTrue(card.Is().NonHero());
            Assert.IsTrue(card.Is().NonHero().Card());
            Assert.IsFalse(card.Is().NonHero().Target());
            Assert.IsTrue(card.Is().NonHero().NonTarget());

            Assert.IsTrue(card.Is().NonVillain().AccordingTo(controller));
            Assert.IsTrue(card.Is().NonVillain().Card().AccordingTo(controller));
            Assert.IsFalse(card.Is().NonVillain().Target().AccordingTo(controller));
            Assert.IsTrue(card.Is().NonVillain().NonTarget().AccordingTo(controller));
        }

        [Test()]
        public void TestHeroCard()
        {
            SetupGameController("BaronBlade", "Legacy", "Jp.SOTMUtilities.TestMod.AlignmentTestEnvironment");

            var card = GetCard("EnvOwnedHeroCard");
            var controller = GetCardController(card);

            Assert.IsFalse(card.Is().Environment());
            Assert.IsFalse(card.Is().Environment().Card());
            Assert.IsFalse(card.Is().Environment().Target());
            Assert.IsFalse(card.Is().Environment().NonTarget());

            Assert.IsTrue(card.Is().Hero());
            Assert.IsTrue(card.Is().Hero().Card());
            Assert.IsFalse(card.Is().Hero().Target());
            Assert.IsTrue(card.Is().Hero().NonTarget());

            Assert.IsFalse(card.Is().Villain().AccordingTo(controller));
            Assert.IsFalse(card.Is().Villain().Card().AccordingTo(controller));
            Assert.IsFalse(card.Is().Villain().Target().AccordingTo(controller));
            Assert.IsFalse(card.Is().Villain().NonTarget().AccordingTo(controller));

            Assert.IsTrue(card.Is().NonEnvironment());
            Assert.IsTrue(card.Is().NonEnvironment().Card());
            Assert.IsFalse(card.Is().NonEnvironment().Target());
            Assert.IsTrue(card.Is().NonEnvironment().NonTarget());

            Assert.IsFalse(card.Is().NonHero());
            Assert.IsFalse(card.Is().NonHero().Card());
            Assert.IsFalse(card.Is().NonHero().Target());
            Assert.IsFalse(card.Is().NonHero().NonTarget());

            Assert.IsTrue(card.Is().NonVillain().AccordingTo(controller));
            Assert.IsTrue(card.Is().NonVillain().Card().AccordingTo(controller));
            Assert.IsFalse(card.Is().NonVillain().Target().AccordingTo(controller));
            Assert.IsTrue(card.Is().NonVillain().NonTarget().AccordingTo(controller));
        }

        [Test()]
        public void TestVillainCard()
        {
            SetupGameController("BaronBlade", "Legacy", "Jp.SOTMUtilities.TestMod.AlignmentTestEnvironment");

            var card = GetCard("EnvOwnedVillainCard");
            var controller = GetCardController(card);

            Assert.IsFalse(card.Is().Environment());
            Assert.IsFalse(card.Is().Environment().Card());
            Assert.IsFalse(card.Is().Environment().Target());
            Assert.IsFalse(card.Is().Environment().NonTarget());

            Assert.IsFalse(card.Is().Hero());
            Assert.IsFalse(card.Is().Hero().Card());
            Assert.IsFalse(card.Is().Hero().Target());
            Assert.IsFalse(card.Is().Hero().NonTarget());

            Assert.IsTrue(card.Is().Villain().AccordingTo(controller));
            Assert.IsTrue(card.Is().Villain().Card().AccordingTo(controller));
            Assert.IsFalse(card.Is().Villain().Target().AccordingTo(controller));
            Assert.IsTrue(card.Is().Villain().NonTarget().AccordingTo(controller));

            Assert.IsTrue(card.Is().NonEnvironment());
            Assert.IsTrue(card.Is().NonEnvironment().Card());
            Assert.IsFalse(card.Is().NonEnvironment().Target());
            Assert.IsTrue(card.Is().NonEnvironment().NonTarget());

            Assert.IsTrue(card.Is().NonHero());
            Assert.IsTrue(card.Is().NonHero().Card());
            Assert.IsFalse(card.Is().NonHero().Target());
            Assert.IsTrue(card.Is().NonHero().NonTarget());

            Assert.IsFalse(card.Is().NonVillain().AccordingTo(controller));
            Assert.IsFalse(card.Is().NonVillain().Card().AccordingTo(controller));
            Assert.IsFalse(card.Is().NonVillain().Target().AccordingTo(controller));
            Assert.IsFalse(card.Is().NonVillain().NonTarget().AccordingTo(controller));
        }

        [Test()]
        public void TestEnvTarget()
        {
            SetupGameController("BaronBlade", "Legacy", "Jp.SOTMUtilities.TestMod.AlignmentTestEnvironment");

            var card = GetCard("EnvOwnedEnvTarget");
            var controller = GetCardController(card);

            Assert.IsTrue(card.Is().Environment());
            Assert.IsTrue(card.Is().Environment().Card());
            Assert.IsTrue(card.Is().Environment().Target());
            Assert.IsFalse(card.Is().Environment().NonTarget());

            Assert.IsFalse(card.Is().Hero());
            Assert.IsFalse(card.Is().Hero().Card());
            Assert.IsFalse(card.Is().Hero().Target());
            Assert.IsFalse(card.Is().Hero().NonTarget());

            Assert.IsFalse(card.Is().Villain().AccordingTo(controller));
            Assert.IsFalse(card.Is().Villain().Card().AccordingTo(controller));
            Assert.IsFalse(card.Is().Villain().Target().AccordingTo(controller));
            Assert.IsFalse(card.Is().Villain().NonTarget().AccordingTo(controller));

            Assert.IsFalse(card.Is().NonEnvironment());
            Assert.IsFalse(card.Is().NonEnvironment().Card());
            Assert.IsFalse(card.Is().NonEnvironment().Target());
            Assert.IsFalse(card.Is().NonEnvironment().NonTarget());

            Assert.IsTrue(card.Is().NonHero());
            Assert.IsTrue(card.Is().NonHero().Card());
            Assert.IsTrue(card.Is().NonHero().Target());
            Assert.IsFalse(card.Is().NonHero().NonTarget());

            Assert.IsTrue(card.Is().NonVillain().AccordingTo(controller));
            Assert.IsTrue(card.Is().NonVillain().Card().AccordingTo(controller));
            Assert.IsTrue(card.Is().NonVillain().Target().AccordingTo(controller));
            Assert.IsFalse(card.Is().NonVillain().NonTarget().AccordingTo(controller));
        }

        [Test()]
        public void TestHeroTarget()
        {
            SetupGameController("BaronBlade", "Legacy", "Jp.SOTMUtilities.TestMod.AlignmentTestEnvironment");

            var card = GetCard("EnvOwnedHeroTarget");
            var controller = GetCardController(card);

            Assert.IsFalse(card.Is().Environment());
            Assert.IsFalse(card.Is().Environment().Card());
            Assert.IsFalse(card.Is().Environment().Target());
            Assert.IsFalse(card.Is().Environment().NonTarget());

            Assert.IsTrue(card.Is().Hero());
            Assert.IsTrue(card.Is().Hero().Card());
            Assert.IsTrue(card.Is().Hero().Target());
            Assert.IsFalse(card.Is().Hero().NonTarget());

            Assert.IsFalse(card.Is().Villain().AccordingTo(controller));
            Assert.IsFalse(card.Is().Villain().Card().AccordingTo(controller));
            Assert.IsFalse(card.Is().Villain().Target().AccordingTo(controller));
            Assert.IsFalse(card.Is().Villain().NonTarget().AccordingTo(controller));

            Assert.IsTrue(card.Is().NonEnvironment());
            Assert.IsTrue(card.Is().NonEnvironment().Card());
            Assert.IsTrue(card.Is().NonEnvironment().Target());
            Assert.IsFalse(card.Is().NonEnvironment().NonTarget());

            Assert.IsFalse(card.Is().NonHero());
            Assert.IsFalse(card.Is().NonHero().Card());
            Assert.IsFalse(card.Is().NonHero().Target());
            Assert.IsFalse(card.Is().NonHero().NonTarget());

            Assert.IsTrue(card.Is().NonVillain().AccordingTo(controller));
            Assert.IsTrue(card.Is().NonVillain().Card().AccordingTo(controller));
            Assert.IsTrue(card.Is().NonVillain().Target().AccordingTo(controller));
            Assert.IsFalse(card.Is().NonVillain().NonTarget().AccordingTo(controller));
        }

        [Test()]
        public void TestVillainTarget()
        {
            SetupGameController("BaronBlade", "Legacy", "Jp.SOTMUtilities.TestMod.AlignmentTestEnvironment");

            var card = GetCard("EnvOwnedVillainTarget");
            var controller = GetCardController(card);

            Assert.IsFalse(card.Is().Environment());
            Assert.IsFalse(card.Is().Environment().Card());
            Assert.IsFalse(card.Is().Environment().Target());
            Assert.IsFalse(card.Is().Environment().NonTarget());

            Assert.IsFalse(card.Is().Hero());
            Assert.IsFalse(card.Is().Hero().Card());
            Assert.IsFalse(card.Is().Hero().Target());
            Assert.IsFalse(card.Is().Hero().NonTarget());

            Assert.IsTrue(card.Is().Villain().AccordingTo(controller));
            Assert.IsTrue(card.Is().Villain().Card().AccordingTo(controller));
            Assert.IsTrue(card.Is().Villain().Target().AccordingTo(controller));
            Assert.IsFalse(card.Is().Villain().NonTarget().AccordingTo(controller));

            Assert.IsTrue(card.Is().NonEnvironment());
            Assert.IsTrue(card.Is().NonEnvironment().Card());
            Assert.IsTrue(card.Is().NonEnvironment().Target());
            Assert.IsFalse(card.Is().NonEnvironment().NonTarget());

            Assert.IsTrue(card.Is().NonHero());
            Assert.IsTrue(card.Is().NonHero().Card());
            Assert.IsTrue(card.Is().NonHero().Target());
            Assert.IsFalse(card.Is().NonHero().NonTarget());

            Assert.IsFalse(card.Is().NonVillain().AccordingTo(controller));
            Assert.IsFalse(card.Is().NonVillain().Card().AccordingTo(controller));
            Assert.IsFalse(card.Is().NonVillain().Target().AccordingTo(controller));
            Assert.IsFalse(card.Is().NonVillain().NonTarget().AccordingTo(controller));
        }

        [Test()]
        public void TestEnvTargetEnvCard()
        {
            SetupGameController("BaronBlade", "Legacy", "Jp.SOTMUtilities.TestMod.AlignmentTestEnvironment");

            var card = GetCard("EnvOwnedEnvTargetEnvCard");
            var controller = GetCardController(card);

            Assert.IsTrue(card.Is().Environment());
            Assert.IsTrue(card.Is().Environment().Card());
            Assert.IsTrue(card.Is().Environment().Target());
            Assert.IsFalse(card.Is().Environment().NonTarget());

            Assert.IsFalse(card.Is().Hero());
            Assert.IsFalse(card.Is().Hero().Card());
            Assert.IsFalse(card.Is().Hero().Target());
            Assert.IsFalse(card.Is().Hero().NonTarget());

            Assert.IsFalse(card.Is().Villain().AccordingTo(controller));
            Assert.IsFalse(card.Is().Villain().Card().AccordingTo(controller));
            Assert.IsFalse(card.Is().Villain().Target().AccordingTo(controller));
            Assert.IsFalse(card.Is().Villain().NonTarget().AccordingTo(controller));

            Assert.IsFalse(card.Is().NonEnvironment());
            Assert.IsFalse(card.Is().NonEnvironment().Card());
            Assert.IsFalse(card.Is().NonEnvironment().Target());
            Assert.IsFalse(card.Is().NonEnvironment().NonTarget());

            Assert.IsTrue(card.Is().NonHero());
            Assert.IsTrue(card.Is().NonHero().Card());
            Assert.IsTrue(card.Is().NonHero().Target());
            Assert.IsFalse(card.Is().NonHero().NonTarget());

            Assert.IsTrue(card.Is().NonVillain().AccordingTo(controller));
            Assert.IsTrue(card.Is().NonVillain().Card().AccordingTo(controller));
            Assert.IsTrue(card.Is().NonVillain().Target().AccordingTo(controller));
            Assert.IsFalse(card.Is().NonVillain().NonTarget().AccordingTo(controller));
        }

        [Test()]
        public void TestHeroTargetEnvCard()
        {
            SetupGameController("BaronBlade", "Legacy", "Jp.SOTMUtilities.TestMod.AlignmentTestEnvironment");

            var card = GetCard("EnvOwnedHeroTargetEnvCard");
            var controller = GetCardController(card);

            Assert.IsTrue(card.Is().Environment());
            Assert.IsTrue(card.Is().Environment().Card());
            Assert.IsFalse(card.Is().Environment().Target());
            Assert.IsFalse(card.Is().Environment().NonTarget());

            Assert.IsFalse(card.Is().Hero());
            Assert.IsFalse(card.Is().Hero().Card());
            Assert.IsTrue(card.Is().Hero().Target());
            Assert.IsFalse(card.Is().Hero().NonTarget());

            Assert.IsFalse(card.Is().Villain().AccordingTo(controller));
            Assert.IsFalse(card.Is().Villain().Card().AccordingTo(controller));
            Assert.IsFalse(card.Is().Villain().Target().AccordingTo(controller));
            Assert.IsFalse(card.Is().Villain().NonTarget().AccordingTo(controller));

            Assert.IsFalse(card.Is().NonEnvironment());
            Assert.IsFalse(card.Is().NonEnvironment().Card());
            Assert.IsTrue(card.Is().NonEnvironment().Target());
            Assert.IsFalse(card.Is().NonEnvironment().NonTarget());

            Assert.IsTrue(card.Is().NonHero());
            Assert.IsTrue(card.Is().NonHero().Card());
            Assert.IsFalse(card.Is().NonHero().Target());
            Assert.IsFalse(card.Is().NonHero().NonTarget());

            Assert.IsTrue(card.Is().NonVillain().AccordingTo(controller));
            Assert.IsTrue(card.Is().NonVillain().Card().AccordingTo(controller));
            Assert.IsTrue(card.Is().NonVillain().Target().AccordingTo(controller));
            Assert.IsFalse(card.Is().NonVillain().NonTarget().AccordingTo(controller));
        }

        [Test()]
        public void TestVillainTargetEnvCard()
        {
            SetupGameController("BaronBlade", "Legacy", "Jp.SOTMUtilities.TestMod.AlignmentTestEnvironment");

            var card = GetCard("EnvOwnedVillainTargetEnvCard");
            var controller = GetCardController(card);

            Assert.IsTrue(card.Is().Environment());
            Assert.IsTrue(card.Is().Environment().Card());
            Assert.IsFalse(card.Is().Environment().Target());
            Assert.IsFalse(card.Is().Environment().NonTarget());

            Assert.IsFalse(card.Is().Hero());
            Assert.IsFalse(card.Is().Hero().Card());
            Assert.IsFalse(card.Is().Hero().Target());
            Assert.IsFalse(card.Is().Hero().NonTarget());

            Assert.IsFalse(card.Is().Villain().AccordingTo(controller));
            Assert.IsFalse(card.Is().Villain().Card().AccordingTo(controller));
            Assert.IsTrue(card.Is().Villain().Target().AccordingTo(controller));
            Assert.IsFalse(card.Is().Villain().NonTarget().AccordingTo(controller));

            Assert.IsFalse(card.Is().NonEnvironment());
            Assert.IsFalse(card.Is().NonEnvironment().Card());
            Assert.IsTrue(card.Is().NonEnvironment().Target());
            Assert.IsFalse(card.Is().NonEnvironment().NonTarget());

            Assert.IsTrue(card.Is().NonHero());
            Assert.IsTrue(card.Is().NonHero().Card());
            Assert.IsTrue(card.Is().NonHero().Target());
            Assert.IsFalse(card.Is().NonHero().NonTarget());

            Assert.IsTrue(card.Is().NonVillain().AccordingTo(controller));
            Assert.IsTrue(card.Is().NonVillain().Card().AccordingTo(controller));
            Assert.IsFalse(card.Is().NonVillain().Target().AccordingTo(controller));
            Assert.IsFalse(card.Is().NonVillain().NonTarget().AccordingTo(controller));
        }
    }
}
