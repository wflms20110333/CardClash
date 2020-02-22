using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

namespace CardClash
{
    public class Card : MonoBehaviour
    {
        Material defaultMaterial;
        Material cardOutlineMaterial;
        SpriteRenderer spriteRenderer;

        BoxCollider2D boxCollider;
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
        Player owner;

        bool selected;

        // Start is called before the first frame update
        void Start()
        {
            defaultMaterial = new Material(Shader.Find("Sprites/Default"));
            cardOutlineMaterial = Resources.Load<Material>("Materials/CardOutline");
            spriteRenderer = GetComponent<SpriteRenderer>();
            boxCollider = GetComponent<BoxCollider2D>();
            currentType = CardType.CardBack;
            drawnType = CardType.None;
            transform.localScale = new Vector3(Constants.CARD_SCALE, Constants.CARD_SCALE, 1);
            selected = false;
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
                spriteRenderer.sprite = atlas.GetSprite(currentType.ToString());
                drawnType = currentType;
                if (drawnType == CardType.None)
                    boxCollider.size = new Vector2(0, 0);
                else
                    boxCollider.size = spriteRenderer.sprite.bounds.size;
            }
        }

        // Not challenging: flips card
        // Challenging, hidden cards: flip, then cycle of selection toggle
        // Challenging, public cards: cycle of selection toggle
        void OnMouseDown()
        {
            if (!owner.IsChallenging() || hidden && currentType == CardType.CardBack)
                FlipCard();
            else
                SetSelection(!selected);
        }

        public void SetCardType(CardType cardType)
        {
            this.faceType = cardType;
        }

        public CardType GetCardType()
        {
            return this.faceType;
        }

        public bool IsHidden()
        {
            return this.hidden;
        }

        public void SetHidden(bool hidden)
        {
            this.hidden = hidden;
        }

        public void SetOwner(Player owner)
        {
            this.owner = owner;
        }

        // Attempts to flip a card.
        private void FlipCard()
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

        public void SetSelection(bool select)
        {
            spriteRenderer.material = select ? cardOutlineMaterial : defaultMaterial;
            selected = select;
        }
    }
}