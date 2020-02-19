using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class Card : MonoBehaviour
{
    public enum CardType { None, CardBack, Card1, Card2, Card3, Card4, Card5, Card6, Card7, Card8, Card9, Card10, CardBattle }

    private const float CARD_SCALE = 0.2f;

    [SerializeField]
	private SpriteAtlas atlas;

    // The type that should be drawn on screen
	[SerializeField]
	private CardType currentType;

    // The type drawn on screen
	private CardType drawnType;

    // The type of the card when face up
    private CardType faceType;

	private bool hidden;

	private SpriteRenderer myRenderer;

    private BoxCollider2D collider;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("In Start of Card.cs");
        myRenderer = GetComponent<SpriteRenderer>();
        drawnType = CardType.None;
        collider = GetComponent<BoxCollider2D>();
        transform.localScale = new Vector3(CARD_SCALE, CARD_SCALE, 1);
    }

    // Update is called once per frame
    void Update()
    {
        UpdateSprite();
    }

    void UpdateSprite()
    {
    	if (currentType != drawnType)
    	{
            Debug.Log("Redrawing");
    		myRenderer.sprite = atlas.GetSprite(currentType.ToString());
    		drawnType = currentType;
            if (drawnType == CardType.None)
                collider.size = new Vector2(0, 0);
            else
                collider.size = new Vector2(myRenderer.sprite.rect.width, myRenderer.sprite.rect.height);
    	}
    }

    void OnMouseDown()
    {
        Debug.Log("Mouse clicked");
        FlipCard();
    }

    public void SetProperties(CardType cardType, bool hidden)
    {
        faceType = cardType;
        this.hidden = hidden;
    }

    // Returns whether or not the card is now face up.
    public bool FlipCard()
    {
        if (hidden) {
            bool faceUp = currentType == faceType;
            currentType = faceUp ? CardType.CardBack : faceType;
            UpdateSprite();
            return !faceUp;
        } else {
            Debug.Log("Cannot flip a card that is public");
            return true;
        }
    }
}
