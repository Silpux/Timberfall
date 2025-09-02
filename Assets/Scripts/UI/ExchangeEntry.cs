using System;
using System.Collections.Generic;
using UnityEngine;

public class ExchangeEntry : MonoBehaviour{

    [SerializeField] private List<ItemSlot> inputSlots;
    [SerializeField] private ItemSlot outputSlot;

    private CraftingRecipeSO currentRecipe;

    public void ResetData(){
        foreach(ItemSlot slot in inputSlots){
            slot.gameObject.SetActive(false);
        }
        outputSlot.gameObject.SetActive(false);
    }

    public void SetRecipeData(CraftingRecipeSO recipe){

        ResetData();

        int slotsToUpdate = Math.Min(inputSlots.Count, recipe.Inputs.Count);

        for(int i = 2, j = 0; j<slotsToUpdate && i > 0; i--,j++){
            inputSlots[i].gameObject.SetActive(true);
            inputSlots[i].Slot = recipe.Inputs[j];
        }

        outputSlot.gameObject.SetActive(true);
        outputSlot.Slot = recipe.Output;

        currentRecipe = recipe;
    }

    public void Exchange(){
        if(Inventory.Instance.Exchange(currentRecipe)){
            ;
        }
    }

}
