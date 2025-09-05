using System.Collections;
using System.Globalization;
using TMPro;
using UnityEngine;

public class CoinsPanel : MonoBehaviour{
    
    [SerializeField] private TextMeshProUGUI coinsNumberText;
    [SerializeField] private ItemDataSO trackingItem;

    private void OnEnable(){
        StartCoroutine(ONEnable());
    }

    private IEnumerator ONEnable(){
        while(Inventory.Instance == null){
            yield return null;
        }
        Inventory.Instance.OnInventoryUpdate += UpdateUI;
        UpdateUI();
    }

    private void UpdateUI(){
        coinsNumberText.text = $"{Inventory.Instance.GetItemAmount(trackingItem).ToString("N0", CultureInfo.InvariantCulture)}";
    }

    private void OnDisable(){
        Inventory.Instance.OnInventoryUpdate -= UpdateUI;
    }

}
