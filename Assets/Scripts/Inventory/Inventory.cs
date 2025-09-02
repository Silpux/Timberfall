using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class Inventory : MonoBehaviour{

    public static Inventory Instance => instance;
    private static Inventory instance;

    private List<InventorySlot> slots = new List<InventorySlot>();

    public event Action OnInventoryUpdate;

    [SerializeField] private ItemDataSO testItem;
    [SerializeField] private int testItemAmount;

    private void Awake(){

        if(instance != null){
            Destroy(gameObject);
            return;
        }
        instance = this;
    }

    [ContextMenu("Add test item")]
    public void AddTestItem(){
        AddItem(testItem, testItemAmount);
    }
    [ContextMenu("Remove test item")]
    public void RemoveTestItem(){
        RemoveItem(testItem, testItemAmount);
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

    public int GetItemAmount(ItemType itemType){
        foreach(InventorySlot slot in slots){
            if(slot.Item.ItemType == itemType){
                return slot.Amount;
            }
        }
        return 0;
    }

    public bool CanBuyLumbermillWorker(WorkerGrade grade){
        return ((int)grade & 1) == 1;
    }
    public bool CanBuyMineWorker(WorkerGrade grade){
        return ((int)grade & 1) != 1;
    }
}
