using System.Collections.Generic;
using UnityEngine;

public abstract class ExchangeBuilding : Building{

    [SerializeField] private List<CraftingRecipeSO> recipes;
    public IReadOnlyList<CraftingRecipeSO> Recipes => recipes;

}
