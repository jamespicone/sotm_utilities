using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NUnit.Framework;
using NUnit.Framework.Legacy;

using Handelabra.Sentinels.Engine.Model;
using Handelabra.Sentinels.UnitTest;
using Handelabra.Sentinels.Engine.Controller;

namespace Jp.SOTMUtilities.UnitTest
{
    [TestFixture()]
    public class GeneralAlignmentTests : BaseTest
    {
        [Test()]
        public void TestNulls()
        {
            SetupGameController("BaronBlade", "Legacy", "Jp.SOTMUtilities.TestMod.AlignmentTestEnvironment");

            Card card = null;
            TurnTaker turntaker = null;
            CardController controller = null;

            Card realCard = baron.CharacterCard;
            TurnTaker realTurntaker = baron.TurnTaker;

            Assert.Throws<NullReferenceException>(
                () => { bool b = (bool)card.Is().Environment(); }
            );

            Assert.Throws<NullReferenceException>(
                () => { bool b = (bool)turntaker.Is().Environment(); }
            );

            Assert.Throws<NullReferenceException>(
                () => { bool b = (bool)controller.Is().Environment(); }
            );

            Assert.Throws<NullReferenceException>(
                () => { bool b = (bool)realCard.Is(controller).Environment(); }
            );

            Assert.Throws<NullReferenceException>(
                () => { bool b = (bool)realCard.Is().Environment().AccordingTo(controller); }
            );

            Assert.Throws<NullReferenceException>(
                () => { bool b = (bool)realTurntaker.Is(controller).Environment(); }
            );

            Assert.Throws<NullReferenceException>(
                () => { bool b = (bool)realTurntaker.Is().Environment().AccordingTo(controller); }
            );
        }

        [Test()]
        public void TestCharacter()
        {
            SetupGameController("BaronBlade", "Legacy", "Megalopolis");

            StartGame();

            var controller = legacy.CharacterCardController;
            var platform = GetMobileDefensePlatform().Card;

            ClassicAssert.IsTrue(baron.CharacterCard.Is().Character());
            ClassicAssert.IsFalse(baron.CharacterCard.Is().Noncharacter());
            ClassicAssert.IsTrue(baron.CharacterCard.Is().Villain().Target().Character().AccordingTo(controller));
            ClassicAssert.IsFalse(baron.CharacterCard.Is().Villain().Target().Noncharacter().AccordingTo(controller));
            ClassicAssert.IsTrue(legacy.CharacterCard.Is().Hero().Character().AccordingTo(controller));

            ClassicAssert.IsFalse(platform.Is().Character());
            ClassicAssert.IsTrue(platform.Is().Noncharacter());
            ClassicAssert.IsFalse(platform.Is().Villain().Target().Character().AccordingTo(controller));
            ClassicAssert.IsTrue(platform.Is().Villain().Target().Noncharacter().AccordingTo(controller));

            // TurnTakers are neither character nor non-character cards.
            ClassicAssert.IsFalse(legacy.TurnTaker.Is().Character());
            ClassicAssert.IsFalse(legacy.TurnTaker.Is().Noncharacter());
            ClassicAssert.IsFalse(legacy.TurnTaker.Is().Hero().Character().AccordingTo(controller));
            ClassicAssert.IsFalse(legacy.TurnTaker.Is().Hero().Noncharacter().AccordingTo(controller));
        }

        [Test()]
        public void TestNoncard()
        {
            SetupGameController("BaronBlade", "Legacy", "Megalopolis");

            var controller = legacy.CharacterCardController;

            ClassicAssert.IsTrue(legacy.TurnTaker.Is().Noncard());
            ClassicAssert.IsTrue(legacy.TurnTaker.Is().Hero().Noncard().AccordingTo(controller));
            ClassicAssert.IsFalse(baron.TurnTaker.Is().Hero().Noncard().AccordingTo(controller));

            ClassicAssert.IsFalse(legacy.CharacterCard.Is().Noncard());
            ClassicAssert.IsFalse(legacy.CharacterCard.Is().Hero().Noncard().AccordingTo(controller));
        }

