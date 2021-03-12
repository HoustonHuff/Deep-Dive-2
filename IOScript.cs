using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Globalization;
using System.Linq;

namespace DeepDive { 
public class IOScript : MonoBehaviour
{
        public Transform player;
        public  Dictionary<string, string> Io_dict = new Dictionary<string, string>() {
            {"Air", "7000"}, {"Depth", "80"}, {"Battery", "86"},{"Timer", "60"},{"Clock", "3:05:22 pm"}, {"X", "23"}, {"Y", "25"}, {"Alert", "Alert: "}
        };

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
            Io_dict["Y"] = player.position.y.ToString();
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
        void setAlert()
        {
            //2 levels of alerts
            string message = "Alert: ";
        //high
            //battery extremely low
            float battery = float.Parse(Io_dict["Battery"], CultureInfo.InvariantCulture.NumberFormat);
            if (battery <= 15)
            {
                message += "\n Battery Critical, Return to the surface";
            }
            else if(battery < 30) //battery low
            {
                message += "\n Battery Low, Please return to the surface";
            }


            //air extremely low
            float air = float.Parse(Io_dict["Air"], CultureInfo.InvariantCulture.NumberFormat);
            //air consumption estimate
            float depth = float.Parse(Io_dict["Depth"], CultureInfo.InvariantCulture.NumberFormat);
            //float pressure = depth / 34 + 1; //result is pressure at that depth in atm
            //float airinPSI = pressure * 14.69; //converts atm to psi
            //float airneeded = airinPSI / 75; //air divided by 75 psi per minute (assuming 75 psi is the amount taken in per minute, amount of time before they must surface
            if (air <= 300)
            {
                message += "\n Air Critical, Return to the surface";
            }
            else if(air < 700) //air low
            {
                message += "\n Air Low, Please return to the surface";
            }


            //depth extremely low
            if (depth >= 120)
            {

                message += "\n Depth Critical, Return to the surface";
            } 
            else if (depth > 80) //depth low
            {
                
                message += "\n Depth Low, Please rise";
            }
            Io_dict["Alert"] = message;
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
            setAlert();
        
        }
    
}
}




