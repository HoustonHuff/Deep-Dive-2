using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DeepDive
{
    public class AirScript : MonoBehaviour
    {
        IOScript scriptInstance = null; //instance creation
        public Text textAir;
        float air = 58;
        public string airval;
        void Awake()
        {
            GameObject tempObj = GameObject.Find("IOController");
            scriptInstance = tempObj.GetComponent<IOScript>();

            //Access io_dict variable from IoScript
            airval = scriptInstance.Io_dict["Air"]; 

            getAir();
        }
        void getAir()
        {
            //if (air> 44.1)
            //{
            //    air = air - .01f;//call to io controller for sensor data
            //}
            //used for demo
            //return air;
            airval = scriptInstance.Io_dict["Air"];

            textAir.text = "Air Pressure: \n" + airval + " psi";

        }
        // Update is called once per frame
        void Update()
        {
            getAir();

        }
    }
}
