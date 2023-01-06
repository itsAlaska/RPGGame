using System;
using System.Collections.Generic;
using System.Linq;
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
        private Dictionary<InventoryItem, int> stock = new Dictionary<InventoryItem, int>();
        private Shopper currentShopper = null;

        public event Action onChange;

        private void Awake()
        {
            foreach (var config in stockConfig)
            {
                stock[config.item] = config.initialStock;
            }
        }

        public void SetShopper(Shopper shopper)
        {
            currentShopper = shopper;
        }

        public IEnumerable<ShopItem> GetFilteredItems()
        {
            return GetAllItems();
        }

        public IEnumerable<ShopItem> GetAllItems()
        {
            foreach (StockItemConfig config in stockConfig)
            {
                float adjustedPrice = config.item.GetPrice() * (1 - config.buyingDiscountPercentage / 100);
                transaction.TryGetValue(config.item, out var quantityInTransaction);
                int currentStock = stock[config.item];
                yield return new ShopItem(config.item, currentStock, adjustedPrice, quantityInTransaction);
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
            if (IsTransactionEmpty()) return false;
            if (!HasSufficientFunds()) return false;
            if (!HasInventorySpace()) return false;
            return true;
        }

        public bool HasSufficientFunds()
        {
            Purse shoppersPurse = currentShopper.GetComponent<Purse>();

            if (shoppersPurse == null) return false;

            return shoppersPurse.GetBalance() >= TransactionTotal();
        }

        public bool HasInventorySpace()
        {
            Inventory shopperInventory = currentShopper.GetComponent<Inventory>();
            if (shopperInventory == null) return false;
            
            List<InventoryItem> flatItems = new List<InventoryItem>();
            foreach (var shopItem in GetAllItems())
            {
                InventoryItem item = shopItem.GetInventoryItem();
                int quantity = shopItem.GetQuantityInTransaction();
                for (int i = 0; i < quantity; i++)
                {
                    flatItems.Add(item);
                }
            }

            return shopperInventory.HasSpaceFor(flatItems);
        }

        public bool IsTransactionEmpty()
        {
            return transaction.Count == 0;
        }

        public void ConfirmTransaction()
        {
            Inventory shopperInventory = currentShopper.GetComponent<Inventory>();
            Purse shopperPurse = currentShopper.GetComponent<Purse>();
            if (shopperInventory == null || shopperPurse == null) return;

            foreach (var shopItem in GetAllItems())
            {
                InventoryItem item = shopItem.GetInventoryItem();
                int quantity = shopItem.GetQuantityInTransaction();
                float price = shopItem.GetPrice();
                for (int i = 0; i < quantity; i++)
                {
                    if (shopperPurse.GetBalance() < price) break;

                    bool success = shopperInventory.AddToFirstEmptySlot(item, 1);
                    if (success)
                    {
                        AddToTransaction(item, -1);
                        stock[item]--;
                        shopperPurse.UpdateBalance(-price);
                    }
                }
            }

            if (onChange != null)
            {
                onChange();
            }
            
        }

        public float TransactionTotal()
        {
            float total = 0;
            foreach (var item in GetAllItems())
            {
                total += item.GetPrice() * item.GetQuantityInTransaction();
            }

            return total;
        }

        public void AddToTransaction(InventoryItem item, int quantity)
        {
            if (!transaction.ContainsKey(item))
            {
                transaction[item] = 0;
            }

            if (transaction[item] + quantity > stock[item])
            {
                transaction[item] = stock[item];
            }
            else
            {
                transaction[item] += quantity;
            }

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