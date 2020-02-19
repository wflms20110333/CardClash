using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace CardClash
{
    public class Game : MonoBehaviour
    {
        enum GameState { Idle, GameStarted, Playing, GameEnded }

        public GameObject cardDeckPrefab;

        private GameState gameState;
        private CardDeck cardDeck;
        private Player[] players;

        // Start is called before the first frame update
        void Start()
        {
            gameState = GameState.GameStarted;
            StartCoroutine(CreateDeckAndDeal());
        }

        // Update is called once per frame
        void Update()
        {
            // check game state
            // if playing:
            //   one of the player's turns, on action switch to other player's turn
            // after each action, check if either player won
            // if won, end game.
        }

        IEnumerator CreateDeckAndDeal()
        {
            GameObject cardDeckObj = Instantiate(cardDeckPrefab, new Vector3(0, 0, 0), Quaternion.identity);
            cardDeck = (CardDeck) cardDeckObj.GetComponent(typeof(CardDeck));
            players = new Player[] { new Player(), new Player() };
            players[0].PlayerId = 0;
            players[1].PlayerId = 1;
            players[0].PublicCardPosition = new Vector2(-7, -3);
            players[1].PublicCardPosition = new Vector2(3, -3);
            players[0].HiddenCardPosition = new Vector2(-7, -1);
            players[1].HiddenCardPosition = new Vector2(3, -1);
            yield return null; // waits for shuffling to finish
            cardDeck.Deal(players);
        }
    }
}