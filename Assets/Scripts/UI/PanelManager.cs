using UnityEngine;

public class PanelManager : MonoBehaviour{

    private static PanelManager instance;
    public static PanelManager Instance => instance;

    [SerializeField] private CameraController cameraController;

    [SerializeField] private GameObject addBuildingPanel;
    [SerializeField] private LumbermillPanel lumbermillPanel;
    [SerializeField] private MinePanel minePanel;

    [SerializeField] private GameObject pauseButton;
    [SerializeField] private GameObject inventoryButton;
    [SerializeField] private GameObject addBuildingsButton;

    private void Awake(){
 
        if(instance != null){
            Destroy(gameObject);
            return;
        }

        instance = this;
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
        addBuildingPanel.SetActive(true);
        cameraController.EnableBuildingMode();
    }

    public void CloseAddBuildingPanel(){
        EnableButtons();
        addBuildingPanel.SetActive(false);
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
