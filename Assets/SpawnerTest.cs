using System.Collections;
using System.Collections.Generic;
using RPG.Inventories;
using RPG.UI.Inventories;
using UnityEngine;

public class SpawnerTest : MonoBehaviour
{
    [SerializeField] ItemTooltip tooltipPrefab = null;

    void Start()
    {
        ItemTooltip tooltipInstance = Instantiate(tooltipPrefab, transform);
        tooltipInstance.Setup(InventoryItem.GetFromID("89efd503-6a37-48d7-8bac-8fa99bc19a4c"));
    }
}