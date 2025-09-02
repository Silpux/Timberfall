using UnityEngine;

public abstract class ExchangeBuildingPanel<B> : BuildingPanel<B> where B : ExchangeBuilding{

    [SerializeField] private ExchangeEntry exchangeEntryPrefab;
    [SerializeField] private Transform exchangeListParent;

    public override void SetBuilding(B building){
        ResetUI();
        foreach(CraftingRecipeSO recipe in building.Recipes){
            ExchangeEntry exchangeEntry = Instantiate(exchangeEntryPrefab, exchangeListParent);
            exchangeEntry.SetRecipeData(recipe);
        }
        this.building = building;
    }

    public override void RefreshUI(){
        SetBuilding(building);
    }

    public override void RemoveBuilding(){
        Close();
        building.Remove();
    }

    public override void ResetUI(){
        foreach(Transform child in exchangeListParent){
            Destroy(child.gameObject);
        }
    }

}
