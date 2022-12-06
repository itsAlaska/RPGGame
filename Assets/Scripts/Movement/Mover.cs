using System.Collections;
using System.Collections.Generic;
using RPG.Core;
using UnityEngine;
using UnityEngine.AI;
using RPG.Saving;

namespace RPG.Movement
{
    public class Mover : MonoBehaviour, IAction, ISaveable
    {
        [SerializeField]
        float maxSpeed = 6;

        NavMeshAgent navMeshAgent;
        Health health;

        bool isAttacking;

        void Start()
        {
            navMeshAgent = GetComponent<NavMeshAgent>();
            health = GetComponent<Health>();
        }

        void Update()
        {
            navMeshAgent.enabled = !health.IsDead;

            UpdateAnimator();
        }

        public void StartMoveAction(Vector3 destination, float speedFraction)
        {
            GetComponent<ActionScheduler>().StartAction(this);
            MoveTo(destination, speedFraction);
        }

        public void MoveTo(Vector3 destination, float speedFraction)
        {
            navMeshAgent.destination = destination;
            navMeshAgent.speed = maxSpeed * Mathf.Clamp01(speedFraction);
            navMeshAgent.isStopped = false;
        }

        public void Cancel()
        {
            navMeshAgent.isStopped = true;
        }

        void UpdateAnimator()
        {
            Vector3 playerVelocity = navMeshAgent.velocity;
            Vector3 localVelocity = transform.InverseTransformDirection(playerVelocity);
            float speed = localVelocity.z;

            GetComponent<Animator>().SetFloat("forwardSpeed", speed);
        }

        [System.Serializable]
        struct MoverSaveData 
        {
            public SerializableVector3 position;
            public SerializableVector3 rotation;
        }

        public object CaptureState()
        {
            // --- Struct approach ---
            MoverSaveData data = new MoverSaveData();
            data.position = new SerializableVector3(transform.position);
            data.rotation = new SerializableVector3(transform.eulerAngles);
            return data;

            // --- Dictionary approach ---
            // Dictionary<string, object> data = new Dictionary<string, object>();
            // data["position"] = new SerializableVector3(transform.position);
            // data["rotation"] = new SerializableVector3(transform.eulerAngles);
            // return data;
        }

        public void RestoreState(object state)
        {
            // --- Struct approach ---
            MoverSaveData data = (MoverSaveData)state;
            GetComponent<NavMeshAgent>().enabled = false;
            transform.position = data.position.ToVector();
            transform.eulerAngles = data.rotation.ToVector();
            GetComponent<NavMeshAgent>().enabled = true;

            // --- Dictionary appraoch ---
            // Dictionary<string, object> data = (Dictionary<string, object>)state;
            // GetComponent<NavMeshAgent>().enabled = false;
            // transform.position = ((SerializableVector3)data["position"]).ToVector();
            // transform.eulerAngles = ((SerializableVector3)data["rotation"]).ToVector();
            // GetComponent<NavMeshAgent>().enabled = true;
        }
    }
}
