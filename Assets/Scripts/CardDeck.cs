using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardDeck : MonoBehaviour
{
    private static System.Random rng = new System.Random();

    private const int NUM_NUMBER_CARDS = 4;
    private const int NUM_BATTLE_CARDS = 12;
    private const int TOTAL_CARDS = NUM_NUMBER_CARDS * 10 + NUM_BATTLE_CARDS;

    public GameObject cardPrefab;
	private Stack<Card> deck;

    // Start is called before the first frame update
    void Start()
    {
        deck = new Stack<Card>();
        foreach (Card.CardType cardType in System.Enum.GetValues(typeof(Card.CardType))) {
            if (cardType == Card.CardType.None || cardType == Card.CardType.CardBack)
                continue;
            else if (cardType == Card.CardType.CardBattle)
                CreateCards(cardType, NUM_BATTLE_CARDS);
            else
                CreateCards(cardType, NUM_NUMBER_CARDS);
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

    private void CreateCards(Card.CardType cardType, int count)
    {
        while (count-- > 0)
            CreateCard(cardType);
    }

    private void CreateCard(Card.CardType cardType)
    {
        GameObject cardObj = Instantiate(cardPrefab, new Vector3(0, 0, 5), Quaternion.identity);
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
        return TOTAL_CARDS - i;
    }
}
