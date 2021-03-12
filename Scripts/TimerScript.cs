using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Globalization;


namespace DeepDive { 
public class TimerScript : MonoBehaviour
{
    float timeRemaining = 9000;
    public bool timerIsRunning = false;
    public Text timerText;
    IOScript scriptInstance = null; //instance creation
    public string timerval;

        void Awake()
        {
            GameObject tempObj = GameObject.Find("IOController");
            scriptInstance = tempObj.GetComponent<IOScript>();

            //Access io_dict variable from IoScript
            timerval = scriptInstance.Io_dict["Timer"];
            timeRemaining = float.Parse(timerval, CultureInfo.InvariantCulture.NumberFormat);  //sets timer value based on user input via iodict
        }
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

            timerText.text = "Time Remaining: \n"+ string.Format("{0:00}:{1:00}", minutes, seconds);
        }
    }
}
