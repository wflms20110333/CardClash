using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CardClash
{
    public class CardAnimation
    {
        public Card card;
        Vector3 destination;
        Quaternion rotation;
 
        public CardAnimation(Card c, Vector3 pos)
        {
            card = c;
            destination = pos;
            rotation = Quaternion.identity;
        }

        public CardAnimation(Card c, Vector3 pos, Quaternion rot)
        {
            card = c;
            destination = pos;
            rotation = rot;
        }

        // Returns true if the animation has finished.
        public bool Play()
        {
            if (Vector2.Distance(card.transform.position, destination) < Constants.CARD_SNAP_DISTANCE) {
                card.transform.position = destination;
                if (!card.IsHidden())
                    card.SetFaceUp(true);
                return true;
            } else {
                card.transform.position = Vector3.MoveTowards(card.transform.position, destination, Constants.CARD_MOVEMENT_SPEED * Time.deltaTime);
                card.transform.rotation = Quaternion.Lerp(card.transform.rotation, rotation, Constants.CARD_ROTATION_SPEED * Time.deltaTime);
                return false;
            }
        }
    }
}