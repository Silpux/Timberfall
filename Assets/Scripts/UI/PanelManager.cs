using System.Collections.Generic;
using UnityEngine;

public class PanelManager : MonoBehaviour{

    private static PanelManager instance;
    public static PanelManager Instance => instance;

    [SerializeField] private CameraController cameraController;

    [SerializeField] private GameObject addBuildingPanel;
    [SerializeField] private LumbermillPanel lumbermillPanel;

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
        lumbermillPanel.SetLumbermillBuilding(sender);
    }

    public void CloseLumbermillPanel(){
        EnableButtons();
        lumbermillPanel.gameObject.SetActive(false);
        cameraController.EnableMovementMode();
    }

}
