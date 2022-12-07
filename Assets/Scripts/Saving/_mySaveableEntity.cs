using RPG.Core;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

namespace RPG.Saving
{
    [ExecuteAlways]
    public class _mySaveableEntity : MonoBehaviour
    {
        [SerializeField]
        string uniqueIdentifier = "";

        public string GetUniqueIdentifier()
        {
            return uniqueIdentifier;
        }

        public object CaptureState()
        {
            return new _mySerializableVector3(transform.position);
        }

        public void RestoreState(object state)
        {
            _mySerializableVector3 position = (_mySerializableVector3)state;
            GetComponent<NavMeshAgent>().enabled = false;
            transform.position = position.ToVector();
            GetComponent<NavMeshAgent>().enabled = true;
            GetComponent<ActionScheduler>().CancelCurrentAction();
        }

#if UNITY_EDITOR
        void Update()
        {
            if (Application.IsPlaying(gameObject))
                return;
            if (string.IsNullOrEmpty(gameObject.scene.path))
                return;

            SerializedObject serializedObject = new SerializedObject(this);
            SerializedProperty property = serializedObject.FindProperty("uniqueIdentifier");

            if (string.IsNullOrEmpty(property.stringValue))
            {
                property.stringValue = System.Guid.NewGuid().ToString();
                serializedObject.ApplyModifiedProperties();
            }
        }
#endif
    }
}
