using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInventoryController : MonoBehaviour
{
    [SerializeField] PlayerData playerData;
    [SerializeField] EquipItemsController equipItemsController;

    [SerializeField] private GameObject inventoryScreen;
    [SerializeField] private ScrollRect inventoryScroll;
    [SerializeField] private InventoryItemHolder inventoryItemHolderPrefab;

    private Dictionary<EquipableItemType, Image> equipImages = new Dictionary<EquipableItemType, Image>();
    [SerializeField] private Image equipHeadImg, equipShirtImg, equipLegsImg, equipShoesImg;
    private void Start()
    {
        equipImages.Add(EquipableItemType.Head, equipHeadImg);
        equipImages.Add(EquipableItemType.Shirt, equipShirtImg);
        equipImages.Add(EquipableItemType.Legs, equipLegsImg);
        equipImages.Add(EquipableItemType.Shoes, equipShoesImg);
    }

    // Update is called once per frame
    void Update()
    {
        // Read inventory input
        if (ControllerHelper.Instance.Input.Gameplay.Inventory.triggered)
        {
            ToggleInventory();
        }
    }

    public void ToggleInventory()
    {
        if (inventoryScreen.activeSelf)
        {
            inventoryScreen.SetActive(false);
            CleanInventory();
        }
        else
        {
            inventoryScreen.SetActive(true);
            LoadInventory();
        }

    }

    private void CleanInventory()
    {
        foreach (Transform child in inventoryScroll.content)
        {
            Destroy(child.gameObject);
        }
    }

    public void LoadInventory()
    {
        CleanInventory();

        foreach (var item in playerData.EquipableItemsInventory)
        {
            InventoryItemHolder newItem = Instantiate(inventoryItemHolderPrefab, inventoryScroll.content);
            newItem.InitItem(item, this);
        }

        UpdateEquipIcons();
    }

    private void UpdateEquipIcons()
    {
        foreach (var item in equipImages)
        {
            item.Value.gameObject.SetActive(false);
        }

        foreach (var item in playerData.EquippedItems)
        {
            equipImages[item.Key].gameObject.SetActive(true);
            equipImages[item.Key].sprite = ItemsData.Instance.AllItems[item.Key][item.Value.Index].Icon;
        }
    }

    public void EquipItem(EquipableItemData inventoryItemData)
    {
        EquipableItemData prevItem = equipItemsController.EquipNewItem(inventoryItemData.Type, inventoryItemData.Index);
        playerData.EquipableItemsInventory.Remove(inventoryItemData);
        
        if (prevItem != null) // Check if has swap Item
        {
            playerData.EquipableItemsInventory.Add(prevItem);
        }

        playerData.SaveInventoryData();

        LoadInventory();
    }

    public void UnequipItem(int type)
    {
        EquipableItemData prevItem = equipItemsController.UnequipItem((EquipableItemType)type);
        if (prevItem != null)
        {
            playerData.EquipableItemsInventory.Add(prevItem);
            playerData.SaveInventoryData();

            LoadInventory();
        }
    }
}
