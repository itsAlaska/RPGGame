using RPG.Shops;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace RPG.UI.Shops
{
    public class RowUI : MonoBehaviour
    {
        [SerializeField] private Image iconField;
        [SerializeField] private TextMeshProUGUI nameField;
        [SerializeField] private TextMeshProUGUI availabilityField;
        [SerializeField] private TextMeshProUGUI priceField;
        [SerializeField] private TextMeshProUGUI quantityField;

        private Shop currentShop = null;
        private ShopItem item = null;
        
        public void Setup(Shop currentShop, ShopItem item)
        {
            this.currentShop = currentShop;
            this.item = item;
            iconField.sprite = item.GetIcon();
            nameField.text = item.GetName();
            availabilityField.text = $"{item.GetAvailability()}";
            priceField.text = $"${item.GetPrice():N2}";
            quantityField.text = $"{item.GetQuantity()}";
        }

        public void Add()
        {
            currentShop.AddToTransaction(item.GetInventoryItem(), 1);
        }

        public void Remove()
        {
            currentShop.AddToTransaction(item.GetInventoryItem(), -1);
        }
    }
}

