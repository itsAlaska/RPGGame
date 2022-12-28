using System;
using System.Collections;
using System.Collections.Generic;
using RPG.Attributes;
using UnityEngine;

public class PumpkinHeal : MonoBehaviour
{
    [SerializeField] GameObject healFXPrefab = null;

    private void OnTriggerEnter(Collider other)
    {
        var player = GameObject.FindGameObjectWithTag("Player");

        if (other.gameObject == player)
        {
            player.GetComponent<Health>().Heal(20);
            Instantiate(healFXPrefab);
            Destroy(gameObject);
        }
    }
}