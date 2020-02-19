using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CardClash
{
    public class Player : System.IEquatable<Player>
    {
        public int PlayerId;
        public Vector2 PublicCardPosition;
        public Vector2 HiddenCardPosition;

        public List<Card> PublicCards = new List<Card>();
        public List<Card> HiddenCards = new List<Card>();

        public Vector2 NextPublicCardPosition()
        {
            return PublicCardPosition + Vector2.right * Constants.PLAYER_PUBLIC_CARD_POSITION_OFFSET * PublicCards.Count;
        }

        public Vector2 NextHiddenCardPosition()
        {
            return HiddenCardPosition + Vector2.right * Constants.PLAYER_HIDDEN_CARD_POSITION_OFFSET * HiddenCards.Count;
        }

        public void receivePublicCard(Card card)
        {
            PublicCards.Add(card);
            card.SetOwnerId(PlayerId);
        }

        public void receiveHiddenCard(Card card)
        {
            HiddenCards.Add(card);
            card.SetOwnerId(PlayerId);
        }

        public bool Equals(Player other)
        {
            return false;
        }
    }
}
