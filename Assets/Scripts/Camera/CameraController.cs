using UnityEngine;

public class CameraController : MonoBehaviour{
    
    private CameraMovement cameraMovement;
    private CameraBuilding cameraBuilding;

    private void Awake(){
        cameraMovement = GetComponent<CameraMovement>();
        cameraBuilding = GetComponent<CameraBuilding>();

        EnableMovementMode();
    }


    public void EnableBuildingMode(){
        cameraMovement.enabled = false;
        cameraBuilding.enabled = true;
    }

    public void EnableMovementMode(){
        cameraBuilding.LowlightSelected();
        cameraBuilding.enabled = false;
        cameraMovement.enabled = true;
    }

    public void DisableAllModes(){
        cameraBuilding.LowlightSelected();
        cameraBuilding.enabled = false;
        cameraMovement.enabled = false;
    }

}
