using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DeepDive;

namespace DeepDive
{
    public struct Subject
    {
        public string order;
        public string family;
        public string genus;
        public string species;

        /*Subject()
        {
            order = "N/A";
            family = "N/A";
            genus = "N/A";
            species = "N/A";
        }*/

        public Subject(string o, string f, string g, string s)
        {
            order = o;
            family = f;
            genus = g;
            species = s;
        }
    }
}