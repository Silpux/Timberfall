using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuildingButton : MonoBehaviour{
    
    [SerializeField] private Button buildButton;
    [SerializeField] private TextMeshProUGUI text;


    private Image buttonImage;

    private void Awake(){
        buttonImage = buildButton.GetComponent<Image>();
    }

    public void SetSprite(Sprite sprite){
        buttonImage.sprite = sprite;
    }

    public void AddListener(UnityEngine.Events.UnityAction a){
        buildButton.onClick.AddListener(a);
    }

    public void SetPrice(int price){
        text.text = price.ToString();
    }

}
