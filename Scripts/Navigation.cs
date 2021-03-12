using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DateTime = System.DateTime;
using UnityEngine.UI;
using DeepDive;

namespace DeepDive
{
    public class Navigation : Hud
    {
        public struct State
        {
            public bool enabled;
            public string mode;

            public State(bool e, string m)
            {
                enabled = e;
                mode = m;
            }
        }

        public static int RETURN_LOG_SIZE = 10;
        public static float UPDATE_LOG_INTERVAL = 0.5f;
        public static float UPDATE_LOG_MINIMUM_DISTANCE = 20.0f;
        public static float RETURN_INTERPOLATE_DISTANCE = 20.0f;
        public static Subject DUMMY_SUBJECT = new Subject("N/A", "N/A", "N/A", "N/A");
        IOScript io;
        private List<Log> permLog;
        private Stack<Log> backLog;
        private State state;
        private float logTimer;
        private float clearTimer;
        public int gateCounter;
        public GameObject n;
        public GameObject prev;
        public bool enabled;
        private Button enableButton; 
        private Slider modeSlider;
        //private int returnEnabledMax;

        public List<Log> getPermLog() { return permLog; }
        public List<Log> getPermLog(int start, int end) { return permLog.GetRange(start, end); }
        public State getState() { return state; }
        public Log getStartLocation() { if (permLog.Count != 0) return permLog[0]; else return new Log(); }
        public Log getLastLocation() { if (permLog.Count != 0) return permLog[permLog.Count - 1]; else return new Log(); }

        // Start is called before the first frame update
        public override void Start()
        {
            io = GameObject.Find("IOController").GetComponent<IOScript>();
            permLog = new List<Log>();
            backLog = new Stack<Log>();
            state = new State(false, "backtrack");
            enabled = false;
            logTimer = 0.0f;
            gateCounter = 0;
            enableButton = GameObject.Find("First Person Player/Camera/Mask/HUDController/Navigation HUD/EnableNavButton").GetComponent<Button>();
            //enableButton = GameObject.Find("EnableNavButton").GetComponent<Button>(); 
            enableButton.onClick.AddListener(toggleEnabled);
            modeSlider = GameObject.Find("First Person Player/Camera/Mask/HUDController/Navigation HUD/ModeSlider").GetComponent<Slider>();
            //modeSlider = GameObject.Find("ModeSlider").GetComponent<Slider>();
            modeSlider.onValueChanged.AddListener(delegate { changeMode(); });
            //returnEnabledMax = RETURN_LOG_SIZE;
        }

        // Update is called once per frame
        public override void Update()
        {
            logTimer += Time.deltaTime;

            //Make log/navigation changes every specified interval.
            if (logTimer >= UPDATE_LOG_INTERVAL)
            {
                //Add a new Log to permLog if under the right conditions (i.e. minimum distance from the newest log location).
                //Adding to backLog also requires Navigation to not currently be running in "backtrack" mode, in order to preserve the path in the Stack.
                if (permLog.Count == 0 || Vector3.Distance(new Vector3(float.Parse(io.Io_dict["X"]), float.Parse(io.Io_dict["Depth"]), float.Parse(io.Io_dict["Y"])), permLog[permLog.Count - 1].location) > UPDATE_LOG_MINIMUM_DISTANCE)
                {
                    Log newLog = new Log(float.Parse(io.Io_dict["X"]), float.Parse(io.Io_dict["Depth"]), float.Parse(io.Io_dict["Y"]), DateTime.Now, DUMMY_SUBJECT);
                    permLog.Add(newLog);
                    if (state.enabled == false || state.mode != "backtrack")
                        backLog.Push(newLog);
                }

                logTimer = 0.0f;
            }

            if (GameObject.Find("Navigation HUD") != null && GameObject.Find("Navigation HUD").activeSelf)
            {
                if (Input.GetKeyDown(KeyCode.P))
                {
                    enableButton.onClick.Invoke();
                }

                if (Input.GetKeyDown(KeyCode.L))
                {
                    if (modeSlider.value == modeSlider.maxValue)
                    {
                        modeSlider.value = 0;
                        modeSlider.onValueChanged.Invoke(modeSlider.value);
                    }
                    else
                    {
                        modeSlider.value = modeSlider.value + 1;
                        modeSlider.onValueChanged.Invoke(modeSlider.value);
                    }
                }

                //If gates are missing, regenerate the return path.
                if (GameObject.FindGameObjectsWithTag("gate").Length < RETURN_LOG_SIZE)
                    generateReturn();
            }
        }

