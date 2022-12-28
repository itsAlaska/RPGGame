using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleFollower : MonoBehaviour
{
    private Transform player = null;

    private void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
    }

    private void Update()
    {
        if (player == null) return;

        transform.position = player.position;
    }
}
