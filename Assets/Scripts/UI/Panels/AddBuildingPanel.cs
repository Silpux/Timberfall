using System;
using UnityEngine;

public class AddBuildingPanel : Panel{

    [SerializeField] private CameraBuilding cameraBuilding;

    [SerializeField] private BuildStrategySO lumbermillStrategy;
    [SerializeField] private BuildStrategySO marketStrategy;
    [SerializeField] private BuildStrategySO forgeStrategy;
    [SerializeField] private BuildStrategySO mineStrategy;

    [SerializeField] private BuildingButton lumbermillButton;
    [SerializeField] private BuildingButton marketButton;
    [SerializeField] private BuildingButton forgeButton;
    [SerializeField] private BuildingButton mineButton;

    [SerializeField] private Sprite selectedBuildingSprite;
    [SerializeField] private Sprite deselectedBuildingSprite;

    public event Action OnClick;

    private void Start(){
        lumbermillButton.AddListener(() => {
            OnClick?.Invoke();
            cameraBuilding.BuildingStrategy = lumbermillStrategy;
            DeselectBuildings();
            lumbermillButton.SetSprite(selectedBuildingSprite);
        });
        marketButton.AddListener(() => {
            OnClick?.Invoke();
            cameraBuilding.BuildingStrategy = marketStrategy;
            DeselectBuildings();
            marketButton.SetSprite(selectedBuildingSprite);
        });
        forgeButton.AddListener(() => {
            OnClick?.Invoke();
            cameraBuilding.BuildingStrategy = forgeStrategy;
            DeselectBuildings();
            forgeButton.SetSprite(selectedBuildingSprite);
        });
        mineButton.AddListener(() => {
            OnClick?.Invoke();
            cameraBuilding.BuildingStrategy = mineStrategy;
            DeselectBuildings();
            mineButton.SetSprite(selectedBuildingSprite);
        });

        lumbermillButton.SetPrice(lumbermillStrategy.Amount);
        marketButton.SetPrice(marketStrategy.Amount);
        forgeButton.SetPrice(forgeStrategy.Amount);
        mineButton.SetPrice(mineStrategy.Amount);
    }   

    private void DeselectBuildings(){
        lumbermillButton.SetSprite(deselectedBuildingSprite);
        marketButton.SetSprite(deselectedBuildingSprite);
        forgeButton.SetSprite(deselectedBuildingSprite);
        mineButton.SetSprite(deselectedBuildingSprite);
    }

    public override void RefreshUI(){

    }

    public override void ResetUI(){

    }

    public override void Close(){
        PanelManager.Instance.CloseAddBuildingPanel();
    }
}
