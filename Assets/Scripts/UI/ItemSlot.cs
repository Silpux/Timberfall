using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour{
    [SerializeField] private Image itemImage;
    [SerializeField] private TextMeshProUGUI amountText;

    private InventorySlot slot;
    public InventorySlot Slot{
        get => slot;
        set{
            SetData(value);
            slot = value;
        }
    }

    private void SetData(InventorySlot data){
        itemImage.sprite = data.Item.Icon;
        amountText.text = $"x{data.Amount.ToString("N0", CultureInfo.InvariantCulture)}";
    }

}
