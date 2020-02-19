using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardDeck : MonoBehaviour
{
    public GameObject cardPrefab;
	private Stack<Card> deck;

    // Start is called before the first frame update
    void Start()
    {
        deck = new Stack<Card>();
        CreateCard(Card.CardType.Card3);
    }

    void CreateCard(Card.CardType cardType)
    {
        GameObject card = Instantiate(cardPrefab, new Vector3(0, 0, 5), Quaternion.identity);
        Card cardScript = (Card) card.GetComponent(typeof(Card));
        cardScript.SetProperties(cardType, true);
        deck.Push(cardScript);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
