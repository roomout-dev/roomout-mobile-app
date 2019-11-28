using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

#pragma warning disable 618
public class TimerController : MonoBehaviour
#pragma warning restore 618
{
    /// <summary>
    /// Test if the time can run
    /// </summary>
    public bool canPlay { get; private set; }

    /// <summary>
    /// In seconds, time left
    /// </summary>
    public float time;

    /// <summary>
    /// For displaying in the text component
    /// </summary>
    public Text timerText;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        TimeSpan t = TimeSpan.FromSeconds(time);
        timerText.text = t.ToString();

        if (canPlay)
        {
            time -= Time.deltaTime;
            GetComponent<TimerNetwork>().CmdSetTimer(time);
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
