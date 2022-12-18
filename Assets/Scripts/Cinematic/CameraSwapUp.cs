using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Cinematics
{
    public class CameraSwapUp : MonoBehaviour
    {
        [SerializeField]
        GameObject cameraToSwap;

        void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Player")
            {
                cameraToSwap.SetActive(true);
            }
        }
    }
}
