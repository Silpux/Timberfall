using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class CameraMovement : MonoBehaviour{

    private bool isDragging;
    private Vector3 dragStartWorld;
    private Vector2 dragStartScreen;
    private Plane yZeroPlane;

    [SerializeField] private Vector2 cameraMinXZ;
    [SerializeField] private Vector2 cameraMaxXZ;
    [SerializeField] private float cameraMinY;
    [SerializeField] private float cameraMaxY;
    [SerializeField] private float maxOffsetToDetectClick = 5f;

    private void Awake(){
        yZeroPlane = new Plane(Vector3.up, Vector3.zero);
    }

    private void OnEnable(){
        StartCoroutine(ONEnable());
    }

    private IEnumerator ONEnable(){
        while(GameInput.Instance == null){
            yield return null;
        }
        GameInput.Instance.OnLeftButtonDown += HandleDown;
        GameInput.Instance.OnLeftButtonUp += HandleUp;
        GameInput.Instance.OnLookPerformed += HandleLook;
        GameInput.Instance.OnScrollPerformed += HandleZoom;
    }

    private void OnDisable(){
        GameInput.Instance.OnLeftButtonDown -= HandleDown;
        GameInput.Instance.OnLeftButtonUp -= HandleUp;
        GameInput.Instance.OnLookPerformed -= HandleLook;
        GameInput.Instance.OnScrollPerformed -= HandleZoom;
    }

    private void HandleZoom(int zoom){
        float newY = Mathf.Clamp(transform.position.y - zoom, cameraMinY, cameraMaxY);
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }

    private void HandleDown(){
        if(EventSystem.current != null && EventSystem.current.IsPointerOverGameObject()){
            return;
        }

        Vector2 mousePosition = Mouse.current.position.ReadValue();
        if(GetMouseGroundPoint(mousePosition, out var hit)){
            dragStartWorld = hit;
            dragStartScreen = mousePosition;
            isDragging = true;
        }
    }

    private void HandleUp(){
        isDragging = false;

        Vector2 mousePosition = Mouse.current.position.ReadValue();
        if(Mathf.Abs(Vector2.Distance(dragStartScreen, mousePosition)) < maxOffsetToDetectClick){
            Ray ray = Camera.main.ScreenPointToRay(mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit)){
                Building building = hit.collider.GetComponent<Building>();
                if(building != null){
                    building.OnClick();
                }
            }
        }
    }

    private void HandleLook(Vector2 delta){
        if(isDragging && GetMouseGroundPoint(Mouse.current.position.ReadValue(), out var point)){
            Vector3 offset = dragStartWorld - point;
            transform.position = new Vector3(
                x: Mathf.Clamp(transform.position.x + offset.x, cameraMinXZ.x, cameraMaxXZ.x),
                y: transform.position.y,
                z: Mathf.Clamp(transform.position.z + offset.z, cameraMinXZ.y, cameraMaxXZ.y)
            );
        }
    }

    private bool GetMouseGroundPoint(Vector2 mousePos, out Vector3 hitPoint){
        Ray ray = Camera.main.ScreenPointToRay(mousePos);
        if(yZeroPlane.Raycast(ray, out float enter)){
            hitPoint = ray.GetPoint(enter);
            return true;
        }
        hitPoint = Vector3.zero;
        return false;
    }
}
