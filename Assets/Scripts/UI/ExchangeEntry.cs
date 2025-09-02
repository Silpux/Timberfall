using System;
using System.Collections.Generic;
using UnityEngine;

public class ExchangeEntry : MonoBehaviour{

    [SerializeField] private List<ExchangeSlot> inputSlots;
    [SerializeField] private ExchangeSlot outputSlot;

    private CraftingRecipeSO currentRecipe;

    public void ResetData(){
        foreach(ExchangeSlot slot in inputSlots){
            slot.gameObject.SetActive(false);
        }
        outputSlot.gameObject.SetActive(false);
    }

    public void SetRecipeData(CraftingRecipeSO recipe){

        ResetData();

        int slotsToUpdate = Math.Min(inputSlots.Count, recipe.Inputs.Count);

        for(int i = 2, j = 0; j<slotsToUpdate && i > 0; i--,j++){
            inputSlots[i].gameObject.SetActive(true);
            inputSlots[i].SetExchangeData(recipe.Inputs[j]);
        }

        outputSlot.gameObject.SetActive(true);
        outputSlot.SetExchangeData(recipe.Output);

        currentRecipe = recipe;
    }

    public void Exchange(){
        if(currentRecipe.Craft(Inventory.Instance)){
            ;
        }
    }

}
