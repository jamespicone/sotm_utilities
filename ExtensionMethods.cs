using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Handelabra.Sentinels.Engine.Controller;
using Handelabra.Sentinels.Engine.Model;

namespace Jp.SOTMUtilities
{
    public static class ExtensionMethods
    {
        // Helper for checking card properties.
        // You can pass a CardController to use when checking card properties such as villain-ness
        // or card keywords.
        public static CardAlignmentHelper Is(this Card c, CardController controller = null)
        {
            return new CardAlignmentHelper(c, controller);
        }

        // Helper for checking card properties.
        // You can pass a CardController to use when checking card properties such as villain-ness
        // or card keywords.
        public static CardAlignmentHelper Is(this CardController c, CardController controller = null)
        {
            return new CardAlignmentHelper(c.Card, controller);
        }

        // Helper for checking TurnTaker properties.
        // You can pass a CardController to use when checking villain-ness
        public static CardAlignmentHelper Is(this TurnTaker t, CardController controller = null)
        {
            return new CardAlignmentHelper(t, controller);
        }

        // Helper for checking TurnTaker properties.
        // You can pass a CardController to use when checking villain-ness
        public static CardAlignmentHelper Is(this TurnTakerController t, CardController controller = null)
        {
            return new CardAlignmentHelper(t.TurnTaker, controller);
        }

        // Helper for checking card/turntaker properties.
        // You can pass a CardController to use when checking card properties such as villain-ness
        // or card keywords.
        //
        // This will work with Card or TurnTaker damage sources.
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

        public static IEnumerator ShowVersionMessage(this CardController co)
        {
            return co.GameController.SendMessageAction(
                "Version 1",
                Priority.Critical,
                co.GetCardSource()
            );
        }
    }
}
