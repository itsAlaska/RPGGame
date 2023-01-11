using GameDevTV.Utils;
using RPG.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

namespace RPG.UI
{
    public class MainMenuUI : MonoBehaviour
    {
        [SerializeField] private Button continueGameButton;
        
        private LazyValue<_mySavingWrapper> savingWrapper;

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
    }
}