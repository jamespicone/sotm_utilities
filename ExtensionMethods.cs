using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Handelabra.Sentinels.Engine.Controller;
using Handelabra.Sentinels.Engine.Model;

namespace Jp.SOTMUtilities
{
    static class ExtensionMethods
    {
        public static CardAlignmentHelper Is(this Card c, CardController controller = null)
        {
            return new CardAlignmentHelper(c, controller);
        }

        public static CardAlignmentHelper Is(this TurnTaker t, CardController controller = null)
        {
            return new CardAlignmentHelper(t, controller);
        }

        public static CardAlignmentHelper Is(this TurnTakerController t, CardController controller = null)
        {
            return new CardAlignmentHelper(t.TurnTaker, controller);
        }

        public static CardAlignmentHelper Is(this DamageSource source, CardController controller = null)
        {
            if (source.IsCard)
            {
                return source.Card.Is(controller);
            }
            else
            {
                return source.TurnTaker.Is(controller);
            }
        }
    }
}
