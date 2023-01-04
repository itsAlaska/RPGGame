using RPG.Shops;
using TMPro;
using UnityEngine;

namespace RPG.UI.Shops
{
    public class ShopUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI title = null;

        private Shopper shopper = null;
        private Shop currentShop = null;

        void Start()
        {
            shopper = GameObject.FindGameObjectWithTag("Player").GetComponent<Shopper>();

            if (shopper == null) return;

            shopper.activeShopChange += ShopChanged;

            ShopChanged();
        }

        void ShopChanged()
        {
            currentShop = shopper.GetActiveShop();
            gameObject.SetActive(currentShop != null);
            
            if (currentShop == null) return;
            
            title.text = currentShop.GetShopName();
        }

        public void Close()
        {
            shopper.SetActiveShop(null);
        }
    }
}