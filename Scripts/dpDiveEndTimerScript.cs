using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Globalization;

public class dpDiveEndTimerScript : MonoBehaviour
{
    float timeRemaining = 3000;
    public bool timerIsRunning = false;
    public Text timerText;
    public string timerval;

    private void Start()
    {
        // Starts the timer automatically
        timerIsRunning = true;

    }

    void getTimer() //calls to io to get timer info
    {

        if (timerIsRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                DisplayTime(timeRemaining);
            }
            else
            {
                Debug.Log("Time has run out!");
                timeRemaining = 0;
                timerIsRunning = false;
            }
        }
    }

    void Update()
    {
        getTimer();
    }

    void DisplayTime(float timeToDisplay)
    {
        timeToDisplay += 1;

        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        timerText.text = "Estimated End of Dive Time: \n"+string.Format("{0:00}:{1:00}", minutes, seconds);
    }
    
}
