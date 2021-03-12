using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DeepDive
{
    public class AlertScript : MonoBehaviour
    {
        public Text alertLabel;
        IOScript scriptInstance = null; //instance creation
        public string alertval;
        void Awake()
        {
            GameObject tempObj = GameObject.Find("IOController");
            scriptInstance = tempObj.GetComponent<IOScript>();

            //Access io_dict variable from IoScript
            alertval = scriptInstance.Io_dict["Alert"];
            getAlert();
        }
        public void getAlert()
        {
            alertval = scriptInstance.Io_dict["Alert"];
            alertLabel.text = alertval;
        }


        // Update is called once per frame
        void Update()
        {
            getAlert();
        }
    }
}