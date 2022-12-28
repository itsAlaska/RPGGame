using System;
using System.Collections;
using System.Collections.Generic;
using RPG.Attributes;
using UnityEngine;

public class PumpkinHeal : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        var player = GameObject.FindGameObjectWithTag("Player");
        if (other.gameObject == player)
        {
            player.GetComponent<Health>().Heal(20);
            Destroy(gameObject);
        }
        
    }
}
