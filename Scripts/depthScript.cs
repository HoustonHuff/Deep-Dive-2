using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;//ui stuffs
using System.Globalization; //this does something..somewhere 

namespace DeepDive
{
    public class depthScript : MonoBehaviour
    {
        public float depth;
        
        public Slider slider;
        public Gradient gradient;
        public Image fill;
        public Transform player;//to be determined later on i suppose
        public Text depthLabel;
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

        public void setMaxDepth(float max)
        {
            slider.maxValue = max;
            slider.value = 0;

            fill.color = gradient.Evaluate(1);
        }
        
        public void setDepth()
        {
            //if (Depth > 60)
            //{
            ////float depth = player.position.y;//get player depth based off y axis
            //Depth = Depth - .06f;
            //used for demo purposes
            //}
            //float depth = (player.position.y -120) *-1;
            //Io_dict["Depth"] = depth.ToString();
            depthval = scriptInstance.Io_dict["Depth"]; 
            depth = (125 - float.Parse(depthval, CultureInfo.InvariantCulture.NumberFormat)) * 1; //doing the same thing as before, string to float
            slider.value = depth;
            depthval = depth.ToString();
            depthLabel.text = depthval + (" ft"); //option to change measurement to be added later possibly
            fill.color = gradient.Evaluate(slider.normalizedValue); //filling the bar levels
        }
        void Update()
        {
            

            setDepth();
        }
    }
}
