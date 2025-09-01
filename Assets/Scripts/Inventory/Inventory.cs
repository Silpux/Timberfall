using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Inventory : MonoBehaviour{

    public static Inventory Instance => instance;
    private static Inventory instance;

    private int slotCount = 20;
    private List<InventorySlot> slots = new List<InventorySlot>();

    public event Action OnInventoryUpdate;

    private void Awake(){

        if(instance != null){
            Destroy(gameObject);
            return;
        }
        instance = this;

        for(int i=0; i<slotCount; i++){
            slots.Add(new InventorySlot());
        }
    }

    public bool AddItem(ItemDataSO item, int amount){
        foreach(var slot in slots){
            if(slot.Item == item){
                slot.AddItem(item, amount);
                OnInventoryUpdate?.Invoke();
                return true;
            }
        }

        foreach(var slot in slots){
            if(slot.IsEmpty){
                slot.AddItem(item, amount);
                OnInventoryUpdate?.Invoke();
                return true;
            }
        }

        return false;
    }

    public bool RemoveItem(ItemDataSO item, int amount){
        foreach(var slot in slots){
            if(slot.Item == item && slot.Count >= amount){
                slot.RemoveItem(amount);
                OnInventoryUpdate?.Invoke();
                return true;
            }
        }
        return false;
    }

    public IEnumerable<InventorySlot> GetInventorySlots(){
        return slots.OrderBy(s => s.Item.itemType);
    }

    public bool ConfirmBuyingLumbermillWorker(WorkerGrade grade){
        return true;
    }
}
