using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Footsteps : MonoBehaviour
{
    [SerializeField]
    AudioSource footstep;

    void PlayFootstep()
    {
        if (gameObject.tag == "Player")
            footstep.Play();
    }
}
