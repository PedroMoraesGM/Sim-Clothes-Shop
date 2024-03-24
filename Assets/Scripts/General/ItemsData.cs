using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemsData : MonoBehaviour
{
    public Dictionary<EquipableItemType, EquipableItem[]> AllItems = new Dictionary<EquipableItemType, EquipableItem[]>();

    [SerializeField] private EquipableItem[] legsItems;
    [SerializeField] private EquipableItem[] shoesItems;
    [SerializeField] private EquipableItem[] shirtItems;
    [SerializeField] private EquipableItem[] headItems;

    public static ItemsData Instance;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this.gameObject);

        DontDestroyOnLoad(this);

        FillItemsDictionary();
    }

    private void FillItemsDictionary()
    {
        AllItems.Add(EquipableItemType.Legs, legsItems);
        AllItems.Add(EquipableItemType.Shoes, shoesItems);
        AllItems.Add(EquipableItemType.Shirt, shirtItems);
        AllItems.Add(EquipableItemType.Head, headItems);
    }
}
