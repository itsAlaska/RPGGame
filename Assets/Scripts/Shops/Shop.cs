using System;
using System.Collections.Generic;
using RPG.Control;
using RPG.Inventories;
using UnityEngine;

namespace RPG.Shops
{
    public class Shop : MonoBehaviour, IRaycastable
    {
        [SerializeField] private string shopName = null;
        [SerializeField] private StockItemConfig[] stockConfig;

        [Serializable]
        class StockItemConfig
        {
            public InventoryItem item;
            public int initialStock;
            [Range(0, 100)] public float buyingDiscountPercentage;
        }

        private Dictionary<InventoryItem, int> transaction = new Dictionary<InventoryItem, int>();

        public event Action onChange;

        public IEnumerable<ShopItem> GetFilteredItems()
        {
            foreach (StockItemConfig config in stockConfig)
            {
                float adjustedPrice = config.item.GetPrice() * (1 - config.buyingDiscountPercentage / 100);
                int quantityInTransaction = 0;
                transaction.TryGetValue(config.item, out quantityInTransaction);
                yield return new ShopItem(config.item, config.initialStock, adjustedPrice, quantityInTransaction);
            }
        }

        public void SelectFilter(ItemCategory category)
        {
        }

        public ItemCategory GetFilter()
        {
            return ItemCategory.None;
        }

        public void SelectMode(bool isBuying)
        {
        }

        public bool IsBuyingMode()
        {
            return true;
        }

        public bool CanTransact()
        {
            return true;
        }

        public void ConfirmTransaction()
        {
        }

        public float TransactionTotal()
        {
            return 0;
        }

        public void AddToTransaction(InventoryItem item, int quantity)
        {
            if (!transaction.ContainsKey(item))
            {
                transaction[item] = 0;
            }
            
            transaction[item] += quantity;

            if (transaction[item] <= 0)
            {
                transaction.Remove(item);
            }

            if (onChange != null)
            {
                onChange();
            }
        }

        public string GetShopName()
        {
            return shopName;
        }

        public CursorType GetCursorType()
        {
            return CursorType.Shop;
        }

        public bool HandleRaycast(PlayerController callingController)
        {
            if (Input.GetMouseButtonDown(0))
            {
                callingController.GetComponent<Shopper>().SetActiveShop(this);
            }

            return true;
        }
    }
}