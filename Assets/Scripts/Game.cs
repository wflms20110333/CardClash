using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    enum GameState { None, Start, Playing, End }

    private GameState gameState;

    // Start is called before the first frame update
    void Start()
    {
        gameState = GameState.Start;
        // shuffle deck
        // deal cards
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
}
