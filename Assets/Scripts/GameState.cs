using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameState : MonoBehaviour
{
    private int gameState = 0;

    public void Update()
    {
        if (gameState == 0)
        {
            gameObject.SetActive(false);
        }
    }

    public void SetGameState(int state)
    {
        gameState = state;
    }

    public int GetGameState()
    {
        return gameState;
    }

    public void HandleStateUi(int state, float time)
    {
        if (state == -1)
        {
            gameObject.SetActive(true);
            gameState = state;
            GetComponentInChildren<Text>().text = "Game Over";
        }

        if (state == 1)
        {
            TimeSpan t = TimeSpan.FromSeconds((int)time);

            gameObject.SetActive(true);
            gameState = state;
            GetComponentInChildren<Text>().text = "C'est gagné !\nVotre avance : " + t.ToString();
        }
    }
}
