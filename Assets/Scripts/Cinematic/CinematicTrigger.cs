using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using RPG.Saving;

namespace RPG.Cinematics
{
    public class CinematicTrigger : MonoBehaviour, ISaveable
    {
        [SerializeField]
        Collider trigger;
        bool alreadyTriggered = false;

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
                trigger.enabled = false;
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
