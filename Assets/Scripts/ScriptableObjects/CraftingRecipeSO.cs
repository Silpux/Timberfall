using System.Collections.Generic;
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

    [SerializeField] private List<Ingredient> inputs;
    public IReadOnlyList<Ingredient> Inputs => inputs;
    [SerializeField] private Ingredient output;
    public Ingredient Output => output;

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

    public bool Craft(Inventory inventory){
        if(!CanCraft(inventory)){
            return false;
        }

        foreach(var ingredient in inputs){
            inventory.RemoveItem(ingredient.Item, ingredient.Amount);
        }

        inventory.AddItem(output.Item, output.Amount);

        return true;
    }
}
