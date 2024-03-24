using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipItemsController : MonoBehaviour
{
    [SerializeField] Animator legsAnimator, shoesAnimator, shirtAnimator, headAnimator;
    private Dictionary<EquipableItemType, Animator> equipAnimators = new Dictionary<EquipableItemType, Animator>();

    // Start is called before the first frame update
    void Start()
    {
        equipAnimators.Add(EquipableItemType.Legs, legsAnimator);
        equipAnimators.Add(EquipableItemType.Shoes, shoesAnimator);
        equipAnimators.Add(EquipableItemType.Shirt, shirtAnimator);
        equipAnimators.Add(EquipableItemType.Head, headAnimator);

        foreach (var item in equipAnimators)
        {
            UpdateItemAnimatorController(item.Key);
        }
    }

    private void UpdateItemAnimatorController(EquipableItemType itemType)
    {
        if (PlayerData.Instance.EquippedItems.ContainsKey(itemType))
        {
            equipAnimators[itemType].gameObject.SetActive(true);
            equipAnimators[itemType].runtimeAnimatorController = ItemsData.Instance.AllItems[itemType][PlayerData.Instance.EquippedItems[itemType].Index].ItemAnimator;
        }
        else
            equipAnimators[itemType].gameObject.SetActive(false);
    }

    public EquipableItemData EquipNewItem(EquipableItemType itemType, int index)
    {
        EquipableItemData previousEquipment = null;
        EquipableItemData itemData = new EquipableItemData();
        itemData.Index = index;
        itemData.Type = itemType;

        if (!PlayerData.Instance.EquippedItems.ContainsKey(itemType))
            PlayerData.Instance.EquippedItems.Add(itemType, itemData);
        else
        {
            previousEquipment = PlayerData.Instance.EquippedItems[itemType];

            PlayerData.Instance.EquippedItems[itemType] = itemData;
        }

        PlayerData.Instance.SaveEquippedItemsData();

        UpdateItemAnimatorController(itemType);

        return previousEquipment;
    }

    public EquipableItemData UnequipItem(EquipableItemType itemType)
    {
        EquipableItemData previousEquipment = null;

        if (!PlayerData.Instance.EquippedItems.ContainsKey(itemType)) return previousEquipment;

        previousEquipment = PlayerData.Instance.EquippedItems[itemType];
        PlayerData.Instance.EquippedItems.Remove(itemType);
        PlayerData.Instance.SaveEquippedItemsData();

        UpdateItemAnimatorController(itemType);

        return previousEquipment;
    }
}
