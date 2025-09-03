using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class Inventory : Singleton<Inventory>{

    private List<InventorySlot> slots = new List<InventorySlot>();

    public event Action OnInventoryUpdate;

    [SerializeField] private ItemDataSO testItem;
    [SerializeField] private int testItemAmount;

    [ContextMenu("Add test item")]
    public void AddTestItem(){
        AddItem(testItem, testItemAmount);
    }
    [ContextMenu("Remove test item")]
    public void RemoveTestItem(){
        RemoveItem(testItem, testItemAmount);
    }

    protected override void Awake(){
        base.Awake();
        AddItem(testItem, testItemAmount);
    }

    public void AddItem(ItemDataSO item, int amount){

        if(item == null || item.ItemType == ItemType.None){
            return;
        }

        foreach(var slot in slots){
            if(slot.Item == item){
                slot.Amount += amount;
                OnInventoryUpdate?.Invoke();
                return;
            }
        }

        slots.Add(new InventorySlot(item, amount));
        OnInventoryUpdate?.Invoke();

    }

    public void RemoveItem(ItemDataSO item, int amount){

        if(item == null || item.ItemType == ItemType.None){
            return;
        }

        for(int i = slots.Count-1;i>=0;i--){
            if(slots[i].Item.ItemType == item.ItemType){
                slots[i].Amount -= amount;
                if(slots[i].IsEmpty){
                    slots.Remove(slots[i]);
                }
                OnInventoryUpdate?.Invoke();
            }
        }
    }
    public void RemoveItem(ItemType type, int amount){

        if(type == ItemType.None){
            return;
        }

        for(int i = slots.Count-1;i>=0;i--){
            if(slots[i].Item.ItemType == type){
                slots[i].Amount -= amount;
                if(slots[i].IsEmpty){
                    slots.Remove(slots[i]);
                }
                OnInventoryUpdate?.Invoke();
            }
        }
    }

    public bool CanExchange(CraftingRecipeSO recipe){
        foreach(var ingredient in recipe.Inputs){
            foreach(var slot in slots){
                if(slot.Item == ingredient.Item){
                    if(slot.Amount < ingredient.Amount){
                        return false;
                    }
                    goto FoundItem;
                }
            }
            return false;
            FoundItem:;
        }
        return true;
    }

    public bool Exchange(CraftingRecipeSO recipe){
        if(!CanExchange(recipe)){
            return false;
        }

        foreach(var ingredient in recipe.Inputs){
            RemoveItem(ingredient.Item, ingredient.Amount);
        }

        AddItem(recipe.Output.Item, recipe.Output.Amount);

        return true;
    }

    public IEnumerable<InventorySlot> GetInventorySlots(){
        return slots.OrderBy(s => s.Item.ItemType);
    }

    public int GetItemAmount(ItemDataSO item){
        foreach(InventorySlot slot in slots){
            if(slot.Item == item){
                return slot.Amount;
            }
        }
        return 0;
    }
    public int GetItemAmount(ItemType type){
        foreach(InventorySlot slot in slots){
            if(slot.Item.ItemType == type){
                return slot.Amount;
            }
        }
        return 0;
    }
    private ItemType GetAxeType(WorkerGrade grade) => grade switch{
        WorkerGrade.Wood => ItemType.WoodenAxe,
        WorkerGrade.Stone => ItemType.StoneAxe,
        WorkerGrade.Iron => ItemType.IronAxe,
        WorkerGrade.Diamond => ItemType.DiamondAxe,
        _ => ItemType.None
    };
    private ItemType GetPickaxeType(WorkerGrade grade) => grade switch{
        WorkerGrade.Wood => ItemType.WoodenPickaxe,
        WorkerGrade.Stone => ItemType.StonePickaxe,
        WorkerGrade.Iron => ItemType.IronPickaxe,
        WorkerGrade.Diamond => ItemType.DiamondPickaxe,
        _ => ItemType.None
    };
    public bool CanBuyLumbermillWorker(WorkerGrade grade, int coinsAmount){
        return GetItemAmount(GetAxeType(grade)) >= 1 && GetItemAmount(ItemType.Coins) >= coinsAmount;
    }
    public bool CanBuyMineWorker(WorkerGrade grade, int coinsAmount){
        return GetItemAmount(GetPickaxeType(grade)) >= 1 && GetItemAmount(ItemType.Coins) >= coinsAmount;
    }

    public bool ConfirmByuingLumbermillWorker(WorkerGrade grade, int coinsAmount){
        if(!CanBuyLumbermillWorker(grade, coinsAmount)){
            return false;
        }
        RemoveItem(GetAxeType(grade), 1);
        RemoveItem(ItemType.Coins, coinsAmount);
        return true;
    }
    public bool ConfirmByuingMineWorker(WorkerGrade grade, int coinsAmount){
        if(!CanBuyMineWorker(grade, coinsAmount)){
            return false;
        }
        RemoveItem(GetPickaxeType(grade), 1);
        RemoveItem(ItemType.Coins, coinsAmount);
        return true;
    }
}
