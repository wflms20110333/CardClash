using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CardClash
{
    public class Player : System.IEquatable<Player>
    {
        int playerId;
        bool challenging;
        Vector2 publicCardPosition;
        Vector2 hiddenCardPosition;

        List<Card> publicCards;
        List<Card> hiddenCards;

        public Player(int playerId, Vector2 publicCardPosition, Vector2 hiddenCardPosition)
        {
            this.playerId = playerId;
            this.challenging = false;
            this.publicCardPosition = publicCardPosition;
            this.hiddenCardPosition = hiddenCardPosition;
            this.publicCards = new List<Card>();
            this.hiddenCards = new List<Card>();
        }

        public int GetPlayerId()
        {
            return this.playerId;
        }

        public bool IsChallenging()
        {
            return challenging;
        }

        public void SetChallenging(bool challenging)
        {
            this.challenging = challenging;
        }

        public Vector2 NextPublicCardPosition()
        {
            return publicCardPosition + Vector2.right * Constants.PLAYER_PUBLIC_CARD_POSITION_OFFSET * publicCards.Count;
        }

        public Vector2 NextHiddenCardPosition()
        {
            return hiddenCardPosition + Vector2.right * Constants.PLAYER_HIDDEN_CARD_POSITION_OFFSET * hiddenCards.Count;
        }

        public void receivePublicCard(Card card)
        {
            publicCards.Add(card);
            card.SetOwner(this);
            card.SetHidden(false);
        }

        public void receiveHiddenCard(Card card)
        {
            hiddenCards.Add(card);
            card.SetOwner(this);
            card.SetHidden(true);
            card.SetFaceUp(false);
        }

        // Returns winning hand as a string, or null
        public string WinningHand()
        {
            int[] cardCounts = new int[11];
            int totalCount = 0;
            for (int i = 0; i < publicCards.Count; i++) {
                int cardValue = (int) publicCards[i].GetCardType();
                if (cardValue >= 1 && cardValue <= 10) {
                    cardCounts[cardValue]++;
                    totalCount++;
                }
            }
            for (int i = 0; i < hiddenCards.Count; i++) {
                int cardValue = (int) hiddenCards[i].GetCardType();
                if (cardValue >= 1 && cardValue <= 10) {
                    cardCounts[cardValue]++;
                    totalCount++;
                }
            }

            if (totalCount < 8)
                return null;
            return WinningHand(cardCounts);
        }

        // Returns winning hand as a string, or null
        // [2, 2], [5, 6, 7], [7, 8, 9]
        private static string WinningHand(int[] cardCounts)
        {
            for (int i = 1; i <= 10; i++) {
                if (cardCounts[i] < 2)
                    continue;
                cardCounts[i] -= 2;
                string straights = WinningStraights(cardCounts, 2);
                cardCounts[i] += 2;
                if (straights != null)
                    return "[" + i + ", " + i + "], " + straights.Substring(0, straights.Length - 2);
            }
            return null;
        }

        // Returns winning straights as a string, or null
        // [5, 6, 7], [7, 8, 9], 
        private static string WinningStraights(int[] cardCounts, int numStraights)
        {
            if (numStraights == 0)
                return "";
            for (int i = 1; i <= 8; i++) {
                if (cardCounts[i] == 0 || cardCounts[i + 1] == 0 || cardCounts[i + 2] == 0)
                    continue;
                cardCounts[i]--;
                cardCounts[i + 1]--;
                cardCounts[i + 2]--;
                string straights = WinningStraights(cardCounts, numStraights - 1);
                cardCounts[i]++;
                cardCounts[i + 1]++;
                cardCounts[i + 2]++;
                if (straights != null)
                    return "[" + i + ", " + (i + 1) + ", " + (i + 2) + "], " + straights;
            }
            return null;
        }

        public bool Equals(Player other)
        {
            return this.playerId == other.playerId;
        }
    }
}
