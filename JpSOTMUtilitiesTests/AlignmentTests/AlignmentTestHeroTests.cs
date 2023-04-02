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
        public void TestHeroTurnTaker()
        {
            SetupGameController("BaronBlade", "Legacy", "Jp.SOTMUtilities.TestMod.AlignmentTestHero");

            var tt = legacy.TurnTaker;
            var controller = GetCardController(legacy.CharacterCard);

            Assert.IsFalse(tt.Is().Environment());
            Assert.IsFalse(tt.Is().Environment().Card());
            Assert.IsFalse(tt.Is().Environment().Target());
            Assert.IsFalse(tt.Is().Environment().NonTarget());

            Assert.IsTrue(tt.Is().Hero().AccordingTo(controller));
            Assert.IsFalse(tt.Is().Hero().Card().AccordingTo(controller));
            Assert.IsFalse(tt.Is().Hero().Target().AccordingTo(controller));
            Assert.IsTrue(tt.Is().Hero().NonTarget().AccordingTo(controller));

            Assert.IsFalse(tt.Is().Villain().AccordingTo(controller));
            Assert.IsFalse(tt.Is().Villain().AccordingTo(controller).Card());
            Assert.IsFalse(tt.Is().Villain().Target().AccordingTo(controller));
            Assert.IsFalse(tt.Is().Villain().NonTarget().AccordingTo(controller));

            Assert.IsTrue(tt.Is().NonEnvironment());
            Assert.IsFalse(tt.Is().NonEnvironment().Card());
            Assert.IsFalse(tt.Is().NonEnvironment().Target());
            Assert.IsTrue(tt.Is().NonEnvironment().NonTarget());

            Assert.IsFalse(tt.Is().NonHero().AccordingTo(controller));
            Assert.IsFalse(tt.Is().NonHero().Card().AccordingTo(controller));
            Assert.IsFalse(tt.Is().NonHero().Target().AccordingTo(controller));
            Assert.IsFalse(tt.Is().NonHero().NonTarget().AccordingTo(controller));

            Assert.IsTrue(tt.Is().NonVillain().AccordingTo(controller));
            Assert.IsFalse(tt.Is().NonVillain().AccordingTo(controller).Card());
            Assert.IsFalse(tt.Is().NonVillain().Target().AccordingTo(controller));
            Assert.IsTrue(tt.Is().NonVillain().NonTarget().AccordingTo(controller));
        }

        [Test()]
        public void TestEnvCard()
        {
            SetupGameController("BaronBlade", "Legacy", "Jp.SOTMUtilities.TestMod.AlignmentTestHero");

            var card = GetCard("HeroOwnedEnvCard");
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
            SetupGameController("BaronBlade", "Legacy", "Jp.SOTMUtilities.TestMod.AlignmentTestHero");

            var card = GetCard("HeroOwnedHeroCard");
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
            SetupGameController("BaronBlade", "Legacy", "Jp.SOTMUtilities.TestMod.AlignmentTestHero");

            var card = GetCard("HeroOwnedVillainCard");
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
            SetupGameController("BaronBlade", "Legacy", "Jp.SOTMUtilities.TestMod.AlignmentTestHero");

            var card = GetCard("HeroOwnedEnvTarget");
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
            SetupGameController("BaronBlade", "Legacy", "Jp.SOTMUtilities.TestMod.AlignmentTestHero");

            var card = GetCard("HeroOwnedHeroTarget");
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
            SetupGameController("BaronBlade", "Legacy", "Jp.SOTMUtilities.TestMod.AlignmentTestHero");

            var card = GetCard("HeroOwnedVillainTarget");
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
        public void TestEnvTargetHeroCard()
        {
            SetupGameController("BaronBlade", "Legacy", "Jp.SOTMUtilities.TestMod.AlignmentTestHero");

            var card = GetCard("HeroOwnedEnvTargetHeroCard");
            var controller = GetCardController(card);

            Assert.IsFalse(card.Is().Environment());
            Assert.IsFalse(card.Is().Environment().Card());
            Assert.IsTrue(card.Is().Environment().Target());
            Assert.IsFalse(card.Is().Environment().NonTarget());

            Assert.IsTrue(card.Is().Hero().AccordingTo(controller));
            Assert.IsTrue(card.Is().Hero().AccordingTo(controller).Card());
            Assert.IsFalse(card.Is().Hero().AccordingTo(controller).Target());
            Assert.IsFalse(card.Is().Hero().AccordingTo(controller).NonTarget());

            Assert.IsFalse(card.Is().Villain().AccordingTo(controller));
            Assert.IsFalse(card.Is().Villain().AccordingTo(controller).Card());
            Assert.IsFalse(card.Is().Villain().Target().AccordingTo(controller));
            Assert.IsFalse(card.Is().Villain().NonTarget().AccordingTo(controller));

            Assert.IsTrue(card.Is().NonEnvironment());
            Assert.IsTrue(card.Is().NonEnvironment().Card());
            Assert.IsFalse(card.Is().NonEnvironment().Target());
            Assert.IsFalse(card.Is().NonEnvironment().NonTarget());

            Assert.IsFalse(card.Is().NonHero().AccordingTo(controller));
            Assert.IsFalse(card.Is().NonHero().AccordingTo(controller).Card());
            Assert.IsTrue(card.Is().NonHero().AccordingTo(controller).Target());
            Assert.IsFalse(card.Is().NonHero().AccordingTo(controller).NonTarget());

            Assert.IsTrue(card.Is().NonVillain().AccordingTo(controller));
            Assert.IsTrue(card.Is().NonVillain().AccordingTo(controller).Card());
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
        public void TestVillainTargetHeroCard()
        {
            SetupGameController("BaronBlade", "Legacy", "Jp.SOTMUtilities.TestMod.AlignmentTestHero");

            var card = GetCard("HeroOwnedVillainTargetHeroCard");
            var controller = GetCardController(card);

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

        [Test()]
        public void TestModifiedDeckKind()
        {
            SetupGameController("BaronBlade", "Legacy", "Jp.SOTMUtilities.TestMod.AlignmentTestHero");

            var card = GetCard("HeroCardInsistsItsVillain");
            var controller = GetCardController(card);
            PlayCard(card); // can't be asked if its hero or not if its not in play

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
        public void TestOngoing()
        {
            SetupGameController("BaronBlade", "Legacy", "Jp.SOTMUtilities.TestMod.AlignmentTestHero");

            var card = GetCard("HeroOngoing");
            var controller = GetCardController(card);

            Assert.IsTrue(card.Is().Ongoing().AccordingTo(controller));
            Assert.IsTrue(card.Is().Hero().Ongoing().AccordingTo(controller));
            Assert.IsFalse(card.Is().Villain().Ongoing().AccordingTo(controller));

            Assert.IsFalse(card.Is().Equipment().AccordingTo(controller));
            Assert.IsFalse(card.Is().Hero().Equipment().AccordingTo(controller));
            Assert.IsFalse(card.Is().Villain().Equipment().AccordingTo(controller));
        }

        [Test()]
        public void TestEquipment()
        {
            SetupGameController("BaronBlade", "Legacy", "Jp.SOTMUtilities.TestMod.AlignmentTestHero");

            var card = GetCard("HeroEquipment");
            var controller = GetCardController(card);

            Assert.IsFalse(card.Is().Ongoing().AccordingTo(controller));
            Assert.IsFalse(card.Is().Hero().Ongoing().AccordingTo(controller));
            Assert.IsFalse(card.Is().Villain().Ongoing().AccordingTo(controller));

            Assert.IsTrue(card.Is().Equipment().AccordingTo(controller));
            Assert.IsTrue(card.Is().Hero().Equipment().AccordingTo(controller));
            Assert.IsFalse(card.Is().Villain().Equipment().AccordingTo(controller));
        }
    }
}
