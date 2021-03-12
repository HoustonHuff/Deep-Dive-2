using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Globalization;
using System.Linq;

namespace DeepDive { 
public class IOScriptOld : MonoBehaviour
{
        public Transform player;
        public  Dictionary<string, string> Io_dict = new Dictionary<string, string>() {
            {"Air", "58.9"}, {"Depth", "132"}, {"Battery", "65"},{"Timer", "60"},{"Clock", "3:05:22 pm"}, {"X", "23"}, {"Y", "25"}, {"Alert", "Alert: Depth too low, please rise 60 ft"}
        };

        public void Start()
        {
            player = GameObject.Find("Mask").transform; 

        }

        void setBattery()
        {
            float battery = float.Parse(Io_dict["Battery"], CultureInfo.InvariantCulture.NumberFormat); //string to float
            battery = battery - .001f;
            Io_dict["Battery"] = battery.ToString("0.0");
        }
        void setX()
        {
            Io_dict["X"] = player.position.x.ToString();
        }
        void setY()
        {
            Io_dict["Y"] = player.position.z.ToString();
        }
        void setDepth()
        {
            Io_dict["Depth"] = player.position.y.ToString();
        }
        void setAir()
        {
            float air = float.Parse(Io_dict["Air"], CultureInfo.InvariantCulture.NumberFormat); //string to float
            air = air - .01f; //adjust as desired
            Io_dict["Air"] = air.ToString("0.0");
        }
        void setClock()
        {
            DateTime time = DateTime.Now;
            string hour = LeadingZero(time.Hour);
            string minute = LeadingZero(time.Minute);
            string second = LeadingZero(time.Second); //used for logs if not all the time
            string am_pm = DateTime.Now.ToString("tt");
            string timetext = hour + ":" + minute + ":" + second + " " + am_pm;
            Io_dict["Clock"] = timetext;
        }
        string LeadingZero(int n)
        {
            return n.ToString().PadLeft(2, '0');
        }

        void setTimer()
    {
        Io_dict["Timer"] = "10000"; //user input during dive plan can go here later
    }

        void Update()
        { 
            setAir();
            setBattery();
            setClock();
            setTimer();
            setX();
            setY();
            setDepth();
        


        }
    
}
}




