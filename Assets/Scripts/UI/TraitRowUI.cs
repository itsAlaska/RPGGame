using RPG.Stats;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace RPG.UI
{
    public class TraitRowUI : MonoBehaviour
    {
        [SerializeField] private Trait trait;
        [SerializeField] private TextMeshProUGUI valueText;
        [SerializeField] private Button minusButton;
        [SerializeField] private Button plusButton;

        private TraitStore playerTraitStore = null;

        private void Start()
        {
            playerTraitStore = GameObject.FindGameObjectWithTag("Player").GetComponent<TraitStore>();
            minusButton.onClick.AddListener(() => Allocate(-1));
            plusButton.onClick.AddListener(() => Allocate(+1));
        }

        private void Update()
        {
            minusButton.interactable = playerTraitStore.CanAssignPoints(trait, -1);
            plusButton.interactable = playerTraitStore.CanAssignPoints(trait, +1);

            valueText.text = playerTraitStore.GetPoints(trait).ToString();
        }

        public void Allocate(int points)
        {
            playerTraitStore.AssignPoints(trait, points);
        }
    }
}

