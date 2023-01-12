using RPG.Core;
using UnityEngine;
using UnityEngine.AI;
using RPG.Saving;
using RPG.Attributes;

namespace RPG.Movement
{
    public class Mover : MonoBehaviour, IAction, ISaveable
    {
        [SerializeField]
        float maxSpeed = 6;

        [SerializeField]
        float maxNavPathLength = 40f;

        NavMeshAgent navMeshAgent;
        Health health;

        bool isAttacking;

        void Awake()
        {
            navMeshAgent = GetComponent<NavMeshAgent>();
            health = GetComponent<Health>();
        }

        void Start() { }

        void Update()
        {
            navMeshAgent.enabled = !health.IsDead();

            UpdateAnimator();
        }

        public void StartMoveAction(Vector3 destination, float speedFraction)
        {
            GetComponent<ActionScheduler>().StartAction(this);
            MoveTo(destination, speedFraction);
        }

        public bool CanMoveTo(Vector3 destination)
        {
            NavMeshPath path = new NavMeshPath();
            bool hasPath = NavMesh.CalculatePath(
                transform.position,
                destination,
                NavMesh.AllAreas,
                path
            );

            if (!hasPath)
                return false;

            if (path.status != NavMeshPathStatus.PathComplete)
                return false;
            if (GetPathLength(path) > maxNavPathLength)
                return false;

            return true;
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

        float GetPathLength(NavMeshPath path)
        {
            float total = 0f;

            if (path.corners.Length < 2)
                return total;
            for (int i = 0; i < path.corners.Length - 1; i++)
            {
                total += Vector3.Distance(path.corners[i], path.corners[i + 1]);
            }

            return total;
        }

        [System.Serializable]
        struct MoverSaveData
        {
            public _mySerializableVector3 position;
            public _mySerializableVector3 rotation;
        }

        public object CaptureState()
        {
            // --- Struct approach ---
            MoverSaveData data = new MoverSaveData();
            data.position = new _mySerializableVector3(transform.position);
            data.rotation = new _mySerializableVector3(transform.eulerAngles);
            return data;

            // --- Dictionary approach ---
            // Dictionary<string, object> data = new Dictionary<string, object>();
            // data["position"] = new _mySerializableVector3(transform.position);
            // data["rotation"] = new _mySerializableVector3(transform.eulerAngles);
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
            // transform.position = ((_mySerializableVector3)data["position"]).ToVector();
            // transform.eulerAngles = ((_mySerializableVector3)data["rotation"]).ToVector();
            // GetComponent<NavMeshAgent>().enabled = true;
        }
    }
}
