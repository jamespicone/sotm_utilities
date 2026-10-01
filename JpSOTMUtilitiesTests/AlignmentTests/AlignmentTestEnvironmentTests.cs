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
    public class AlignmentTestEnvironmentTests : BaseTest
    {
        [Test()]
        public void TestEnvTurnTaker()
        {
            SetupGameController("BaronBlade", "Legacy", "Jp.SOTMUtilities.TestMod.AlignmentTestEnvironment");

            var tt = FindEnvironment();
            var controller = GetCardController(legacy.CharacterCard);

            ClassicAssert.IsTrue(tt.Is().Environment());
            ClassicAssert.IsFalse(tt.Is().Environment().Card());
            ClassicAssert.IsFalse(tt.Is().Environment().Target());
            ClassicAssert.IsTrue(tt.Is().Environment().NonTarget());

            ClassicAssert.IsFalse(tt.Is().Hero().AccordingTo(controller));
            ClassicAssert.IsFalse(tt.Is().Hero().Card().AccordingTo(controller));
            ClassicAssert.IsFalse(tt.Is().Hero().Target().AccordingTo(controller));
            ClassicAssert.IsFalse(tt.Is().Hero().NonTarget().AccordingTo(controller));

            ClassicAssert.IsFalse(tt.Is().Villain().AccordingTo(controller));
            ClassicAssert.IsFalse(tt.Is().Villain().Card().AccordingTo(controller));
            ClassicAssert.IsFalse(tt.Is().Villain().Target().AccordingTo(controller));
            ClassicAssert.IsFalse(tt.Is().Villain().NonTarget().AccordingTo(controller));

            ClassicAssert.IsFalse(tt.Is().NonEnvironment());
            ClassicAssert.IsFalse(tt.Is().NonEnvironment().Card());
            ClassicAssert.IsFalse(tt.Is().NonEnvironment().Target());
            ClassicAssert.IsFalse(tt.Is().NonEnvironment().NonTarget());

            ClassicAssert.IsTrue(tt.Is().NonHero().AccordingTo(controller));
            ClassicAssert.IsFalse(tt.Is().NonHero().Card().AccordingTo(controller));
            ClassicAssert.IsFalse(tt.Is().NonHero().Target().AccordingTo(controller));
            ClassicAssert.IsTrue(tt.Is().NonHero().NonTarget().AccordingTo(controller));

            ClassicAssert.IsTrue(tt.Is().NonVillain().AccordingTo(controller));
            ClassicAssert.IsFalse(tt.Is().NonVillain().Card().AccordingTo(controller));
            ClassicAssert.IsFalse(tt.Is().NonVillain().Target().AccordingTo(controller));
            ClassicAssert.IsTrue(tt.Is().NonVillain().NonTarget().AccordingTo(controller));
        }

        [Test()]
        public void TestEnvCard()
        {
            SetupGameController("BaronBlade", "Legacy", "Jp.SOTMUtilities.TestMod.AlignmentTestEnvironment");

            var card = GetCard("EnvOwnedEnvCard");
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
            ClassicAssert.IsFalse(card.Is().Villain().Card().AccordingTo(controller));
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
            ClassicAssert.IsTrue(card.Is().NonVillain().Card().AccordingTo(controller));
            ClassicAssert.IsFalse(card.Is().NonVillain().Target().AccordingTo(controller));
            ClassicAssert.IsTrue(card.Is().NonVillain().NonTarget().AccordingTo(controller));
        }

        [Test()]
        public void TestHeroCard()
        {
            SetupGameController("BaronBlade", "Legacy", "Jp.SOTMUtilities.TestMod.AlignmentTestEnvironment");

            var card = GetCard("EnvOwnedHeroCard");
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
            ClassicAssert.IsFalse(card.Is().Villain().Card().AccordingTo(controller));
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
            ClassicAssert.IsTrue(card.Is().NonVillain().Card().AccordingTo(controller));
            ClassicAssert.IsFalse(card.Is().NonVillain().Target().AccordingTo(controller));
            ClassicAssert.IsTrue(card.Is().NonVillain().NonTarget().AccordingTo(controller));
        }

        [Test()]
        public void TestVillainCard()
        {
            SetupGameController("BaronBlade", "Legacy", "Jp.SOTMUtilities.TestMod.AlignmentTestEnvironment");

            var card = GetCard("EnvOwnedVillainCard");
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
            ClassicAssert.IsTrue(card.Is().Villain().Card().AccordingTo(controller));
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
            ClassicAssert.IsFalse(card.Is().NonVillain().Card().AccordingTo(controller));
            ClassicAssert.IsFalse(card.Is().NonVillain().Target().AccordingTo(controller));
            ClassicAssert.IsFalse(card.Is().NonVillain().NonTarget().AccordingTo(controller));
        }

        [Test()]
        public void TestEnvTarget()
        {
            SetupGameController("BaronBlade", "Legacy", "Jp.SOTMUtilities.TestMod.AlignmentTestEnvironment");

            var card = GetCard("EnvOwnedEnvTarget");
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
            ClassicAssert.IsFalse(card.Is().Villain().Card().AccordingTo(controller));
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
            ClassicAssert.IsTrue(card.Is().NonVillain().Card().AccordingTo(controller));
            ClassicAssert.IsTrue(card.Is().NonVillain().Target().AccordingTo(controller));
            ClassicAssert.IsFalse(card.Is().NonVillain().NonTarget().AccordingTo(controller));
        }

        [Test()]
        public void TestHeroTarget()
        {
            SetupGameController("BaronBlade", "Legacy", "Jp.SOTMUtilities.TestMod.AlignmentTestEnvironment");

            var card = GetCard("EnvOwnedHeroTarget");
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
            ClassicAssert.IsFalse(card.Is().Villain().Card().AccordingTo(controller));
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
            ClassicAssert.IsTrue(card.Is().NonVillain().Card().AccordingTo(controller));
            ClassicAssert.IsTrue(card.Is().NonVillain().Target().AccordingTo(controller));
            ClassicAssert.IsFalse(card.Is().NonVillain().NonTarget().AccordingTo(controller));
        }

        [Test()]
        public void TestVillainTarget()
        {
            SetupGameController("BaronBlade", "Legacy", "Jp.SOTMUtilities.TestMod.AlignmentTestEnvironment");

            var card = GetCard("EnvOwnedVillainTarget");
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
            ClassicAssert.IsTrue(card.Is().Villain().Card().AccordingTo(controller));
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
            ClassicAssert.IsFalse(card.Is().NonVillain().Card().AccordingTo(controller));
            ClassicAssert.IsFalse(card.Is().NonVillain().Target().AccordingTo(controller));
            ClassicAssert.IsFalse(card.Is().NonVillain().NonTarget().AccordingTo(controller));
        }

        [Test()]
        public void TestEnvTargetEnvCard()
        {
            SetupGameController("BaronBlade", "Legacy", "Jp.SOTMUtilities.TestMod.AlignmentTestEnvironment");

            var card = GetCard("EnvOwnedEnvTargetEnvCard");
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
            ClassicAssert.IsFalse(card.Is().Villain().Card().AccordingTo(controller));
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
            ClassicAssert.IsTrue(card.Is().NonVillain().Card().AccordingTo(controller));
            ClassicAssert.IsTrue(card.Is().NonVillain().Target().AccordingTo(controller));
            ClassicAssert.IsFalse(card.Is().NonVillain().NonTarget().AccordingTo(controller));
        }

        [Test()]
        public void TestHeroTargetEnvCard()
        {
            SetupGameController("BaronBlade", "Legacy", "Jp.SOTMUtilities.TestMod.AlignmentTestEnvironment");

            var card = GetCard("EnvOwnedHeroTargetEnvCard");
            var controller = GetCardController(card);

            ClassicAssert.IsTrue(card.Is().Environment());
            ClassicAssert.IsTrue(card.Is().Environment().Card());
            ClassicAssert.IsFalse(card.Is().Environment().Target());
            ClassicAssert.IsFalse(card.Is().Environment().NonTarget());

            ClassicAssert.IsFalse(card.Is().Hero().AccordingTo(controller));
            ClassicAssert.IsFalse(card.Is().Hero().AccordingTo(controller).Card());
            ClassicAssert.IsTrue(card.Is().Hero().AccordingTo(controller).Target());
            ClassicAssert.IsFalse(card.Is().Hero().AccordingTo(controller).NonTarget());

            ClassicAssert.IsFalse(card.Is().Villain().AccordingTo(controller));
            ClassicAssert.IsFalse(card.Is().Villain().Card().AccordingTo(controller));
            ClassicAssert.IsFalse(card.Is().Villain().Target().AccordingTo(controller));
            ClassicAssert.IsFalse(card.Is().Villain().NonTarget().AccordingTo(controller));

            ClassicAssert.IsFalse(card.Is().NonEnvironment());
            ClassicAssert.IsFalse(card.Is().NonEnvironment().Card());
            ClassicAssert.IsTrue(card.Is().NonEnvironment().Target());
            ClassicAssert.IsFalse(card.Is().NonEnvironment().NonTarget());

            ClassicAssert.IsTrue(card.Is().NonHero().AccordingTo(controller));
            ClassicAssert.IsTrue(card.Is().NonHero().AccordingTo(controller).Card());
            ClassicAssert.IsFalse(card.Is().NonHero().AccordingTo(controller).Target());
            ClassicAssert.IsFalse(card.Is().NonHero().AccordingTo(controller).NonTarget());

            ClassicAssert.IsTrue(card.Is().NonVillain().AccordingTo(controller));
            ClassicAssert.IsTrue(card.Is().NonVillain().Card().AccordingTo(controller));
            ClassicAssert.IsTrue(card.Is().NonVillain().Target().AccordingTo(controller));
            ClassicAssert.IsFalse(card.Is().NonVillain().NonTarget().AccordingTo(controller));
        }

        [Test()]
        public void TestVillainTargetEnvCard()
        {
            SetupGameController("BaronBlade", "Legacy", "Jp.SOTMUtilities.TestMod.AlignmentTestEnvironment");

            var card = GetCard("EnvOwnedVillainTargetEnvCard");
            var controller = GetCardController(card);

            ClassicAssert.IsTrue(card.Is().Environment());
            ClassicAssert.IsTrue(card.Is().Environment().Card());
            ClassicAssert.IsFalse(card.Is().Environment().Target());
            ClassicAssert.IsFalse(card.Is().Environment().NonTarget());

            ClassicAssert.IsFalse(card.Is().Hero().AccordingTo(controller));
            ClassicAssert.IsFalse(card.Is().Hero().AccordingTo(controller).Card());
            ClassicAssert.IsFalse(card.Is().Hero().AccordingTo(controller).Target());
            ClassicAssert.IsFalse(card.Is().Hero().AccordingTo(controller).NonTarget());

            ClassicAssert.IsFalse(card.Is().Villain().AccordingTo(controller));
            ClassicAssert.IsFalse(card.Is().Villain().Card().AccordingTo(controller));
            ClassicAssert.IsTrue(card.Is().Villain().Target().AccordingTo(controller));
            ClassicAssert.IsFalse(card.Is().Villain().NonTarget().AccordingTo(controller));

            ClassicAssert.IsFalse(card.Is().NonEnvironment());
            ClassicAssert.IsFalse(card.Is().NonEnvironment().Card());
            ClassicAssert.IsTrue(card.Is().NonEnvironment().Target());
            ClassicAssert.IsFalse(card.Is().NonEnvironment().NonTarget());

            ClassicAssert.IsTrue(card.Is().NonHero().AccordingTo(controller));
            ClassicAssert.IsTrue(card.Is().NonHero().AccordingTo(controller).Card());
            ClassicAssert.IsTrue(card.Is().NonHero().AccordingTo(controller).Target());
            ClassicAssert.IsFalse(card.Is().NonHero().AccordingTo(controller).NonTarget());

            ClassicAssert.IsTrue(card.Is().NonVillain().AccordingTo(controller));
            ClassicAssert.IsTrue(card.Is().NonVillain().Card().AccordingTo(controller));
            ClassicAssert.IsFalse(card.Is().NonVillain().Target().AccordingTo(controller));
            ClassicAssert.IsFalse(card.Is().NonVillain().NonTarget().AccordingTo(controller));
        }
    }
}
