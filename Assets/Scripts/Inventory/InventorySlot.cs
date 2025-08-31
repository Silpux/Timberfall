using System;
using UnityEngine;

[Serializable]
public class InventorySlot{
    public ItemDataSO Item{get; set;}
    public int Count{get; private set;}

    public bool IsEmpty => Item == null || Item.itemType == ItemType.None || Count <= 0;

    public void AddItem(ItemDataSO newItem, int amount){
        if(Item == null || Item.itemType == ItemType.None){
            Item = newItem;
            Count = amount;
        }
        else if(Item == newItem){
            Count += amount;
        }
    }

    public bool RemoveItem(int amount){
        if(Item == null || Count < amount){
            return false;
        }
        Count -= amount;
        return true;
    }
}
