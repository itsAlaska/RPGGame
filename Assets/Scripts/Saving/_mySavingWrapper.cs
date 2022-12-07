using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Saving
{
    public class _mySavingWrapper : MonoBehaviour
    {
        const string defaultSaveFile = "_mySave";

        void Start()
        {
            Load();
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.S))
            {
                Save();
            }
            if (Input.GetKeyDown(KeyCode.L))
            {
                Load();
            }
        }

        public void Load()
        {
            GetComponent<_mySavingSystem>().Load(defaultSaveFile);
        }

        public void Save()
        {
            GetComponent<_mySavingSystem>().Save(defaultSaveFile);
        }
    }
}
