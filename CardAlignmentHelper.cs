using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

using Handelabra.Sentinels.Engine.Controller;
using Handelabra.Sentinels.Engine.Model;

using Handelabra;

namespace Jp.SOTMUtilities
{
    // Enum representing card 'alignment' - hero/villain/environment.
    // Also can represent nonhero/nonvillain/nonenvironment.
    // Used for card text that says something like "the hero card..." or "the nonenvironment target..."
    //
    // The values are deliberately set up so that the bottom bit is on for the 'non' case.
    // It's not a full bitflags thing.
    public enum CardAlignment
    {
        Hero = 0,
        Nonhero = 1,
        Environment = 2,
        Nonenvironment = 3,
        Villain = 4,
        Nonvillain = 5
    };

    // Enum representing card 'target-ness' - either a target, a non-target, or just a 'card'.
    // Used for card text that says something like "the villain target..." (target) or
    // "the environment card..." (either) or "an environment non-target" (nontarget)
    public enum CardTarget
    {
        Either,
        Target,
        Nontarget
    };

    // Builder class for testing card/turntaker properties.
    //
    // Can indicate whether or not a card is:
    // - A hero/villain/environment/nonhero/nonvillain/nonenvironment
    // - A target/nontarget
    // - A character/noncharacter
    // - Has specific keywords
    // - Doesn't have specific keywords
    //
    // Can indicate whether or not a turntaker is:
    // - A hero/villain/environment/nonhero/nonvillain/nonenvironment
    //
    // This takes into account cards that have one alignment as a card
    // and a different alignment as a target (e.g. Heroic Infinitor).
    //
    // It takes into account cards that change villain-ness of other cards.
    //
    // It takes into account cards that add keywords to other cards.
    //
    // If target-ness or keywords are specified turntakers always return false.
    // This is mostly to support .Is() on DamageSources.
    //
    // If checking for villain-ness or keywords a CardController must be provided
    // via .AccordingTo(). That's the CardController that wants to know - it's
    // necessary for villain-ness so we can call AskCardControllersIfIsVillain.
    // It's necessary for keyword-ness because we can't otherwise get to GameController
    // to call DoesCardContainKeyword
    public class CardAlignmentHelper
    {
        internal CardAlignmentHelper(Card _card, CardController _controller = null)
        {
            card = _card;
            controller = _controller;
        }

        internal CardAlignmentHelper(TurnTaker _turntaker, CardController _controller = null)
        {
            turntaker = _turntaker;
            controller = _controller;
        }

        // Is specifically sourced from a card. Excludes TurnTakers (does not exclude targets)
        public CardAlignmentHelper Card()
        {
            isCard = true;
            return this;
        }

        // Is sourced from a TurnTaker. Excludes cards.
        public CardAlignmentHelper Noncard()
        {
            isCard = false;
            return this;
        }

        // Is specifically a target. Excludes TurnTakers as well as non-target cards.
        public CardAlignmentHelper Target()
        {
            target = CardTarget.Target;
            return this;
        }

        // Is specifically not a target. Either a TurnTaker or a non-target card.
        public CardAlignmentHelper NonTarget()
        {
            target = CardTarget.Nontarget;
            return this;
        }

        // Is specifically a character card. Excludes TurnTakers.
        public CardAlignmentHelper Character()
        {
            character = true;
            return this;
        }

        // Is specifically a non-character card. Excludes TurnTakers.
        public CardAlignmentHelper Noncharacter()
        {
            character = false;
            return this;
        }

        // Is specifically Hero.
        // If Target() is specified, a Hero target (uses targetKind). Otherwise, a Hero card or TurnTaker.
        public CardAlignmentHelper Hero()
        {
            alignment = CardAlignment.Hero;
            return this;
        }

        // Is specifically Environment.
        // If Target() is specified, an Environment target (uses targetKind). Otherwise an Environment card or TurnTaker.
        public CardAlignmentHelper Environment()
        {
            alignment = CardAlignment.Environment;
            return this;
        }

        // Is specifically Villain.
        // If Target() is specified, a Villain target (uses targetKind). Otherwise a Villain card or TurnTaker.
        //
        // Requires that a controller is passed via AccordingTo or the parameter to Is; that's the controller
        // that wants to know whether a card is a villain. It gets passed to AskCardControllersIfIsVillain.
        public CardAlignmentHelper Villain()
        {
            alignment = CardAlignment.Villain;
            return this;
        }

