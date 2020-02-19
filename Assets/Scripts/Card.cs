using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

namespace CardClash
{
    public class Card : MonoBehaviour
    {
        SpriteRenderer renderer;

        BoxCollider2D collider;
        [SerializeField]
        SpriteAtlas atlas;

        // The type that should be drawn on screen
        [SerializeField]
        CardType currentType;

        // The type drawn on screen
        CardType drawnType;

        // The type of the card when face up
        [SerializeField]
        CardType faceType;

        bool hidden;
        int ownerId;

        // Start is called before the first frame update
        void Start()
        {
            // Debug.Log("In Start of Card.cs");
            renderer = GetComponent<SpriteRenderer>();
            collider = GetComponent<BoxCollider2D>();
            currentType = CardType.CardBack;
            drawnType = CardType.None;
            transform.localScale = new Vector3(Constants.CARD_SCALE, Constants.CARD_SCALE, 1);
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
                // Debug.Log("Redrawing");
                renderer.sprite = atlas.GetSprite(currentType.ToString());
                drawnType = currentType;
                // if (drawnType == CardType.None)
                //     collider.size = new Vector2(0, 0);
                // else
                //     collider.size = new Vector2(renderer.sprite.rect.width, renderer.sprite.rect.height);
                // collider.bounds.size = renderer.bounds.size;
                // collider.bounds.Encapsulate(renderer.bounds.size);
                Vector2 S = renderer.sprite.bounds.size;
                collider.size = S;
                collider.center = new Vector2 ((S.x / 2), 0);
            }
        }

        void OnMouseDown()
        {
            Debug.Log("Mouse clicked");
            Debug.Log(transform.position.x + " " + transform.position.y);
            Debug.Log(collider.transform.position.x + " " + collider.transform.position.y);
            Debug.Log(renderer.bounds.size);
            Debug.Log(collider.bounds.size);
            Debug.Log(collider.size);

            FlipCard();
        }

        public void SetCardType(CardType cardType)
        {
            faceType = cardType;
        }

        public void SetHidden(bool hidden)
        {
            this.hidden = hidden;
        }

        public void SetOwnerId(int ownerId)
        {
            this.ownerId = ownerId;
        }

        // Attempts to flip a card.
        public void FlipCard()
        {
            SetFaceUp(currentType != faceType);
        }

        public void SetFaceUp(bool up)
        {
            if (!up && !hidden) {
                Debug.Log("Cannot flip a card that is public to face down");
                return;
            }
            currentType = up ? faceType : CardType.CardBack;
            UpdateSprite();
        }

        public void MoveTo(Vector2 newPosition)
        {
            transform.position = new Vector3(newPosition.x, newPosition.y, transform.position.z);
            // TODO: animate!
        }
    }
}