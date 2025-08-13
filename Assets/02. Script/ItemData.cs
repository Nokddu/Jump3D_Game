using System;
using UnityEngine;

public enum ItemType
{
    EquipMent,
    Consumable
}

public enum ConsumableType
{
    Stamina,
    Speed,
    Jump
}

[Serializable]
public class ItemDataConsumable
{
    public ConsumableType type;
    public float value;
}

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Objects/obj", order = 1)]
public class ItemData : ScriptableObject
{
    [Header("Info")]
    public string displayName;
    public string ItemDescription;
    public ItemType type;

    [Header("Infinity")]
    public bool infinity;

    [Header("IsInteractive")]
    public bool isInterableObject;

    [Header("Consumable")]
    public ItemDataConsumable[] consumables;
    public float duration;
}
