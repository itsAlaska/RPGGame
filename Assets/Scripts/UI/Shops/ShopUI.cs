using RPG.Inventories;
using RPG.Shops;
using TMPro;
using UnityEngine;
using UnityEngine.SubsystemsImplementation;
using UnityEngine.UI;

namespace RPG.UI.Shops
{
    public class ShopUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI title = null;
        [SerializeField] private Transform listRoot;
        [SerializeField] private RowUI rowPrefab;
        [SerializeField] private TextMeshProUGUI totalField;
        [SerializeField] private Button confirmButton;
        [SerializeField] private Button switchButton;

        private Shopper shopper = null;
        private Shop currentShop = null;

        private Color originalTotalTextColor;

        void Start()
        {
            originalTotalTextColor = totalField.color;
            shopper = GameObject.FindGameObjectWithTag("Player").GetComponent<Shopper>();

            if (shopper == null) return;

            shopper.activeShopChange += ShopChanged;
            confirmButton.onClick.AddListener(ConfirmTransaction);
            switchButton.onClick.AddListener(SwitchMode);

            ShopChanged();
        }

        void ShopChanged()
        {
            if (currentShop != null)
            {
                currentShop.onChange -= RefreshUI;
            }

            currentShop = shopper.GetActiveShop();
            gameObject.SetActive(currentShop != null);

            foreach (var button in GetComponentsInChildren<FilterButtonUI>())
            {
                button.SetShop(currentShop);
            }

            if (currentShop == null) return;

            title.text = currentShop.GetShopName();
            currentShop.onChange += RefreshUI;

            RefreshUI();
        }

        private void RefreshUI()
        {
            foreach (Transform child in listRoot)
            {
                Destroy(child.gameObject);
            }

            foreach (ShopItem item in currentShop.GetFilteredItems())
            {
                var row = Instantiate(rowPrefab, listRoot);

                row.Setup(currentShop, item);
            }

            totalField.text = $"Total: ${currentShop.TransactionTotal():N2}";
            totalField.color = currentShop.HasSufficientFunds()
                ? totalField.color = originalTotalTextColor
                : totalField.color = Color.red;
            confirmButton.interactable = currentShop.CanTransact();
            var switchText = switchButton.GetComponentInChildren<TextMeshProUGUI>();
            var confirmText = confirmButton.GetComponentInChildren<TextMeshProUGUI>();
            if (currentShop.IsBuyingMode())
            {
                switchText.text = "Switch to Selling";
                confirmText.text = "Buy";
            }
            else
            {
                switchText.text = "Switch to Buying";
                confirmText.text = "Sell";
            }

            foreach (var button in GetComponentsInChildren<FilterButtonUI>())
            {
                button.RefreshUI();
            }
        }

        public void Close()
        {
            shopper.SetActiveShop(null);
        }

        public void ConfirmTransaction()
        {
            currentShop.ConfirmTransaction();
        }

        public void SwitchMode()
        {
            currentShop.SelectMode(!currentShop.IsBuyingMode());
        }
    }
}