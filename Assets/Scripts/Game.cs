using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace CardClash
{
    public class Game : MonoBehaviour
    {
        enum GameState { Idle, GameStarted, TurnStarted, WaitingForChallenge, ChallengeSubmitted, GameEnded }

        public GameObject cardDeckPrefab;

        private GameState gameState;
        private CardDeck cardDeck;
        private Player[] players;

        private int turnPlayer;

        // Start is called before the first frame update
        void Start()
        {
            gameState = GameState.GameStarted;
            StartCoroutine(CreateDeckAndDeal());
            // TESTS: change Player.WinningHand to public
            // Debug.Log(Player.WinningHand(new int[] { 0, 1, 3, 1, 0, 0, 1, 1, 1, 1, 1 })); // [2, 2], [1, 2, 3], [6, 7, 8]
            // Debug.Log(Player.WinningHand(new int[] { 0, 1, 1, 0, 1, 2, 1, 1, 0, 0, 3 })); // null
            // Debug.Log(Player.WinningHand(new int[] { 0, 1, 1, 0, 1, 2, 1, 1, 1, 1, 3 })); // [10, 10], [4, 5, 6], [7, 8, 9]
            // Debug.Log(Player.WinningHand(new int[] { 0, 1, 2, 1, 0, 2, 1, 1, 1, 1, 1 })); // [2, 2], [5, 6, 7], [8, 9, 10]
        }

        // Update is called once per frame
        void Update()
        {
            // Debug.Log("Game state: " + gameState);

            // check game state
            // if playing:
            //   running through rounds. each round, each player gets a turn
            //   a card is flipped from the deck. the other player gets a chance to challenge (a button to say all sacrificed cards are selected)
            //   if no challenge, continue. if challenge, roll die, continue.
            // after each action, check if either player won
            // if won, end game.
            if (gameState == GameState.TurnStarted) {
                cardDeck.FlipTopCard();
                players[turnPlayer].SetChallenging(true);
                players[turnPlayer ^ 1].SetChallenging(false);
                gameState = GameState.WaitingForChallenge;
            } else if (gameState == GameState.WaitingForChallenge) {
                // keep waiting, do nothing, maybe delete this chunk?
                // gameState set to ChallengeSubmitted when players click a button
            } else if (gameState == GameState.ChallengeSubmitted) {
                // collect sacrificed cards, roll die and give card to someone
                // check if someone won! if so set gameState to GameEnded
                string player0WinningHand = players[0].WinningHand();
                string player1WinningHand = players[1].WinningHand();
                if (player0WinningHand == null && player1WinningHand == null) {
                    // game continues, set game state
                    gameState = GameState.TurnStarted;
                    turnPlayer ^= 1;
                } else {
                    gameState = GameState.GameEnded;
                    if (player0WinningHand != null && player1WinningHand == null) {
                        // player 0 won
                    } else if (player0WinningHand == null && player1WinningHand != null) {
                        // player 1  won
                    } else {
                        // tied
                    }
                }
            } else if (gameState == GameState.GameEnded) {
                // goes to ending screen, ask if they want to play again
                // since not on this scene anymore, this object is gone
            }
        }

        IEnumerator CreateDeckAndDeal()
        {
            GameObject cardDeckObj = Instantiate(cardDeckPrefab, new Vector3(0, 0, 0), Quaternion.identity);
            cardDeck = (CardDeck) cardDeckObj.GetComponent(typeof(CardDeck));
            players = new Player[] { new Player(0, new Vector2(-7, -3), new Vector2(-7, -1)), 
                                     new Player(1, new Vector2(3, -3), new Vector2(3, -1)) };
            yield return null; // waits for shuffling to finish
            cardDeck.Deal(players);
            yield return null;
            turnPlayer = 0;
            gameState = GameState.TurnStarted;
        }
    }
}