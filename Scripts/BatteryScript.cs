using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Globalization;

namespace DeepDive
{
    public class BatteryScript : MonoBehaviour
    {
        IOScript scriptInstance = null; //instance creation
        public Slider slider;
        public Gradient gradient;
        public Image fill;
        public Text batteryLabel;
        public string batteryval;
        float battery = 75;

        void Awake()
        {
            GameObject tempObj = GameObject.Find("IOController");
            scriptInstance = tempObj.GetComponent<IOScript>();

            //Access io_dict variable from IoScript
            batteryval = scriptInstance.Io_dict["Battery"];
            battery = float.Parse(batteryval, CultureInfo.InvariantCulture.NumberFormat); //string to float
            getBattery();
        }
        public void getBattery() //calls to io controller for battery info
        {
            battery = float.Parse(batteryval, CultureInfo.InvariantCulture.NumberFormat); //string to float
            batteryval = scriptInstance.Io_dict["Battery"];//battery = battery - .001f;
            slider.value = battery;
            batteryLabel.text = batteryval + (" %");
            fill.color = gradient.Evaluate(slider.normalizedValue);
        }

        // Update is called once per frame
        void Update()
        {
            getBattery();
        }
    }
}