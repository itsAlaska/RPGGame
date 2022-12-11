using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using RPG.Saving;

namespace RPG.Cinematics
{
    public class CinematicTrigger : MonoBehaviour, ISaveable
    {
        bool alreadyTriggered;

        void Start() {
            if (alreadyTriggered)
            {
                return;
            }    
        }

        void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Player" && !alreadyTriggered)
            {
                alreadyTriggered = true;
                GetComponent<PlayableDirector>().Play();
            }
        }

        public object CaptureState()
        {
            print($"alreadyTriggere = {alreadyTriggered}");
            return alreadyTriggered;
        }

        public void RestoreState(object state)
        {
            alreadyTriggered = (bool)state;
        }
    }
}
