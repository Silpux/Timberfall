using UnityEngine;

public class AddBuildingPanel : Panel{

    [SerializeField] private CameraBuilding cameraBuilding;

    [SerializeField] private BuildStrategySO lumbermillStrategy;
    [SerializeField] private BuildStrategySO marketStrategy;
    [SerializeField] private BuildStrategySO blacksmithStrategy;
    [SerializeField] private BuildStrategySO mineStrategy;

    [SerializeField] private BuildingButton lumbermillButton;
    [SerializeField] private BuildingButton marketButton;
    [SerializeField] private BuildingButton blacksmithButton;
    [SerializeField] private BuildingButton mineButton;

    [SerializeField] private Sprite selectedBuildingSprite;
    [SerializeField] private Sprite deselectedBuildingSprite;

    private void Start(){
        lumbermillButton.AddListener(() => {
            cameraBuilding.BuildingStrategy = lumbermillStrategy;
            DeselectBuildings();
            lumbermillButton.SetSprite(selectedBuildingSprite);
        });
        marketButton.AddListener(() => {
            cameraBuilding.BuildingStrategy = marketStrategy;
            DeselectBuildings();
            marketButton.SetSprite(selectedBuildingSprite);
        });
        blacksmithButton.AddListener(() => {
            cameraBuilding.BuildingStrategy = blacksmithStrategy;
            DeselectBuildings();
            blacksmithButton.SetSprite(selectedBuildingSprite);
        });
        mineButton.AddListener(() => {
            cameraBuilding.BuildingStrategy = mineStrategy;
            DeselectBuildings();
            mineButton.SetSprite(selectedBuildingSprite);
        });

        lumbermillButton.SetPrice(lumbermillStrategy.Price);
        marketButton.SetPrice(marketStrategy.Price);
        blacksmithButton.SetPrice(blacksmithStrategy.Price);
        mineButton.SetPrice(mineStrategy.Price);
    }   

    private void DeselectBuildings(){
        lumbermillButton.SetSprite(deselectedBuildingSprite);
        marketButton.SetSprite(deselectedBuildingSprite);
        blacksmithButton.SetSprite(deselectedBuildingSprite);
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
