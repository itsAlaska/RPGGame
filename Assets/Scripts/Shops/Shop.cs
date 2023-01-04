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

        public event Action onChange;

        public IEnumerable<ShopItem> GetFilteredItems()
        {
            yield return new ShopItem(
                InventoryItem.GetFromID("47bda7a8-bec5-42d0-aff1-fddb57d4f272"),
                1,
                10,
                0
            );
            yield return new ShopItem(
                InventoryItem.GetFromID("a572bf4d-3fbe-4ff7-8ca0-30e86f41f3c5"),
                1,
                10,
                0
            );
            yield return new ShopItem(
                InventoryItem.GetFromID("28ce746e-bca3-48b1-b311-173abb744914"),
                1,
                10,
                0
            );
            yield return new ShopItem(
                InventoryItem.GetFromID("f4e1833d-f08f-4a23-82eb-cef5f5d44d10"),
                1,
                10,
                0
            );
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