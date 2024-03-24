using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopKeeperController : MonoBehaviour
{
    [SerializeField] private GameObject shopScreen;
    [SerializeField] private TextMeshProUGUI moneyText, titleText;
    [SerializeField] private ScrollRect hatScroll, legScroll, shoesScroll, shirtScroll;
    [SerializeField] private ShopItemHolder shopItemHolderPrefab;

    public float sellRatio = 0.8f;

    private Dictionary<EquipableItemType, ScrollRect> itemScrolls = new Dictionary<EquipableItemType, ScrollRect>();

    private void Start()
    {
        itemScrolls.Add(EquipableItemType.Head, hatScroll);
        itemScrolls.Add(EquipableItemType.Legs, legScroll);
        itemScrolls.Add(EquipableItemType.Shoes, shoesScroll);
        itemScrolls.Add(EquipableItemType.Shirt, shirtScroll);
    }

    public void ToggleShop()
    {
        if (shopScreen.activeInHierarchy)
        {
            CloseShop();
        }
        else
        {
            shopScreen.SetActive(true);

            OpenBuyTab();
        }
    }

    public void CloseShop()
    {
        shopScreen.SetActive(false);

        foreach (var item in itemScrolls)
            CleanItemScroll(item.Key);
    }

    public void OpenBuyTab()
    {
        titleText.text = "Buy Clothes";

        foreach (var item in itemScrolls)
        {
            CleanItemScroll(item.Key);
            LoadBuyWindow(item.Key);
        }
        UpdateMoneyText();
    }

    public void OpenSellTab()
    {
        titleText.text = "Sell Clothes";

        foreach (var item in itemScrolls)
        {
            CleanItemScroll(item.Key);
            LoadSellWindow(item.Key);
        }
        UpdateMoneyText();
    }
    private void LoadBuyWindow(EquipableItemType type)
    {
        foreach (var item in ItemsData.Instance.AllItems[type])
        {
            var newItem = Instantiate(shopItemHolderPrefab, itemScrolls[type].content);
            EquipableItemData itemData = new EquipableItemData();
            itemData.Type = item.Type;
            itemData.Index = item.Index;

            newItem.InitItem(itemData, this, false);
        }
    }

    private void LoadSellWindow(EquipableItemType type)
    {
        foreach (var item in PlayerData.Instance.EquipableItemsInventory)
        {
            if (item.Type != type) continue;

            var newItem = Instantiate(shopItemHolderPrefab, itemScrolls[type].content);
            newItem.InitItem(item, this, true);
        }
    }

    private void CleanItemScroll(EquipableItemType type)
    {
        foreach (Transform child in itemScrolls[type].content)
        {
            Destroy(child.gameObject);
        }
    }

    public void SellItem(EquipableItemData item)
    {
        EquipableItem selectedItem = ItemsData.Instance.AllItems[item.Type][item.Index];

        if (PlayerData.Instance.EquipableItemsInventory.Remove(item)){
            PlayerData.Instance.AddMoney((int) (selectedItem.Price * sellRatio));

            PlayerData.Instance.SaveInventoryData();

            UpdateMoneyText();

            CleanItemScroll(item.Type);
            LoadSellWindow(item.Type);
        }
    }

    public void BuyItem(EquipableItemData item)
    {
        EquipableItem selectedItem = ItemsData.Instance.AllItems[item.Type][item.Index];

        if (PlayerData.Instance.Money >= selectedItem.Price)
        {
            PlayerData.Instance.AddMoney(-selectedItem.Price);

            PlayerData.Instance.EquipableItemsInventory.Add(item);
            PlayerData.Instance.SaveInventoryData();

            UpdateMoneyText();

            CleanItemScroll(item.Type);
            LoadBuyWindow(item.Type);
        }
        else
        {
            // Not enough money to buy
            Debug.Log("Not enough money to buy");
        }        
    }

    private void UpdateMoneyText()
    {
        moneyText.text = "Money:  " + PlayerData.Instance.Money;
    }

    private void Update()
    {
        if (ControllerHelper.Instance.Input.Gameplay.Cancel.triggered || ControllerHelper.Instance.Input.Gameplay.Inventory.triggered)
        {
            CloseShop();
        }
    }
}
