using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractibleUIHighlight : MonoBehaviour
{
    //private Button b;
    //private Slider s;
    //private Color defaultColor;
    public float TIMER_INTERVAL = 0.5f;
    float timer;
    // Start is called before the first frame update
    void Start()
    {
        timer = 0.5f;
    }

    // Update is called once per frame
    void Update()
    {
        timer = timer - Time.deltaTime;
        if (timer < 0)
        {
            if (this.gameObject.GetComponent<Button>() != null)
            {
                var c = this.gameObject.GetComponent<Button>().colors;
                c.normalColor = Color.white;
                this.gameObject.GetComponent<Button>().colors = c;
            }

            else if (this.gameObject.GetComponent<Slider>() != null)
            {
                var c = this.gameObject.GetComponent<Slider>().colors;
                c.normalColor = Color.white;
                this.gameObject.GetComponent<Slider>().colors = c;
            }
            timer = TIMER_INTERVAL;
        }
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("fingertip"))
        {
            if (this.gameObject.GetComponent<Button>() != null)
            {
                var c = this.gameObject.GetComponent<Button>().colors;
                c.normalColor = Color.green;
                this.gameObject.GetComponent<Button>().colors = c;
            }

            else if (this.gameObject.GetComponent<Slider>() != null)
            {
                var c = this.gameObject.GetComponent<Slider>().colors;
                c.normalColor = Color.green;
                this.gameObject.GetComponent<Slider>().colors = c;
            }
        }
    }

    private void OnTriggerExit(Collider col)
    {
        if (col.CompareTag("fingertip"))
        {
            if (this.gameObject.GetComponent<Button>() != null)
            {
                var c = this.gameObject.GetComponent<Button>().colors;
                c.normalColor = Color.white;
                this.gameObject.GetComponent<Button>().colors = c;
            }

            else if (this.gameObject.GetComponent<Slider>() != null)
            {
                var c = this.gameObject.GetComponent<Slider>().colors;
                c.normalColor = Color.white;
                this.gameObject.GetComponent<Slider>().colors = c;
            }
        }
    }
}
