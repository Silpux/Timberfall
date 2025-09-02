using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ExchangeSlot : MonoBehaviour{
    [SerializeField] private Image itemImage;
    [SerializeField] private TextMeshProUGUI amountText;

    public void SetExchangeData(CraftingRecipeSO.Ingredient recipe){
        itemImage.sprite = recipe.Item.icon;
        amountText.text = $"x{recipe.Amount}";
    }

}
