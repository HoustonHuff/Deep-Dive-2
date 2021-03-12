using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentController : MonoBehaviour
{
    string enviroState;

    // Start is called before the first frame update
    void Start()
    {
        enviroState = "light";
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            if (enviroState == "light")
            {
                enviroState = "dark";
                this.gameObject.GetComponentInChildren<Skybox>().enabled = true;
                RenderSettings.fogDensity = 0.02f;
            }
            else
            {
                enviroState = "light";
                this.gameObject.GetComponentInChildren<Skybox>().enabled = false;
                RenderSettings.fogDensity = 0.002f;
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
}
