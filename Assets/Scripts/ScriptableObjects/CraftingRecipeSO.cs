using UnityEngine;

[CreateAssetMenu(fileName = "Recipe", menuName = "Recipe")]
public class CraftingRecipeSO : ScriptableObject{

    [System.Serializable]
    public class Ingredient{
        [SerializeField] private ItemDataSO item;
        public ItemDataSO Item => item;
        [SerializeField] private int amount;
        public int Amount => amount;
    }

    [SerializeField] private Ingredient[] inputs;
    [SerializeField] private ItemDataSO outputItem;
    [SerializeField] private int outputAmount = 1;

    public bool CanCraft(Inventory inventory){
        foreach(var ingredient in inputs){
            int actualAmount = 0;
            foreach(var slot in inventory.GetInventorySlots()){
                if(slot.Item == ingredient.Item){
                    actualAmount = slot.Count;
                    break;
                }
            }

            if(actualAmount < ingredient.Amount){
                return false;
            }
        }
        return true;
    }

    public void Craft(Inventory inventory){
        if(!CanCraft(inventory)){
            return;
        }

        foreach(var ingredient in inputs){
            inventory.RemoveItem(ingredient.Item, ingredient.Amount);
        }

        inventory.AddItem(outputItem, outputAmount);
    }
}
