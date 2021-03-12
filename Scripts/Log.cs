using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DateTime = System.DateTime;
using DeepDive;

namespace DeepDive
{
    public struct Log
    {
        public Vector3 location;
        public DateTime time;
        public Subject subject;

        /*Log()
        {
            location = new Vector3(0,0,0);
        }*/

        public Log(float x, float y, float z, DateTime t, Subject s)
        {
            location = new Vector3(x, y, z);
            time = t;
            subject = s;
        }
    }
}