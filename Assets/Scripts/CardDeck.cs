using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CardClash
{
    public class CardDeck : MonoBehaviour
    {
        private static System.Random rng = new System.Random();

        public GameObject cardPrefab;
        private Stack<Card> deck;

        // Start is called before the first frame update
        void Start()
        {
            deck = new Stack<Card>();
            foreach (CardType cardType in System.Enum.GetValues(typeof(CardType))) {
                if (cardType == CardType.None || cardType == CardType.CardBack)
                    continue;
                else if (cardType == CardType.CardBattle)
                    CreateCards(cardType, Constants.NUM_BATTLE_CARDS);
                else
                    CreateCards(cardType, Constants.NUM_NUMBER_CARDS);
            }
            ShuffleDeck();
        }

        // Update is called once per frame
        void Update()
        {
            
        }

        // Shuffles the deck of cards. Modifier method.
        public void ShuffleDeck()
        {
            Debug.Log("Shuffling the deck");
            int n = deck.Count;
            Card[] arr = new Card[n];
            for (int i = 0; i < n; i++)
                arr[i] = deck.Pop();
            while (n-- > 1) {
                int k = rng.Next(n + 1);
                Card value = arr[k];
                arr[k] = arr[n];
                arr[n] = value;
            }
            foreach (Card card in arr)
                AddToDeck(card);
        }

        public void Deal(Player[] players)
        {
            Debug.Log("Dealing initial hand to players");
            for (int i = 0; i < Constants.INITIAL_HIDDEN_CARDS; i++)
                foreach (Player player in players)
                    DealCard(player, true);
            for (int i = 0; i < Constants.INITIAL_PUBLIC_CARDS; i++)
                foreach (Player player in players)
                    DealCard(player, false);
        }

        public void DealCard(Player player, bool hidden)
        {
            Debug.Log("Dealing a " + (hidden ? "hidden" : "public") + " card to player " + player.PlayerId);
            Card card = deck.Pop();
            if (hidden) {
                Vector2 newPosition = player.NextHiddenCardPosition();
                card.MoveTo(newPosition);
                player.receiveHiddenCard(card);
            } else {
                Vector2 newPosition = player.NextPublicCardPosition();
                card.MoveTo(newPosition);
                player.receivePublicCard(card);
            }
        }

        private void CreateCards(CardType cardType, int count)
        {
            while (count-- > 0)
                CreateCard(cardType);
        }

        private void CreateCard(CardType cardType)
        {
            GameObject cardObj = Instantiate(cardPrefab, new Vector3(0, 0, 0), Quaternion.identity);
            Card card = (Card) cardObj.GetComponent(typeof(Card));
            card.SetCardType(cardType);
            AddToDeck(card);
        }

        // Adds a card to the deck, setting the Z position at the same time.
        private void AddToDeck(Card card)
        {
            card.transform.position = new Vector3(card.transform.position.x, card.transform.position.y, StackIndexToZ(deck.Count));
            deck.Push(card);
        }

        private int StackIndexToZ(int i)
        {
            return Constants.TOTAL_CARDS - i;
        }
    }
}