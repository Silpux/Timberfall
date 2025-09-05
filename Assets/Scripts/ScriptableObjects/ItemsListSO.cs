using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "Items list", menuName = "Items list")]
public class ItemsListSO : ScriptableObject{
    
    public List<ItemDataSO> items;
    private Dictionary<ItemType, ItemDataSO> lookup;

    public void Init(){
        lookup = items.ToDictionary(i => i.ItemType, i => i);
    }

    public ItemDataSO GetItem(ItemType id){
        if (lookup == null) Init();
        return lookup.TryGetValue(id, out var item) ? item : null;
    }
}
