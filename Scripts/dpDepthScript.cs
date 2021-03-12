using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;//ui stuffs
using System.Globalization; //this does something..somewhere 
using System;

namespace DeepDive
{


public class dpDepthScript : MonoBehaviour
{
        public float depth;

        public Text textDepth;
        public Transform player;
        public string depthval;
        IOScript scriptInstance = null; //instance creation
        void Awake()
        {
            GameObject tempObj = GameObject.Find("IOController");
            scriptInstance = tempObj.GetComponent<IOScript>();

            //Access io_dict variable from IoScript
            depthval = scriptInstance.Io_dict["Depth"];

            depth = float.Parse(depthval, CultureInfo.InvariantCulture.NumberFormat); //string to float
            setDepth();
        }


        public void setDepth()
        {
            depthval = scriptInstance.Io_dict["Depth"];
            depth = float.Parse(depthval, CultureInfo.InvariantCulture.NumberFormat); //doing the same thing as before, string to float
            textDepth.text = "Current Depth: \n"+ depthval + " ft";
        }
        void Update()
        {
            setDepth();
        }
    }
}

