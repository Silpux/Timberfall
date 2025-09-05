using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExchangeEntry : MonoBehaviour{

    [SerializeField] private List<ItemSlot> inputSlots;
    [SerializeField] private ItemSlot outputSlot;

    [SerializeField] private Image backgroundImage;

    [SerializeField] private Sprite backgroundOkSprite;
    [SerializeField] private Sprite backgroundNoSprite;

    private CraftingRecipeSO currentRecipe;

    private void OnEnable(){
        Inventory.Instance.OnInventoryUpdate += UpdateBackground;
    }
    private void OnDisable(){
        Inventory.Instance.OnInventoryUpdate -= UpdateBackground;
    }

    private void UpdateBackground(){
        if(Inventory.Instance.CanExchange(currentRecipe)){
            backgroundImage.sprite = backgroundOkSprite;
        }
        else{
            backgroundImage.sprite = backgroundNoSprite;
        }
    }

    public void ResetData(){
        foreach(ItemSlot slot in inputSlots){
            slot.gameObject.SetActive(false);
        }
        outputSlot.gameObject.SetActive(false);
    }

    public void SetRecipeData(CraftingRecipeSO recipe){

        ResetData();

        int slotsToUpdate = Math.Min(inputSlots.Count, recipe.Inputs.Count);

        for(int i = 2, j = 0; j<slotsToUpdate && i >= 0; i--,j++){
            inputSlots[i].gameObject.SetActive(true);
            inputSlots[i].Slot = recipe.Inputs[j];
        }

        outputSlot.gameObject.SetActive(true);
        outputSlot.Slot = recipe.Output;

        currentRecipe = recipe;
        UpdateBackground();
    }

    public void Exchange(){
        if(Inventory.Instance.Exchange(currentRecipe)){
            ;
        }
    }

}
