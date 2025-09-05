using UnityEngine;

public class PanelManager : Singleton<PanelManager>{

    [SerializeField] private CameraController cameraController;

    [SerializeField] private AddBuildingPanel addBuildingPanel;
    [SerializeField] private LumbermillPanel lumbermillPanel;
    [SerializeField] private MinePanel minePanel;

    [SerializeField] private MarketPanel marketPanel;
    [SerializeField] private ForgePanel forgePanel;
    [SerializeField] private InventoryPanel inventoryPanel;

    [SerializeField] private PausePanel pausePanel;

    [SerializeField] private GameObject pauseButton;
    [SerializeField] private GameObject inventoryButton;
    [SerializeField] private GameObject addBuildingsButton;


    public void Pause(){
        Time.timeScale = 0f;
        pausePanel.gameObject.SetActive(true);
    }

    public void ClosePause(){
        Time.timeScale = 1f;
        pausePanel.gameObject.SetActive(false);
    }

    public void DisableButtons(){
        pauseButton.SetActive(false);
        inventoryButton.SetActive(false);
        addBuildingsButton.SetActive(false);
    }

    public void EnableButtons(){
        pauseButton.SetActive(true);
        inventoryButton.SetActive(true);
        addBuildingsButton.SetActive(true);
    }

    public void OpenAddBuildingPanel(){
        DisableButtons();
        addBuildingPanel.gameObject.SetActive(true);
        cameraController.EnableBuildingMode();
    }

    public void CloseAddBuildingPanel(){
        EnableButtons();
        addBuildingPanel.gameObject.SetActive(false);
        cameraController.EnableMovementMode();
    }

    public void OpenInventoryPanel(){
        DisableButtons();
        cameraController.DisableAllModes();
        inventoryPanel.gameObject.SetActive(true);
        inventoryPanel.RefreshUI();
    }

    public void CloseInventoryPanel(){
        EnableButtons();
        inventoryPanel.gameObject.SetActive(false);
        cameraController.EnableMovementMode();
    }

    public void OpenMarketPanel(MarketBuilding sender){
        DisableButtons();
        cameraController.DisableAllModes();
        marketPanel.gameObject.SetActive(true);
        marketPanel.SetBuilding(sender);
    }

    public void CloseMarketPanel(){
        EnableButtons();
        marketPanel.gameObject.SetActive(false);
        cameraController.EnableMovementMode();
    }

    public void OpenForgePanel(ForgeBuilding sender){
        DisableButtons();
        cameraController.DisableAllModes();
        forgePanel.gameObject.SetActive(true);
        forgePanel.SetBuilding(sender);
    }

    public void CloseForgePanel(){
        EnableButtons();
        forgePanel.gameObject.SetActive(false);
        cameraController.EnableMovementMode();
    }


    public void OpenLumbermillPanel(LumbermillBuilding sender){
        DisableButtons();
        cameraController.DisableAllModes();
        lumbermillPanel.gameObject.SetActive(true);
        lumbermillPanel.SetBuilding(sender);
    }

    public void CloseLumbermillPanel(){
        EnableButtons();
        lumbermillPanel.gameObject.SetActive(false);
        cameraController.EnableMovementMode();
    }

    public void OpenMinePanel(MineBuilding sender){
        DisableButtons();
        cameraController.DisableAllModes();
        minePanel.gameObject.SetActive(true);
        minePanel.SetBuilding(sender);
    }

    public void CloseMinePanel(){
        EnableButtons();
        minePanel.gameObject.SetActive(false);
        cameraController.EnableMovementMode();
    }

}
