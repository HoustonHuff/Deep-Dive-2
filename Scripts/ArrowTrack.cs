using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArrowTrack : MonoBehaviour
{
    Quaternion initialRotation;

    // Start is called before the first frame update
    void Start()
    {
        //initialRotation = Quaternion.Euler(new Vector3(this.transform.localRotation.eulerAngles.x, this.transform.localRotation.eulerAngles.y, this.transform.localRotation.eulerAngles.z);
        //initialRotation = Quaternion.Euler(new Vector3(this.transform.rotation.eulerAngles.x, this.transform.rotation.eulerAngles.y, this.transform.rotation.eulerAngles.z));
    }

    // Update is called once per frame
    void Update()
    {
        if (GameObject.FindGameObjectsWithTag("gate").Length > 0)
        {
            this.transform.LookAt(GameObject.Find("NavGate(Clone)").transform.position);
            this.transform.rotation = this.transform.rotation * Quaternion.Euler(0, 90, 90);
        }

        else
        {
            this.transform.localRotation = Quaternion.identity;
            this.transform.rotation = this.transform.rotation * Quaternion.Euler(0, 90, 90);
        }
    }
}
