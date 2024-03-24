using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class EquipableItem
{
    public string Name;
    public Sprite Icon;
    public int Price;
    public EquipableItemType Type;
    public AnimatorOverrideController ItemAnimator;
    public int Index;
}

public class EquipableItemData
{
    public EquipableItemType Type;
    public int Index;
}

public enum EquipableItemType
{
    None = 0, Legs = 1, Shoes = 2, Shirt = 3, Head = 4
}