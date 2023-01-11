using GameDevTV.Utils;
using RPG.SceneManagement;
using TMPro;
using UnityEngine;

namespace RPG.UI
{
    public class MainMenuUI : MonoBehaviour
    {
        private LazyValue<_mySavingWrapper> savingWrapper;

        [SerializeField] private TMP_InputField newGameNameField;

        private void Awake()
        {
            savingWrapper = new LazyValue<_mySavingWrapper>(GetSavingWrapper);
        }

        private _mySavingWrapper GetSavingWrapper()
        {
            return FindObjectOfType<_mySavingWrapper>();
        }

        public void ContinueGame()
        {
            savingWrapper.value.ContinueGame();
        }

        public void NewGame()
        {
            savingWrapper.value.NewGame(newGameNameField.text);
        }
    }
}