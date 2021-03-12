using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DeepDive;

namespace DeepDive
{
    public class HUDScript : MonoBehaviour
    {
        private static int HUD_SCREEN_COUNT = 3;
        private static int STARTING_HUD_INDEX = 0;
        public int hudIndex;
        private bool displaying;
        private string curr;
        public GameObject universal;
        public GameObject general;
        public GameObject plan;
        public GameObject navigation;
        private GameObject[] hudList;


        // Start is called before the first frame update
        void Start()
        {
            displaying = true;
            curr = "general";
            

            universal = GameObject.Find("Universal HUD");
            general = GameObject.Find("General HUD");
            plan = GameObject.Find("Dive Plan HUD");
            navigation = GameObject.Find("Navigation HUD");
            hudList = new GameObject[] { general, plan, navigation };
            
            //navigation.SetActive(false);
            hudIndex = STARTING_HUD_INDEX;
            setActiveHud();
        }

        // Update is called once per frame
        void Update()
        { 
            if (Input.GetKeyDown(KeyCode.I))
            {
                if (displaying)
                {
                    displaying = false;
                    if (GameObject.Find("Universal HUD") != null)
                        universal.SetActive(false);
                    if (GameObject.Find("Navigation HUD") != null)
                        this.gameObject.GetComponent<Navigation>().setState(false, this.gameObject.GetComponent<Navigation>().getState().mode);
                    //navigation.SetActive(false);
                    hudList[hudIndex].SetActive(false);
                }
                else
                {
                    displaying = true;
                    universal.SetActive(true);
                    hudList[hudIndex].SetActive(true);
                }
            }

            if (Input.GetKeyDown(KeyCode.O) && displaying)
            {
                if (hudIndex == HUD_SCREEN_COUNT - 1)
                {
                    hudIndex = 0;
                }
                else
                {
                    hudIndex++;
                }

                setActiveHud();

                if (hudList[hudIndex] == navigation)
                {
                    this.gameObject.GetComponent<Navigation>().setState(false, this.gameObject.GetComponent<Navigation>().getState().mode);
                }
                this.GetComponent<Navigation>().resetReturn();

            }
        }

        private void setActiveHud()
        {
            for (int i = 0; i < HUD_SCREEN_COUNT; i++)
            {
                if (i != hudIndex)
                {
                    hudList[i].SetActive(false);
                }
            }
            hudList[hudIndex].SetActive(true);
        }
    }
}