        //Sets a new State.
        //First make sure the returnLog is cleared and afterwards regenerate it. Reset variables used in generateReturn before calling it.
        public void setState(bool e, string m)
        {
            resetReturn();
            state = new State(e, m);
            if (state.enabled == true)
                enableButton.gameObject.GetComponentInChildren<Text>().text = "Disable";
            else
                enableButton.gameObject.GetComponentInChildren<Text>().text = "Enable";
            //returnEnabledMax = RETURN_LOG_SIZE;
            n = null;
            generateReturn();
        }

        //A wrapper for setting a new state, toggling whether the navigation is enabled or disabled
        public void toggleEnabled()
        {
            string mode = state.mode;
            if (state.enabled == true)
            {
                setState(false, mode);
            }
            else
            {
                setState(true, mode);
            }
        }

        public void changeMode()
        {
            bool e = state.enabled;
            switch((int)modeSlider.value)
            {
                case 0:
                    setState(e, "backtrack");
                    break;
                case 1:
                    setState(e, "return");
                    break;
            }
        }

        //Clears the returnLog. If in "backtrack" mode, it ensures its values are returned to backLog to preserve the overall backtrack path.
        public void resetReturn()
        {
            GameObject[] list = GameObject.FindGameObjectsWithTag("gate");
            for (int i = list.Length - 1; i > -1; i--)
            {
                Vector3 l = list[i].transform.position;
                if (state.mode == "backtrack")
                    backLog.Push(new Log(l.x, l.y, l.z, DateTime.Now, DUMMY_SUBJECT));
                Destroy(list[i]);
            }
        }
        
        //Refills the returnLog to its full size
        private void generateReturn()
        {
            if (state.enabled)
            {
                if (state.mode == "backtrack")
                {
                    while (GameObject.FindGameObjectsWithTag("gate").Length < RETURN_LOG_SIZE && backLog.Count > 0)
                    {
                        Vector3 curr;
                        if (GameObject.FindGameObjectsWithTag("gate").Length > 0)
                            curr = GameObject.FindGameObjectsWithTag("gate")[GameObject.FindGameObjectsWithTag("gate").Length - 1].transform.position;
                        else
                            curr = this.gameObject.transform.position;

                        n = (GameObject)Instantiate(Resources.Load("NavGate"), backLog.Peek().location, Quaternion.identity);
                        n.GetComponent<GateBehavior>().index = gateCounter;
                        gateCounter++;
                        n.transform.LookAt(curr);
                        backLog.Pop();
                    }
                }

                else
                {
                    while (GameObject.FindGameObjectsWithTag("gate").Length < RETURN_LOG_SIZE && permLog.Count > 0)
                    {
                        Vector3 newCoords = Vector3.MoveTowards(permLog[permLog.Count - 1].location, permLog[0].location, RETURN_INTERPOLATE_DISTANCE * (GameObject.FindGameObjectsWithTag("gate").Length + 1));
                        //If generateReturn would create a new gate farther from origin than the last, or a new gate risks overshooting the actual origin, or the diver's close to origin, stop generating.
                        if ((n != null && Vector3.Distance(permLog[0].location, newCoords) + (RETURN_INTERPOLATE_DISTANCE*0.5) > Vector3.Distance(permLog[0].location, n.transform.position)) 
                            || Vector3.Distance(permLog[0].location, newCoords) < RETURN_INTERPOLATE_DISTANCE
                            || Vector3.Distance(permLog[0].location, permLog[permLog.Count-1].location) < RETURN_INTERPOLATE_DISTANCE*4)
                            break;
                        n = (GameObject)Instantiate(Resources.Load("NavGate"), newCoords, Quaternion.identity);
                        n.GetComponent<GateBehavior>().index = gateCounter;
                        gateCounter++;
                        n.transform.LookAt(permLog[0].location);
                    }
                    //returnEnabledMax = GameObject.FindGameObjectsWithTag("gate").Length;
                }
            }
        }
    }
}