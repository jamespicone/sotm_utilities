using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NUnit.Framework;
using NUnit.Framework.Legacy;

using Handelabra.Sentinels.Engine.Model;
using Handelabra.Sentinels.Engine.Controller;
using Handelabra.Sentinels.UnitTest;

namespace Jp.SOTMUtilities.UnitTest
{
    // Maze of Mirrors (MMFFCC) reads "Treat every instance of the word 'villain' on hero cards as if it were the word
    // 'hero' instead". The engine implements that by answering villain-ness questions differently depending on which
    // card is asking, which is what AccordingTo() is for.
    [TestFixture()]
    public class MazeOfMirrorsTests : BaseTest
    {
        [Test()]
        public void TestDependsOnWhoIsAsking()
        {
            SetupGameController("BaronBlade", "Legacy", "MMFFCC");

            StartGame();

            PlayCard("MazeOfMirrors");

            var heroAsking = legacy.CharacterCardController;
            var villainAsking = baron.CharacterCardController;
            var platform = GetMobileDefensePlatform().Card;

            // According to a hero card, "villain" means hero.
            ClassicAssert.IsFalse(baron.CharacterCard.Is().Villain().AccordingTo(heroAsking));
            ClassicAssert.IsFalse(platform.Is().Villain().Target().AccordingTo(heroAsking));
            ClassicAssert.IsFalse(baron.TurnTaker.Is().Villain().AccordingTo(heroAsking));
            ClassicAssert.IsTrue(legacy.CharacterCard.Is().Villain().AccordingTo(heroAsking));
            ClassicAssert.IsTrue(legacy.CharacterCard.Is().Villain().Target().AccordingTo(heroAsking));
            ClassicAssert.IsTrue(legacy.TurnTaker.Is().Villain().AccordingTo(heroAsking));

            // "hero" still means hero.
            ClassicAssert.IsTrue(legacy.CharacterCard.Is().Hero().Target().AccordingTo(heroAsking));
            ClassicAssert.IsFalse(baron.CharacterCard.Is().Hero().Target().AccordingTo(heroAsking));

            // Villain cards aren't affected.
            ClassicAssert.IsTrue(baron.CharacterCard.Is().Villain().AccordingTo(villainAsking));
            ClassicAssert.IsTrue(platform.Is().Villain().Target().AccordingTo(villainAsking));
            ClassicAssert.IsTrue(baron.TurnTaker.Is().Villain().AccordingTo(villainAsking));
            ClassicAssert.IsFalse(legacy.CharacterCard.Is().Villain().AccordingTo(villainAsking));
            ClassicAssert.IsFalse(legacy.TurnTaker.Is().Villain().AccordingTo(villainAsking));
        }

        private void AssertTargetAlignmentMatchesEngine(Card card, CardController asker, bool expectedHero, bool expectedVillain)
        {
            var source = asker.GetCardSource();

            ClassicAssert.AreEqual(expectedHero, GameController.AskCardControllersIfIsHeroTarget(card, source), "Engine hero target");
            ClassicAssert.AreEqual(expectedVillain, GameController.AskCardControllersIfIsVillainTarget(card, source), "Engine villain target");

            ClassicAssert.AreEqual(expectedHero, (bool)card.Is().Hero().Target().AccordingTo(asker), "Hero target");
            ClassicAssert.AreEqual(expectedVillain, (bool)card.Is().Villain().Target().AccordingTo(asker), "Villain target");
        }

        // Maze of Mirrors has a higher AskPriority than other cards that change alignment, so the engine asks it
        // first. These cards are in decks set up before the environment, so would be asked first otherwise.
        [Test()]
        public void TestAskedBeforeHeroModifier()
        {
            SetupGameController("BaronBlade", "Legacy", "Jp.SOTMUtilities.TestMod.AlignmentTestHero", "MMFFCC");

            // Insists it's a villain target, not a hero target. According to a hero card, Maze of Mirrors says it's
            // not a villain target because it's not a hero target.
            var card = PlayCard("HeroTargetInsistsItsVillainTarget");
            PlayCard("MazeOfMirrors");

            AssertTargetAlignmentMatchesEngine(card, legacy.CharacterCardController, expectedHero: false, expectedVillain: false);
        }

        [Test()]
        public void TestAskedBeforeVillainModifier()
        {
            SetupGameController("BaronBlade", "Legacy", "Jp.SOTMUtilities.TestMod.AlignmentTestVillain", "MMFFCC");

            // Insists it's a hero target, not a villain target. According to a hero card, Maze of Mirrors says it's a
            // villain target because it's a hero target.
            var card = PlayCard("VillainTargetInsistsItsHeroTarget");
            PlayCard("MazeOfMirrors");

            AssertTargetAlignmentMatchesEngine(card, legacy.CharacterCardController, expectedHero: true, expectedVillain: true);
        }
    }
}
