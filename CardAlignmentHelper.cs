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

    public class CardAlignmentHelperStorage
    {
        protected CardAlignmentHelperStorage()
        { }

        protected CardAlignmentHelperStorage(CardAlignmentHelperStorage other)
        {
            CopyFrom(other);
        }

        public void CopyFrom(CardAlignmentHelperStorage other)
        {
            turntaker = other.turntaker;
            card = other.card;
            controller = other.controller;

            alignment = other.alignment;
            target = other.target;
            character = other.character;
            isCard = other.isCard;

            expectedKeywords = other.expectedKeywords;
            unwantedKeywords = other.unwantedKeywords;
        }

        // Indicates whether or not the Card/TurnTaker 'helper' was constructed on meets the requirements specified.
        internal bool ConvertToBool()
        {
            if (alignment == CardAlignment.Villain && controller == null)
            {
                throw new InvalidOperationException("CardAlignmentHelper with villain alignment without controller converted to bool (Missing AccordingTo?)");
            }

            if (alignment == CardAlignment.Hero && controller == null)
            {
                throw new InvalidOperationException("CardAlignmentHelper with hero alignment without controller converted to bool (Missing AccordingTo?)");
            }

            if (expectedKeywords.Count() > 0 && controller == null)
            {
                throw new InvalidOperationException("CardAlignmentHelper with expected keywords but without controller converted to bool (Missing AccordingTo?)");
            }

            if (unwantedKeywords.Count() > 0 && controller == null)
            {
                throw new InvalidOperationException("CardAlignmentHelper with unwanted keywords but without controller converted to bool (Missing AccordingTo?)");
            }

            if (card != null)
            {
                if (isCard == false)
                {
                    return false;
                }

                bool hasAlignment = true;
                if (alignment.HasValue)
                {
                    var baseAlignment = (CardAlignment)(((int)alignment.Value) & ~1);
                    switch (baseAlignment)
                    {
                        case CardAlignment.Hero:
                            if (cachedAskAllCardControllersInList == null)
                            {
                                var method = controller.GameController.GetType().GetMethod(
                                    "AskAllCardControllersInList",
                                    BindingFlags.NonPublic | BindingFlags.Instance
                                );

                                cachedAskAllCardControllersInList = method.MakeGenericMethod(typeof(bool?));
                            }

                            if (target == CardTarget.Target)
                            {
                                var result = (bool?)cachedAskAllCardControllersInList.Invoke(
                                    controller.GameController,
                                    new object[] {
                                        CardControllerListType.ModifiesDeckKind,
                                        (Func<CardController, bool?>)(cc => cc.AskIfIsVillainTarget(card, controller.GetCardSource())),
                                        true,
                                        null
                                    }
                                );

                                var kind = card.TargetKind ?? card.Kind;
                                var isHeroDefault = kind == DeckDefinition.DeckKind.Hero;

                                hasAlignment = result.GetValueOrDefault(isHeroDefault);
                            }
                            else { hasAlignment = controller.GameController.AskCardControllersIfIsHero(card, controller.GetCardSource()); }
                            break;
                        case CardAlignment.Villain:
                            if (cachedAskAllCardControllersInList == null)
                            {
                                var method = controller.GameController.GetType().GetMethod(
                                    "AskAllCardControllersInList",
                                    BindingFlags.NonPublic | BindingFlags.Instance
                                );

                                cachedAskAllCardControllersInList = method.MakeGenericMethod(typeof(bool?));
                            }

                            if (target == CardTarget.Target)
                            {
                                var result = (bool?)cachedAskAllCardControllersInList.Invoke(
                                    controller.GameController,
                                    new object[] {
                                        CardControllerListType.ModifiesDeckKind,
                                        (Func<CardController, bool?>)(cc => cc.AskIfIsVillainTarget(card, controller.GetCardSource())),
                                        true,
                                        null
                                    }
                                );

                                var kind = card.TargetKind ?? card.Kind;
                                var isVillainDefault = kind == DeckDefinition.DeckKind.Villain ||
                                    kind == DeckDefinition.DeckKind.VillainTeam;

                                hasAlignment = result.GetValueOrDefault(isVillainDefault);
                            }
                            else { hasAlignment = controller.GameController.AskCardControllersIfIsVillain(card, controller.GetCardSource()); }
                            break;
                        case CardAlignment.Environment:
                            if (target == CardTarget.Target)
                            {
                                hasAlignment = (card.TargetKind ?? card.Kind) == DeckDefinition.DeckKind.Environment;
                            }
                            else { hasAlignment = card.IsEnvironment; }
                            break;
                        default:
                            hasAlignment = false;
                            break;
                    }

                    var isNonCase = (((int)alignment.Value) & 1) > 0;
                    if (isNonCase)
                    {
                        hasAlignment = !hasAlignment;
                    }
                }

                switch (target)
                {
                    case CardTarget.Target: hasAlignment = hasAlignment && card.IsTarget; break;
                    case CardTarget.Nontarget: hasAlignment = hasAlignment && !card.IsTarget; break;
                    case CardTarget.Either:
                    default: break;
                }

                foreach (string keyword in expectedKeywords)
                {
                    if (!controller.GameController.DoesCardContainKeyword(card, keyword))
                    {
                        return false;
                    }
                }

                foreach (string keyword in unwantedKeywords)
                {
                    if (controller.GameController.DoesCardContainKeyword(card, keyword))
                    {
                        return false;
                    }
                }

                return hasAlignment && (character == null || character.Value == card.IsCharacter);
            }

            if (turntaker != null)
            {
                if (isCard == true)
                {
                    return false;
                }

                if (target == CardTarget.Target)
                {
                    return false;
                }

                if (expectedKeywords.Count() > 0 || unwantedKeywords.Count() > 0)
                {
                    return false;
                }

                if (alignment.HasValue)
                {
                    // CardAlignment enum is deliberately set up so that the bottom bit is 'non-'.
                    var baseAlignment = (CardAlignment)(((int)alignment) & ~1);

                    bool hasBaseAlignment = false;
                    switch (baseAlignment)
                    {
                        case CardAlignment.Hero:
                            hasBaseAlignment = turntaker.IsHero;
                            break;

                        case CardAlignment.Villain:
                            hasBaseAlignment = controller.IsVillain(turntaker);
                            break;

                        case CardAlignment.Environment:
                            hasBaseAlignment = turntaker.IsEnvironment;
                            break;

                        default:
                            break;
                    }

                    var isNonCase = (((int)alignment) & 1) > 0;
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

        protected TurnTaker turntaker;
        protected Card card;
        protected CardController controller;

        protected CardAlignment? alignment;
        protected CardTarget target = CardTarget.Either;
        protected bool? character = null;
        protected bool? isCard = null;

        protected List<string> expectedKeywords = new List<string>();
        protected List<string> unwantedKeywords = new List<string>();

        protected static MethodInfo cachedAskAllCardControllersInList = null;
    }

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
    public class CardAlignmentHelperBase<NoChangeType, NeedControllerType, ControllerSuppliedType> : CardAlignmentHelperStorage
        where NoChangeType : CardAlignmentHelperStorage, new()
        where NeedControllerType : CardAlignmentHelperStorage, new()
        where ControllerSuppliedType : CardAlignmentHelperStorage, new()
    {
        // Isn't a TurnTaker.
        public NoChangeType Card()
        {
            isCard = true;
            var ret = new NoChangeType();
            ret.CopyFrom(this);
            return ret;
        }

        // Is sourced from a TurnTaker. Excludes cards.
        public NoChangeType Noncard()
        {
            isCard = false;
            var ret = new NoChangeType();
            ret.CopyFrom(this);
            return ret;
        }

        // Is specifically a target. Excludes TurnTakers as well as non-target cards.
        public NoChangeType Target()
        {
            target = CardTarget.Target;
            var ret = new NoChangeType();
            ret.CopyFrom(this);
            return ret;
        }

        // Is specifically not a target. Either a TurnTaker or a non-target card.
        public NoChangeType NonTarget()
        {
            target = CardTarget.Nontarget;
            var ret = new NoChangeType();
            ret.CopyFrom(this);
            return ret;
        }

        // Is specifically a character card. Excludes TurnTakers.
        public NoChangeType Character()
        {
            character = true;
            var ret = new NoChangeType();
            ret.CopyFrom(this);
            return ret;
        }

        // Is specifically a non-character card. Excludes TurnTakers.
        public NoChangeType Noncharacter()
        {
            character = false;
            var ret = new NoChangeType();
            ret.CopyFrom(this);
            return ret;
        }

        // Is specifically Hero.
        // If Target() is specified, a Hero target (uses targetKind). Otherwise, a Hero card or TurnTaker.
        public NeedControllerType Hero()
        {
            alignment = CardAlignment.Hero;
            var ret = new NeedControllerType();
            ret.CopyFrom(this);
            return ret;
        }

        // Is specifically Environment.
        // If Target() is specified, an Environment target (uses targetKind). Otherwise an Environment card or TurnTaker.
        public NoChangeType Environment()
        {
            alignment = CardAlignment.Environment;
            var ret = new NoChangeType();
            ret.CopyFrom(this);
            return ret;
        }

        // Is specifically Villain.
        // If Target() is specified, a Villain target (uses targetKind). Otherwise a Villain card or TurnTaker.
        //
        // Requires that a controller is passed via AccordingTo or the parameter to Is; that's the controller
        // that wants to know whether a card is a villain. It gets passed to AskCardControllersIfIsVillain.
        public NeedControllerType Villain()
        {
            alignment = CardAlignment.Villain;
            var ret = new NeedControllerType();
            ret.CopyFrom(this);
            return ret;
        }

        // Is specifically non-hero.
        // If Target() is specified, a non-hero target (uses targetKind). Otherwise a non-hero card or TurnTaker.
        //
        // Note that this doesn't call AskCardControllersIfIsVillain; this is consistent with Handleabra's usage.
        // That might be because they only use that mechanism for Maze of Mirrors, which wouldn't affect this
        // usage. It's not clear what the 'correct' behaviour would be.
        public NeedControllerType NonHero()
        {
            alignment = CardAlignment.Nonhero;
            var ret = new NeedControllerType();
            ret.CopyFrom(this);
            return ret;
        }

        // Is specifically non-environment.
        // If Target() is specified, a non-environment target (uses targetKind). Otherwise a non-environment card or TurnTaker.
        //
        // Note that this doesn't call AskCardControllersIfIsVillain; this is consistent with Handleabra's usage.
        // That might be because they only use that mechanism for Maze of Mirrors, which wouldn't affect this
        // usage. It's not clear what the 'correct' behaviour would be.
        public NoChangeType NonEnvironment()
        {
            alignment = CardAlignment.Nonenvironment;
            var ret = new NoChangeType();
            ret.CopyFrom(this);
            return ret;
        }

        // Is specifically non-villain.
        // If Target() is specified, a non-villain target (uses targetKind).
        //
        // Requires that a controller is passed via AccordingTo or the parameter to Is; that's the controller
        // that wants to know whether a card is a villain. It gets passed to AskCardControllersIfIsVillain.
        public NeedControllerType NonVillain()
        {
            alignment = CardAlignment.Nonvillain;
            var ret = new NeedControllerType();
            ret.CopyFrom(this);
            return ret;
        }

        // Adds a keyword that the card must have (e.g., Ongoing).
        // Excludes TurnTakers (as they don't have keywords).
        //
        // A CardController must be specified with AccordingTo or Is because of implementation limitations.
        public NeedControllerType WithKeyword(string keyword)
        {
            expectedKeywords.Add(keyword);
            var ret = new NeedControllerType();
            ret.CopyFrom(this);
            return ret; 
        }

        // Adds a keyword that the card musn't have (i.e. "non-Citizen")
        // Excludes TurnTakers.
        //
        // A CardController must be specified with AccordingTo or Is because of implementation limitations.
        public NeedControllerType WithoutKeyword(string keyword)
        {
            unwantedKeywords.Add(keyword);
            var ret = new NeedControllerType();
            ret.CopyFrom(this);
            return ret;
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
        public ControllerSuppliedType AccordingTo(CardController _controller)
        {
            controller = _controller;
            var ret = new ControllerSuppliedType();
            ret.CopyFrom(this);
            return ret;
        }
    };

    public class CardAlignmentHelperWithController : CardAlignmentHelperBase<CardAlignmentHelperWithController, CardAlignmentHelperWithController, CardAlignmentHelperWithController>
    {
        public CardAlignmentHelperWithController() { }

        internal CardAlignmentHelperWithController(Card _card, CardController _controller)
        {
            card = _card;
            controller = _controller;
        }

        internal CardAlignmentHelperWithController(TurnTaker _turntaker, CardController _controller)
        {
            turntaker = _turntaker;
            controller = _controller;
        }

        // Indicates whether or not the Card/TurnTaker 'helper' was constructed on meets the requirements specified.
        public static implicit operator bool(CardAlignmentHelperWithController helper)
        {
            return helper.ConvertToBool();
        }
    };

    public class CardAlignmentHelperNeedsController : CardAlignmentHelperBase<CardAlignmentHelperNeedsController, CardAlignmentHelperNeedsController, CardAlignmentHelperWithController>
    {

    };

    public class CardAlignmentHelperImpl : CardAlignmentHelperBase<CardAlignmentHelperImpl, CardAlignmentHelperNeedsController, CardAlignmentHelperWithController>
    {
        public CardAlignmentHelperImpl() { }

        internal CardAlignmentHelperImpl(Card _card)
        {
            card = _card;
        }

        internal CardAlignmentHelperImpl(TurnTaker _turntaker)
        {
            turntaker = _turntaker;
        }

        // Indicates whether or not the Card/TurnTaker 'helper' was constructed on meets the requirements specified.
        public static implicit operator bool(CardAlignmentHelperImpl helper)
        {
            return helper.ConvertToBool();
        }
    };

    
}