        // Is specifically non-hero.
        // If Target() is specified, a non-hero target (uses targetKind). Otherwise a non-hero card or TurnTaker.
        //
        // Note that this doesn't call AskCardControllersIfIsVillain; this is consistent with Handleabra's usage.
        // That might be because they only use that mechanism for Maze of Mirrors, which wouldn't affect this
        // usage. It's not clear what the 'correct' behaviour would be.
        public CardAlignmentHelper NonHero()
        {
            alignment = CardAlignment.Nonhero;
            return this;
        }

        // Is specifically non-environment.
        // If Target() is specified, a non-environment target (uses targetKind). Otherwise a non-environment card or TurnTaker.
        //
        // Note that this doesn't call AskCardControllersIfIsVillain; this is consistent with Handleabra's usage.
        // That might be because they only use that mechanism for Maze of Mirrors, which wouldn't affect this
        // usage. It's not clear what the 'correct' behaviour would be.
        public CardAlignmentHelper NonEnvironment()
        {
            alignment = CardAlignment.Nonenvironment;
            return this;
        }

        // Is specifically non-villain.
        // If Target() is specified, a non-villain target (uses targetKind).
        //
        // Requires that a controller is passed via AccordingTo or the parameter to Is; that's the controller
        // that wants to know whether a card is a villain. It gets passed to AskCardControllersIfIsVillain.
        public CardAlignmentHelper NonVillain()
        {
            alignment = CardAlignment.Nonvillain;
            return this;
        }

        // Adds a keyword that the card must have (e.g., Ongoing).
        // Excludes TurnTakers (as they don't have keywords).
        //
        // A CardController must be specified with AccordingTo or Is because of implementation limitations.
        public CardAlignmentHelper WithKeyword(string keyword)
        {
            expectedKeywords.Add(keyword);
            return this;
        }

        // Adds a keyword that the card musn't have (i.e. "non-Citizen")
        // Excludes TurnTakers.
        //
        // A CardController must be specified with AccordingTo or Is because of implementation limitations.
        public CardAlignmentHelper WithoutKeyword(string keyword)
        {
            unwantedKeywords.Add(keyword);
            return this;
        }

        // Specifies a CardController that is doing the asking.
        // This is necessary for Villain, NonVillain, WithKeyword and WithoutKeyword.
        //
        // For the villain checks this is because of the AskCardControllersIfIsVillain function,
        // which needs to know which controller is doing the asking. This is used in the base game to implement
        // Maze of Mirrors and probably won't work for any other use, but for the sake of future-proofing it's
        // handled here.
        //
        // For the keyword checks it's just because we need to get a GameController somehow and we only have
        // a Card; it's an implementation issue.
        public CardAlignmentHelper AccordingTo(CardController _controller)
        {
            controller = _controller;
            return this;
        }

