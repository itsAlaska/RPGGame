using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Saving
{
    public class _mySavingWrapper : MonoBehaviour
    {
        const string defaultSaveFile = "_mySave";

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.S))
            {
                GetComponent<_mySavingSystem>().Save(defaultSaveFile);
            }
            if (Input.GetKeyDown(KeyCode.L))
            {
                GetComponent<_mySavingSystem>().Load(defaultSaveFile);
            }
        }
    }
}
