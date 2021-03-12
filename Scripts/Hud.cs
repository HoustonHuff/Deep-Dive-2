using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DeepDive;

namespace DeepDive
{
    public abstract class Hud : MonoBehaviour
    {
        // Start is called before the first frame update
        public abstract void Start();

        // Update is called once per frame
        public abstract void Update();
    }
}