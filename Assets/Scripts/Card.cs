﻿using System.Collections;
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
        int ownerId;

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

        void OnMouseDown()
        {
            FlipCard();
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

        public void SetOwnerId(int ownerId)
        {
            this.ownerId = ownerId;
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

        public void SetOutline(bool outline)
        {
            spriteRenderer.material = outline ? cardOutlineMaterial : defaultMaterial;
        }
    }
}