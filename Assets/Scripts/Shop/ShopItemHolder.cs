using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopItemHolder : MonoBehaviour
{
    [SerializeField] private Image itemIcon;
    [SerializeField] private TextMeshProUGUI priceText;
    private EquipableItemData equipableItem;
    private ShopKeeperController shopController;

    private bool isOwned;

    public void InitItem(EquipableItemData data, ShopKeeperController shop, bool own)
    {
        equipableItem = data;
        itemIcon.sprite = ItemsData.Instance.AllItems[data.Type][data.Index].Icon;
        shopController = shop;
        isOwned = own;

        if (!isOwned)
        {            
            priceText.text = "<color=#42B627>$" + ItemsData.Instance.AllItems[data.Type][data.Index].Price;
        }
        else
        {
            priceText.text = "<color=#B62744>$" + ItemsData.Instance.AllItems[data.Type][data.Index].Price * shop.sellRatio;
        }
            

    }

    public void ItemButton()
    {
        if (isOwned)
            shopController.SellItem(equipableItem);
        else
            shopController.BuyItem(equipableItem);
    }
}
