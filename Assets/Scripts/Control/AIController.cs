using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Control
{
    public class AIController : MonoBehaviour
    {
        [SerializeField]
        float chaseDistance = 5f;

        void Update()
        {
            DistanceToPlayer();
            if (DistanceToPlayer() <= chaseDistance)
            {
                print($"{name} has aggro'd on to the player!");

            }
        }

        private float DistanceToPlayer()
        {
            GameObject player = GameObject.FindWithTag("Player");

            return Vector3.Distance(transform.position, player.transform.position);

            
        }
    }
}