        [Test()]
        public void TestOverloads()
        {
            SetupGameController("BaronBlade", "Legacy", "Megalopolis");

            StartGame();

            var controller = legacy.CharacterCardController;

            var cardDamageSource = new DamageSource(GameController, legacy.CharacterCard);
            ClassicAssert.IsTrue(cardDamageSource.Is().Hero().Target().Character().AccordingTo(controller));
            ClassicAssert.IsTrue(cardDamageSource.Is(controller).Hero().Target().Character());
            ClassicAssert.IsFalse(cardDamageSource.Is(controller).Villain());

            var turnTakerDamageSource = new DamageSource(GameController, legacy.TurnTaker);
            ClassicAssert.IsTrue(turnTakerDamageSource.Is().Hero().Noncard().AccordingTo(controller));
            ClassicAssert.IsTrue(turnTakerDamageSource.Is(controller).Hero().Noncard());
            ClassicAssert.IsFalse(turnTakerDamageSource.Is().Target());
            ClassicAssert.IsFalse(turnTakerDamageSource.Is(controller).Villain());

            ClassicAssert.IsTrue(controller.Is().Hero().Character().AccordingTo(controller));
            ClassicAssert.IsTrue(controller.Is(controller).Hero().Character());
            ClassicAssert.IsFalse(baron.CharacterCardController.Is(controller).Hero());

            ClassicAssert.IsTrue(legacy.Is().Hero().AccordingTo(controller));
            ClassicAssert.IsTrue(legacy.Is(controller).Hero());
            ClassicAssert.IsFalse(baron.Is(controller).Hero());

            ClassicAssert.IsTrue(baron.CharacterCard.Is(controller).Villain().Target().Character());
            ClassicAssert.IsTrue(baron.TurnTaker.Is(controller).Villain());
        }

        [Test()]
        public void TestReusingPartialHelper()
        {
            SetupGameController("BaronBlade", "Legacy", "Jp.SOTMUtilities.TestMod.AlignmentTestHero");

            var target = GetCard("HeroOwnedHeroTarget");
            var targetHelper = target.Is(GetCardController(target));

            // Each branch off targetHelper shouldn't affect targetHelper or the other branches.
            ClassicAssert.IsFalse(targetHelper.NonTarget());
            ClassicAssert.IsTrue(targetHelper.Hero());
            ClassicAssert.IsTrue(targetHelper.Target());
            ClassicAssert.IsFalse(targetHelper.Villain());
            ClassicAssert.IsTrue(targetHelper);

            var ongoing = GetCard("HeroOngoing");
            var ongoingController = GetCardController(ongoing);
            var ongoingHelper = ongoing.Is().Hero();

            ClassicAssert.IsFalse(ongoingHelper.Equipment().AccordingTo(ongoingController));
            ClassicAssert.IsTrue(ongoingHelper.Ongoing().AccordingTo(ongoingController));
            ClassicAssert.IsFalse(ongoingHelper.WithoutKeyword("ongoing").AccordingTo(ongoingController));
            ClassicAssert.IsTrue(ongoingHelper.AccordingTo(ongoingController));
        }

        [Test()]
        public void TestVillainTeam()
        {
            SetupGameController("LaCapitanTeam", "Legacy", "Megalopolis");

            StartGame();

            var controller = legacy.CharacterCardController;
            var lacapitan = lacapitanTeam.CharacterCard;

            ClassicAssert.IsTrue(lacapitan.Is().Villain().AccordingTo(controller));
            ClassicAssert.IsTrue(lacapitan.Is().Villain().Target().Character().AccordingTo(controller));
            ClassicAssert.IsFalse(lacapitan.Is().NonVillain().Target().AccordingTo(controller));
            ClassicAssert.IsFalse(lacapitan.Is().Hero().Target().AccordingTo(controller));
            ClassicAssert.IsTrue(lacapitan.Is().NonHero().Target().AccordingTo(controller));

            ClassicAssert.IsTrue(lacapitanTeam.TurnTaker.Is().Villain().AccordingTo(controller));
        }
    }
}
