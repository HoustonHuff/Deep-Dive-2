using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Globalization;

public class dpDiveTimerScript : MonoBehaviour
{
    
        float time = 0;
        public bool timerIsRunning = false;
        public Text divetimerText;
        public string timerval;

        
        private void Start()
        {
            // Starts the timer automatically
            timerIsRunning = true;

        }

        void getTimer() //calls to io to get timer info
        {

                    time += Time.deltaTime;
                    DisplayTime(time);
              
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

            divetimerText.text = "Dive Time: \n" +string.Format("{0:00}:{1:00}", minutes, seconds);
        }
    }