        // Indicates whether or not the Card/TurnTaker 'helper' was constructed on meets the requirements specified.
        public static implicit operator bool(CardAlignmentHelper helper)
        {
            if (helper.alignment == CardAlignment.Villain && helper.controller == null)
            {
                throw new InvalidOperationException("CardAlignmentHelper with villain alignment without controller converted to bool (Missing AccordingTo?)");
            }

            if (helper.expectedKeywords.Count() > 0 && helper.controller == null)
            {
                throw new InvalidOperationException("CardAlignmentHelper with expected keywords but without controller converted to bool (Missing AccordingTo?)");
            }

            if (helper.unwantedKeywords.Count() > 0 && helper.controller == null)
            {
                throw new InvalidOperationException("CardAlignmentHelper with unwanted keywords but without controller converted to bool (Missing AccordingTo?)");
            }

            if (helper.card != null)
            {
                if (helper.isCard == false)
                {
                    return false;
                }

                bool hasAlignment = true;
                if (helper.alignment.HasValue)
                {
                    var baseAlignment = (CardAlignment)(((int)helper.alignment.Value) & ~1);
                    switch(baseAlignment)
                    {
                        case CardAlignment.Hero:
                            if (helper.target == CardTarget.Target) {
                                hasAlignment = (helper.card.TargetKind ?? helper.card.Kind) == DeckDefinition.DeckKind.Hero;
                            }
                            else { hasAlignment = helper.card.IsHero; }
                            break;
                        case CardAlignment.Villain:
                            if (helper.target == CardTarget.Target) {

                                if (cachedAskAllCardControllersInList == null)
                                {
                                    var method = helper.controller.GameController.GetType().GetMethod(
                                        "AskAllCardControllersInList",
                                        BindingFlags.NonPublic | BindingFlags.Instance
                                    );

                                    cachedAskAllCardControllersInList = method.MakeGenericMethod(typeof(bool?));
                                }

                                var result = (bool?)cachedAskAllCardControllersInList.Invoke(
                                    helper.controller.GameController,
                                    new object[] {
                                        CardControllerListType.ModifiesDeckKind,
                                        (Func<CardController, bool?>)(cc => cc.AskIfIsVillainTarget(helper.card, helper.controller.GetCardSource())),
                                        true,
                                        null
                                    }
                                );

                                var kind = helper.card.TargetKind ?? helper.card.Kind;
                                var isVillainDefault = kind == DeckDefinition.DeckKind.Villain ||
                                    kind == DeckDefinition.DeckKind.VillainTeam;

                                hasAlignment = result.GetValueOrDefault(isVillainDefault);
                            }
                            else { hasAlignment = helper.controller.GameController.AskCardControllersIfIsVillain(helper.card, helper.controller.GetCardSource()); }
                            break;
                        case CardAlignment.Environment:
                            if (helper.target == CardTarget.Target) {
                                hasAlignment = (helper.card.TargetKind ?? helper.card.Kind) == DeckDefinition.DeckKind.Environment;
                            }
                            else { hasAlignment = helper.card.IsEnvironment; }
                            break;
                        default:
                            hasAlignment = false;
                            break;
                    }

                    var isNonCase = (((int)helper.alignment.Value) & 1) > 0;
                    if (isNonCase)
                    {
                        hasAlignment = !hasAlignment;
                    }
                }

                switch(helper.target)
                {
                    case CardTarget.Target: hasAlignment = hasAlignment && helper.card.IsTarget; break;
                    case CardTarget.Nontarget: hasAlignment = hasAlignment && ! helper.card.IsTarget; break;
                    case CardTarget.Either:
                    default: break;
                }

                foreach (string keyword in helper.expectedKeywords)
                {
                    if (! helper.controller.GameController.DoesCardContainKeyword(helper.card, keyword))
                    {
                        return false;
                    }
                }

                foreach (string keyword in helper.unwantedKeywords)
                {
                    if (helper.controller.GameController.DoesCardContainKeyword(helper.card, keyword))
                    {
                        return false;
                    }
                }

                return hasAlignment && (helper.character == null || helper.character.Value == helper.card.IsCharacter);
            }

            if (helper.turntaker != null)
            {
                if (helper.isCard == true)
                {
                    return false;
                }

                if (helper.target == CardTarget.Target)
                {
                    return false;
                }

                if (helper.expectedKeywords.Count() > 0 || helper.unwantedKeywords.Count() > 0)
                {
                    return false;
                }

                if (helper.alignment.HasValue)
                {
                    // CardAlignment enum is deliberately set up so that the bottom bit is 'non-'.
                    var baseAlignment = (CardAlignment)(((int)helper.alignment) & ~1);

                    bool hasBaseAlignment = false;
                    switch (baseAlignment)
                    {
                        case CardAlignment.Hero:
                            hasBaseAlignment = helper.turntaker.IsHero;
                            break;

                        case CardAlignment.Villain:
                            hasBaseAlignment = helper.controller.IsVillain(helper.turntaker);
                            break;

                        case CardAlignment.Environment:
                            hasBaseAlignment = helper.turntaker.IsEnvironment;
                            break;

                        default:
                            break;
                    }

                    var isNonCase = (((int)helper.alignment) & 1) > 0;
                    if (isNonCase)
                    {
                        hasBaseAlignment = !hasBaseAlignment;
                    }

                    return hasBaseAlignment;
                }

                return true;
            }

            throw new InvalidOperationException("CardAlignmentHelper without card or turntaker converted to bool");
        }

        private TurnTaker turntaker;
        private Card card;
        private CardController controller;

        private CardAlignment? alignment;
        private CardTarget target = CardTarget.Either;
        private bool? character = null;
        private bool? isCard = null;

        private List<string> expectedKeywords = new List<string>();
        private List<string> unwantedKeywords = new List<string>();

        private static MethodInfo cachedAskAllCardControllersInList = null;
    }
}
