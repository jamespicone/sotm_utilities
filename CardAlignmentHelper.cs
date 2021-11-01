using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Handelabra.Sentinels.Engine.Controller;
using Handelabra.Sentinels.Engine.Model;

namespace Jp.SOTMUtilities
{
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

    public enum CardTarget
    {
        Either,
        Target,
        Nontarget
    };

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

        public CardAlignmentHelper Target()
        {
            target = CardTarget.Target;
            return this;
        }

        public CardAlignmentHelper NonTarget()
        {
            target = CardTarget.Nontarget;
            return this;
        }

        public CardAlignmentHelper Character()
        {
            character = true;
            return this;
        }

        public CardAlignmentHelper Noncharacter()
        {
            character = false;
            return this;
        }

        public CardAlignmentHelper Hero()
        {
            alignment = CardAlignment.Hero;
            return this;
        }

        public CardAlignmentHelper Environment()
        {
            alignment = CardAlignment.Environment;
            return this;
        }

        public CardAlignmentHelper Villain()
        {
            alignment = CardAlignment.Villain;
            return this;
        }

        public CardAlignmentHelper NonHero()
        {
            alignment = CardAlignment.Nonhero;
            return this;
        }

        public CardAlignmentHelper NonEnvironment()
        {
            alignment = CardAlignment.Nonenvironment;
            return this;
        }

        public CardAlignmentHelper NonVillain()
        {
            alignment = CardAlignment.Nonvillain;
            return this;
        }

        public CardAlignmentHelper AccordingTo(CardController _controller)
        {
            controller = _controller;
            return this;
        }

        public static implicit operator bool(CardAlignmentHelper helper)
        {
            if (!helper.alignment.HasValue)
            {
                throw new InvalidOperationException("CardAlignmentHelper without alignment converted to bool");
            }

            if (helper.card != null)
            {
                bool hasAlignment = true;
                if (helper.alignment.HasValue)
                {
                    var baseAlignment = (CardAlignment)(((int)helper.alignment.Value) & ~1);
                    switch(baseAlignment)
                    {
                        case CardAlignment.Hero:
                            if (helper.target == CardTarget.Target) {
                                hasAlignment = helper.card.TargetKind == DeckDefinition.DeckKind.Hero ||
                                    (! helper.card.TargetKind.HasValue && helper.card.IsHero);
                            }
                            else { hasAlignment = helper.card.IsHero; }
                            break;
                        case CardAlignment.Villain:
                            if (helper.target == CardTarget.Target) {
                                hasAlignment = helper.controller.GameController.AskCardControllersIfIsVillainTarget(helper.card, helper.controller.GetCardSource());
                            }
                            else { hasAlignment = helper.controller.GameController.AskCardControllersIfIsVillain(helper.card, helper.controller.GetCardSource()); }
                            break;
                        case CardAlignment.Environment:
                            if (helper.target == CardTarget.Target) { hasAlignment = helper.card.IsEnvironmentTarget; }
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

                return hasAlignment && (helper.character == null || helper.character.Value == helper.card.IsCharacter);
            }

            if (helper.turntaker != null)
            {
                if (helper.target == CardTarget.Target)
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
    }
}
