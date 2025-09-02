using System.Collections;
using System.Globalization;
using TMPro;
using UnityEngine;

public class CoinsPanel : MonoBehaviour{
    
    [SerializeField] private TextMeshProUGUI coinsNumberText;

    private void OnEnable(){
        StartCoroutine(ONEnable());
    }

    private IEnumerator ONEnable(){
        while(GameInput.Instance == null){
            yield return null;
        }
        Inventory.Instance.OnInventoryUpdate += UpdateUI;
        UpdateUI();
    }

    private void UpdateUI(){
        coinsNumberText.text = $"{Inventory.Instance.GetItemAmount(ItemType.Coins).ToString("N0", CultureInfo.InvariantCulture)}";
    }

    private void OnDisable(){
        Inventory.Instance.OnInventoryUpdate -= UpdateUI;
    }

}
