using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Serialization;

namespace RPG.Dialogue
{
    [System.Serializable]
    public class DialogueNode
    {
        public string uniqueID;
        public string text;
        public string[] children;
        public Rect rect = new Rect(0, 0, 200, 100);
    }
}

