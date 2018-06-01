using System;
using System.Collections.Generic;
using UnityEngine;

namespace Bartok
{
    public class SlotDef
    {
        public float x;
        public float y;
        public bool faceUp = false;
        public string layerName = "Default";
        public int layerID = 0;
        public int id;
        public List<int> hiddenBy = new List<int>();
        public string type = "slot";
        public Vector2 stagger;
    }
}

