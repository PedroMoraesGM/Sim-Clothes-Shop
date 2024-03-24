using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    public Dictionary<EquipableItemType, EquipableItemData> EquippedItems = new Dictionary<EquipableItemType, EquipableItemData>();
    public List<EquipableItemData> EquipableItemsInventory = new List<EquipableItemData>();
    public int Money = 50000;


    private void Awake()
    {
        LoadEquippedItemsData();
        LoadInventoryData();
        LoadMoneyData();
    }

    public void SaveEquippedItemsData()
    {
        ES3.Save(DataKeys.PlayerEquippedItemsDic, EquippedItems);
    }

    public void LoadEquippedItemsData()
    {
        EquippedItems = ES3.Load(DataKeys.PlayerEquippedItemsDic, EquippedItems);
    }

    public void SaveInventoryData()
    {
        ES3.Save(DataKeys.PlayerInventoryList, EquipableItemsInventory);
    }

    public void LoadInventoryData()
    {
        EquipableItemsInventory = ES3.Load(DataKeys.PlayerInventoryList, EquipableItemsInventory);
    }

    public void SaveMoneyData() 
    {
        ES3.Save(DataKeys.PlayerMoney, Money);
    }

    public void LoadMoneyData()
    {
        Money = ES3.Load(DataKeys.PlayerMoney, Money);
    }
}
