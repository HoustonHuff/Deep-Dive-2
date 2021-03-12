using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateBehavior : MonoBehaviour
{
    [SerializeField]
    //public GameObject previous;
    public int index;

    public void recursiveDestroy()
    {
        //if (previous != null)
        //    previous.GetComponent<GateBehavior>().recursiveDestroy();
        GameObject[] list = GameObject.FindGameObjectsWithTag("gate");
        //list[list.Length - 1].GetComponent<GateBehavior>()
        for (int i = 0; list[i] != this.gameObject; i++)
        {
            //if (list[i].GetComponent<GateBehavior>().index < index)
                //Debug.Log("Gate " + index + ": Recursively destroy Gate " + list[i].GetComponent<GateBehavior>().index);
                //list[i].GetComponent<GateBehavior>().recursiveDestroy();
                //previous.GetComponent<GateBehavior>().recursiveDestroy();
                Destroy(list[i]);
            //break;
        }
        //Debug.Log("Gate " + index + ": Destroy itself");
        Destroy(this.gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        //previous = null;

        GameObject[] list = GameObject.FindGameObjectsWithTag("gate");
        GameObject prev = list[0];

        if (list.Length > 1)
        {
            //Debug.Log("Gate " + n.GetComponent<GateBehavior>().index + ": There's another gate. Attempting to set previous.");
            for (int i = 0; i < list.Length; i++)
            {
                //Debug.Log("Gate " + n.GetComponent<GateBehavior>().index + ": Iterating through list of gates.");
                if ((list[i].GetComponent<GateBehavior>().index > prev.GetComponent<GateBehavior>().index && list[i] != this.gameObject)
                || (prev == this.gameObject && list[i] != this.gameObject))
                //if (list[i].transform.position == curr)
                {
                    //Debug.Log("Gate " + n.GetComponent<GateBehavior>().index + ": Found most likely previous, assigning to prev.");
                    prev = list[i];
                }
            }
            //Debug.Log("Gate " + n.GetComponent<GateBehavior>().index + ": Assigning Previous to " + prev);
            //if (prev != this.gameObject)
                //previous = prev;
        }
    }

    private void OnTriggerEnter(Collider c)
    {
        if (c.CompareTag("Player"))
            recursiveDestroy();
    }
}
