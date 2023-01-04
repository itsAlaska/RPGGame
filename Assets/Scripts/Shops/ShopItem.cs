using System.Globalization;
using RPG.Inventories;
using UnityEngine;

namespace RPG.Shops
{
    public class ShopItem
    {
        private InventoryItem item;
        private int availability;
        private float price;
        private int quantityInTransaction;

        public ShopItem(InventoryItem item, int availability, float price, int quantityInTransaction)
        {
            this.item = item;
            this.availability = availability;
            this.price = price;
            this.quantityInTransaction = quantityInTransaction;
        }

        public string GetName()
        {
            return item.GetDisplayName();
        }

        public Sprite GetIcon()
        {
            return item.GetIcon();
        }

        public int GetAvailability()
        {
            return availability;
        }
        
        public float GetPrice()
        {
            return price;
        }
        
        public int GetQuantity()
        {
            return quantityInTransaction;
        }
    }
}

