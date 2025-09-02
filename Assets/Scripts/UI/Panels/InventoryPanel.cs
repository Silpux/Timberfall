using System.Collections.Generic;
using UnityEngine;

public class InventoryPanel : Panel{

    [SerializeField] private ItemSlot itemSlotPrefab;
    [SerializeField] private Transform itemsListParent;

    private List<ItemSlot> itemSlots = new();

    private void Awake(){
        ResetUI();
    }
    private void OnEnable(){
        RefreshUI();
        Inventory.Instance.OnInventoryUpdate += RefreshUI;
    }

    private void OnDisable(){
        Inventory.Instance.OnInventoryUpdate -= RefreshUI;
    }

    public override void Close(){
        PanelManager.Instance.CloseInventoryPanel();
    }

    public override void RefreshUI(){
        foreach(var slot in Inventory.Instance.GetInventorySlots()){
            foreach(var s in itemSlots){
                if(slot.Item.ItemType == s.Slot.Item.ItemType){
                    s.Slot = slot;
                    goto FoundItem;
                }
            }
            ItemSlot newEntry = Instantiate(itemSlotPrefab, itemsListParent);
            newEntry.Slot = slot;
            itemSlots.Add(newEntry);
            FoundItem:;
        }

        for(int i = itemSlots.Count - 1;i>=0;i--){
            foreach(var slot in Inventory.Instance.GetInventorySlots()){
                if(slot.Item.ItemType == itemSlots[i].Slot.Item.ItemType){
                    goto FoundItem;
                }
            }
            Destroy(itemSlots[i].gameObject);
            itemSlots.RemoveAt(i);
            FoundItem:;
        }
    }

    public override void ResetUI(){
        foreach(Transform child in itemsListParent){
            Destroy(child.gameObject);
        }
    }
}
