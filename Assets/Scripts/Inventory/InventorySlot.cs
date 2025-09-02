using System;
using UnityEngine;

[Serializable]
public class InventorySlot{

    [field: SerializeField] public ItemDataSO Item{get; private set;}
    [field: SerializeField] public int Amount{get; set;}

    public bool IsEmpty => Item == null || Item.ItemType == ItemType.None || Amount <= 0;

    public InventorySlot(ItemDataSO data, int amount){
        Item = data;
        Amount = amount;
    }
}
