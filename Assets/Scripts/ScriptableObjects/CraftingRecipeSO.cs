using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Recipe", menuName = "Recipe")]
public class CraftingRecipeSO : ScriptableObject{

    [SerializeField] private List<InventorySlot> inputs;
    public IReadOnlyList<InventorySlot> Inputs => inputs;
    [SerializeField] private InventorySlot output;
    public InventorySlot Output => output;

}
