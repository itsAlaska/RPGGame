using RPG.Shops;
using TMPro;
using UnityEngine;

namespace RPG.UI.Shops
{
    public class ShopUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI title = null;
        [SerializeField] private Transform listRoot;
        [SerializeField] private RowUI rowPrefab;
        

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

                row.Setup(item);
            }
        }

        public void Close()
        {
            shopper.SetActiveShop(null);
        }
    }
}