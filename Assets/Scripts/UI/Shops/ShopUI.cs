using System.Collections;
using System.Collections.Generic;
using RPG.Shops;
using UnityEngine;

namespace RPG.UI.Shops
{
    public class ShopUI : MonoBehaviour
    {
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
        }
    }
}