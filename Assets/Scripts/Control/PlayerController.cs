using UnityEngine;
using RPG.Movement;
using RPG.Combat;
using RPG.Attributes;
using System;
using RPG.Inventories;
using UnityEngine.EventSystems;
using UnityEngine.AI;
using UnityEngine.Assertions.Must;

namespace RPG.Control
{
    public class PlayerController : MonoBehaviour
    {
        Health health;
        private ActionStore actionStore;

        [System.Serializable]
        struct CursorMapping
        {
            public CursorType type;
            public Texture2D texture;
            public Vector2 hotspot;
        }

        [SerializeField] CursorMapping[] cursorMappings = null;
        [SerializeField] float maxNavMeshProjectionDistance = 1f;
        [SerializeField] float rayCastRadius = 1f;
        [SerializeField] private int numberOfAbilities = 6;

        private bool isDraggingUI = false;

        void Awake()
        {
            health = GetComponent<Health>();
            actionStore = GetComponent<ActionStore>();
        }

        void Update()
        {
            if (InteractWithUI())
                return;
            if (health.IsDead())
            {
                SetCursor(CursorType.None);
                return;
            }

            UseAbilities();

            if (InteractWithComponent())
                return;
            if (InteractWithMovement())
                return;

            SetCursor(CursorType.None);
        }

        

        bool InteractWithComponent()
        {
            RaycastHit[] hits = RaycastAllSorted();
            foreach (RaycastHit hit in hits)
            {
                IRaycastable[] raycastables = hit.transform.GetComponents<IRaycastable>();
                foreach (IRaycastable raycastable in raycastables)
                {
                    if (raycastable.HandleRaycast(this))
                    {
                        SetCursor(raycastable.GetCursorType());
                        return true;
                    }
                }
            }

            return false;
        }

        RaycastHit[] RaycastAllSorted()
        {
            RaycastHit[] hits = Physics.SphereCastAll(GetMouseRay(), rayCastRadius);
            float[] distances = new float[hits.Length];

            for (int i = 0; i < hits.Length; i++)
            {
                distances[i] = hits[i].distance;
            }

            Array.Sort(distances, hits);

            return hits;
        }

        bool InteractWithUI()
        {
            if (Input.GetMouseButtonUp(0))
            {
                isDraggingUI = false;
            }

            if (EventSystem.current.IsPointerOverGameObject())
            {
                if (Input.GetMouseButtonDown(0))
                {
                    isDraggingUI = true;
                }

                SetCursor(CursorType.UI);
                return true;
            }

            if (isDraggingUI)
            {
                return true;
            }

            return false;
        }
        
        bool InteractWithMovement()
        {
            Vector3 target;
            bool hasHit = RaycastNavMesh(out target);

            if (hasHit)
            {
                if (!GetComponent<Mover>().CanMoveTo(target)) return false;

                if (Input.GetMouseButton(0))
                {
                    GetComponent<Mover>().StartMoveAction(target, 1f);
                }

                SetCursor(CursorType.Movement);
                return true;
            }

            return false;
        }

        private void UseAbilities()
        {
            for (int i = 0; i < numberOfAbilities; i++)
            {
                if (Input.GetKeyDown(KeyCode.Alpha1 + i))
                {
                    actionStore.Use(i, gameObject);
                }
            }
           
        }
        
        bool RaycastNavMesh(out Vector3 target)
        {
            target = new Vector3();

            RaycastHit hit;
            bool hasHit = Physics.Raycast(GetMouseRay(), out hit);

            if (!hasHit)
                return false;

            // Find nearest navmesh point
            NavMeshHit navMeshHit;
            bool hasCastToNavMesh = NavMesh.SamplePosition(
                hit.point,
                out navMeshHit,
                maxNavMeshProjectionDistance,
                NavMesh.AllAreas
            );

            if (!hasCastToNavMesh)
                return false;

            target = navMeshHit.position;

            return true;
        }

        void SetCursor(CursorType type)
        {
            CursorMapping mapping = GetCursorMapping(type);
            Cursor.SetCursor(mapping.texture, mapping.hotspot, CursorMode.Auto);
        }

        CursorMapping GetCursorMapping(CursorType type)
        {
            foreach (CursorMapping mapping in cursorMappings)
            {
                if (mapping.type == type)
                {
                    return mapping;
                }
            }

            return cursorMappings[0];
        }

        public static Ray GetMouseRay()
        {
            return Camera.main.ScreenPointToRay(Input.mousePosition);
        }
    }
}