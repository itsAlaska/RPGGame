using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Inventories
{
    public class Purse : MonoBehaviour
    {
        [SerializeField] private float startingBalance = 400;

        private float balance = 0;

        private void Awake()
        {
            balance = startingBalance;
            Debug.Log($"Balance: {balance}");
        }

        public float GetBalance()
        {
            return balance;
        }

        public void UpdateBalance(float amount)
        {
            balance += amount;
            Debug.Log($"Balance: {balance}");
        }
    }
}

