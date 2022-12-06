using System.IO;
using UnityEngine;

namespace RPG.Saving
{
    public class _mySavingSystem : MonoBehaviour
    {
        public void Save(string saveFile)
        {
            print($"{GetPathFromSaveFile(saveFile)}");
        }

        public void Load(string saveFile)
        {
            print($"Loading from {saveFile}!");
        } 

        private string GetPathFromSaveFile(string saveFile)
        {
            return Path.Combine(Application.persistentDataPath, saveFile + ".sav");
        }
    }
}
