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
        [SerializeField] [Range(0, 100)] public float sellingPercentage = 80;

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
        private bool isBuyingMode = true;

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
                float adjustedPrice = GetPrice(config);
                transaction.TryGetValue(config.item, out var quantityInTransaction);
                var availability = GetAvailability(config.item);
                yield return new ShopItem(config.item, availability, adjustedPrice, quantityInTransaction);
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
            isBuyingMode = isBuying;
            if (onChange != null)
            {
                onChange();
            }
        }

        public bool IsBuyingMode()
        {
            return isBuyingMode;
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
            if (!IsBuyingMode()) return true;
            Purse shoppersPurse = currentShopper.GetComponent<Purse>();

            if (shoppersPurse == null) return false;

            return shoppersPurse.GetBalance() >= TransactionTotal();
        }

        public bool HasInventorySpace()
        {
            if (!IsBuyingMode()) return true;
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
                    if (IsBuyingMode())
                    {
                        BuyItem(shopperInventory, shopperPurse, item, price);
                    }
                    else
                    {
                        SellItem(shopperInventory, shopperPurse, item, price);
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

            var availability = GetAvailability(item);

            if (transaction[item] + quantity > availability)
            {
                transaction[item] = availability;
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

        private int GetAvailability(InventoryItem item)
        {
            if (IsBuyingMode())
            {
                return stock[item];
            }

            return CountItemsInInventory(item);
        }

        private int CountItemsInInventory(InventoryItem item)
        {
            var inventory = currentShopper.GetComponent<Inventory>();

            if (inventory == null) return 0;
            
            var total = 0;
            
            for (int i = 0; i < inventory.GetSize(); i++)
            {
                if (item == inventory.GetItemInSlot(i))
                {
                    total += inventory.GetNumberInSlot(i);
                }
            }

            return total;
        }

        private float GetPrice(StockItemConfig config)
        {
            if (IsBuyingMode())
            {
                return config.item.GetPrice() * (1 - config.buyingDiscountPercentage / 100);
            }

            return config.item.GetPrice() * (sellingPercentage / 100);
        }

        private void SellItem(Inventory shopperInventory, Purse shopperPurse, InventoryItem item, float price)
        {
            var slot = FindFirstItemSlot(shopperInventory, item);
            if (slot == -1) return;

            AddToTransaction(item, -1);
            shopperInventory.RemoveFromSlot(slot, 1);
            stock[item]++;
            shopperPurse.UpdateBalance(price);
        }


        private void BuyItem(Inventory shopperInventory, Purse shopperPurse, InventoryItem item, float price)
        {
            if (shopperPurse.GetBalance() < price) return;

            var success = shopperInventory.AddToFirstEmptySlot(item, 1);
            if (success)
            {
                AddToTransaction(item, -1);
                stock[item]--;
                shopperPurse.UpdateBalance(-price);
            }
        }

        private int FindFirstItemSlot(Inventory shopperInventory, InventoryItem item)
        {
            for (var i = 0; i < shopperInventory.GetSize(); i++)
            {
                if (shopperInventory.GetItemInSlot(i) == item)
                {
                    return i;
                }
            }

            return -1;
        }
    }
}