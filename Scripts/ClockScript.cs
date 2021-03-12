using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

namespace DeepDive
{
    public class ClockScript : MonoBehaviour
    {
        public Text textClock;
        IOScript scriptInstance = null; //instance creation
        public string clockval;
        void Awake()
        {
            GameObject tempObj = GameObject.Find("IOController");
            scriptInstance = tempObj.GetComponent<IOScript>();

            //Access io_dict variable from IoScript
            clockval = scriptInstance.Io_dict["Clock"];
            getCurrent();
        }
        void getCurrent() //call to io controller for time 
        {
            //DateTime time = DateTime.Now;
            //string hour = LeadingZero(time.Hour);
            //string minute = LeadingZero(time.Minute);
            //string second = LeadingZero(time.Second); //used for logs if not all the time
            //string am_pm = DateTime.Now.ToString("tt");
            //string timetext = hour + ":" + minute + ":" + second + " " + am_pm;
            textClock.text = "Current Time: \n"+ clockval;
        }
        // Update is called once per frame
        void Update()
        {
            clockval = scriptInstance.Io_dict["Clock"];
            getCurrent();

        }
        //string LeadingZero(int n)
        //{
        //    return n.ToString().PadLeft(2, '0');
        //}
    }
}
