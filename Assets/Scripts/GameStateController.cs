using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateController : MonoBehaviour
{
    public GameObject gameObjectUi;

    public int gameState;

    public void HandleState(int state, float time)
    {
        gameState = state;
        gameObjectUi.GetComponent<GameState>().HandleStateUi(state, time);
    }

    public void SetGameState(int state)
    {
        gameState = state;
        gameObjectUi.GetComponent<GameState>().SetGameState(state);
    }

    public int GetGameState()
    {
        return gameObjectUi.GetComponent<GameState>().GetGameState();
    }
}
