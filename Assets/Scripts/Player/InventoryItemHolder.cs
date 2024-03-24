using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItemHolder : MonoBehaviour
{
    [SerializeField] private Image itemIcon;
    private EquipableItemData equipableItem;
    private PlayerInventoryController playerInventory;

    public void InitItem(EquipableItemData data, PlayerInventoryController inventory)
    {
        equipableItem = data;
        itemIcon.sprite = ItemsData.Instance.AllItems[data.Type][data.Index].Icon;
        playerInventory = inventory;
    }

    public void EquipItemButton()
    {
        playerInventory.EquipItem(equipableItem);
    }
}
