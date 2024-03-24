using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    public Dictionary<EquipableItemType, EquipableItemData> EquippedItems = new Dictionary<EquipableItemType, EquipableItemData>();
    public List<EquipableItemData> EquipableItemsInventory = new List<EquipableItemData>();
    public int Money = 50000;

    public static PlayerData Instance;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this.gameObject);

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
        EquippedItems = ES3.Load(DataKeys.PlayerEquippedItemsDic, new Dictionary<EquipableItemType, EquipableItemData>());
    }

    public void SaveInventoryData()
    {
        ES3.Save(DataKeys.PlayerInventoryList, EquipableItemsInventory);
    }

    public void LoadInventoryData()
    {
        EquipableItemsInventory = ES3.Load(DataKeys.PlayerInventoryList, new List<EquipableItemData>());
    }

    public void SaveMoneyData() 
    {
        ES3.Save(DataKeys.PlayerMoney, Money);
    }

    public void LoadMoneyData()
    {
        Money = ES3.Load(DataKeys.PlayerMoney, Money);
    }

    public void AddMoney(int value)
    {
        Money += value;
        if (Money < 0)
            Money = 0;

        SaveMoneyData();
    }
}
