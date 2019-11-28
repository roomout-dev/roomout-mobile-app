using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

#pragma warning disable 618
public class TimerController : NetworkBehaviour
#pragma warning restore 618
{
    /// <summary>
    /// Test if the time can run
    /// </summary>
    public bool canPlay { get; private set; }

    /// <summary>
    /// The total time as the game started
    /// </summary>
    public float startingTime;

    /// <summary>
    /// In seconds, time left
    /// </summary>
#pragma warning disable 618
    [SyncVar] public float time;
#pragma warning restore 618

#pragma warning disable 618
    [SyncVar] public int gameState;
#pragma warning restore 618
    /// <summary>
    /// For displaying in the text component
    /// </summary>
    public Text timerText;

    // Start is called before the first frame update
    void Start()
    {
    }

    public override void OnStartClient()
    {
        timerText = GameObject.Find("TextTime").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        // Check GameState
        int currentGameState = GameObject.Find("GameState").GetComponent<GameStateController>().GetGameState();

        // Game State Handling
        if (currentGameState == 1)
        {
            GameObject.Find("GameState").GetComponent<GameStateController>().HandleState(1, time);
            canPlay = false;
            gameState = 1;
        }

        // Time Handling
        TimeSpan t = TimeSpan.FromSeconds((int)time);
        timerText.text = t.ToString();

        if (canPlay)
        {
            time -= Time.deltaTime;
        }

        if (time < 0)
        {
            canPlay = false;
            gameState = -1;
            GameObject.Find("GameState").GetComponent<GameStateController>().HandleState(-1, 0);
        }
    }

    /// <summary>
    /// Set the timer in seconds
    /// </summary>
    /// <param name="timer"></param>
    public void SetTimer(float timer)
    {
        time = timer;
    }

    /// <summary>
    /// Start the timer
    /// </summary>
    public void StartTimer()
    {
        canPlay = true;
    }

    /// <summary>
    /// Stop the timer
    /// </summary>
    public void StopTimer()
    {
        canPlay = false;
    }

    public void SetText(Text text)
    {
        timerText = text;
    }
}
