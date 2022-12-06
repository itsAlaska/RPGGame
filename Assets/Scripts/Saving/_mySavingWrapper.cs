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
            if (Input.GetKeyDown(KeyCode.R))
            {
                GetComponent<_mySavingSystem>().Save(defaultSaveFile);
            }
            if (Input.GetKeyDown(KeyCode.T))
            {
                GetComponent<_mySavingSystem>().Load(defaultSaveFile);
            }
        }
    }
}
