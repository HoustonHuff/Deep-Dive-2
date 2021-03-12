using System;
using System.Threading;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DeepDive;

namespace DeepDive
{
        public class Ascension : Hud
    {
        public string message;
        public float depth = 0;
        public float prevDepth;
        public float maxDepth;

        //used only in the demo to help recalculate depth
        public float yValue;

        //Object reference to IOScript to get data
        IOScript scriptInstance = null;

        public override void Start()
        {
            GameObject tempObj = GameObject.Find("IOController");
            scriptInstance = tempObj.GetComponent<IOScript>();

            //Access io_dict variable from IoScript
           depth = float.Parse(scriptInstance.Io_dict["Depth"]);
        }
        
        public override void Update()
        {
            //update depth variables
            prevDepth = depth;

            //This logic is only used for the demo and must be tweaked for actual application
            yValue = float.Parse(scriptInstance.Io_dict["Depth"]);
            depth = (yValue - 125);

            //Actual Logic
            //depth = float.Parse(scriptInstance.Io_dict["Depth"]);


            if (depth < maxDepth)
            {
                maxDepth = depth;
            }

            //Compare depths to determine if a user is ascending then send a message to the IOScript if needed
            if (prevDepth<=depth && depth>-20)
            {
                message = "Alert: Stop here for a final safety stop for 5 min.";
                scriptInstance.Io_dict["Alert"] = message;
                //Console.Writeline(message);
            }
            else if (depth >= maxDepth/2)
            {
                message = "Alert: Stop here for a mid way safety stop for a minute.";
                scriptInstance.Io_dict["Alert"] = message;
                //Console.Writeline(message);
            }
            //Debug message
            //else
            //{
                //message = "Resume diving.";
                //Console.Writeline(message);
            //}
        }
    }
}