using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D.Aseprite;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class CameraBuilding : MonoBehaviour{

    private Plane yZeroPlane;
    [SerializeField] private TileManager tileManager;
    private List<Tile> selectedTiles = new();
    private IBuildingStrategy buildingStrategy;

    private Tile clickStartTile;

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
    }

    private void OnDisable(){
        GameInput.Instance.OnLeftButtonDown -= HandleDown;
        GameInput.Instance.OnLeftButtonUp -= HandleUp;
        GameInput.Instance.OnLookPerformed -= HandleLook;
    }

    private void HandleDown(){
        if(EventSystem.current != null && EventSystem.current.IsPointerOverGameObject()){
            LowlightSelected();
            return;
        }
        if(GetMouseGroundPoint(Mouse.current.position.ReadValue(), out var point)){
            clickStartTile = tileManager.GetTileAtPosition(point);
        }
    }

    private void HandleUp(){
        if(EventSystem.current != null && EventSystem.current.IsPointerOverGameObject()){
            LowlightSelected();
            return;
        }
        if(GetMouseGroundPoint(Mouse.current.position.ReadValue(), out var point)){
            Tile releaseButtonTile = tileManager.GetTileAtPosition(point);
            if(clickStartTile == releaseButtonTile){

                (int x, int y) = tileManager.GetCoordinatesOfTile(releaseButtonTile);
                Tile[] tiles = tileManager.GetNeighborTiles(x, y, includeSelf: true);

                bool canPlace = false;
                if(buildingStrategy != null){
                    canPlace = buildingStrategy.CanPlace(tiles);
                }
                
                if(canPlace){
                    PlaceBuilding(releaseButtonTile, tiles);
                }

            }
        }
    }

    private void PlaceBuilding(Tile center, Tile[] tilesToOccupy){
        Building building = Instantiate(buildingStrategy.BuildingPrefab, center.PlacementPoint);
        building.transform.localPosition = Vector3.zero;

        foreach(Tile t in tilesToOccupy){
            t.HasBuilding = true;
        }
    }

    public void SetBuildingStrategy(IBuildingStrategy strategy){
        buildingStrategy = strategy;
    }

    private void HandleLook(Vector2 delta){
        if(EventSystem.current != null && EventSystem.current.IsPointerOverGameObject()){
            LowlightSelected();
            return;
        }
        if(GetMouseGroundPoint(Mouse.current.position.ReadValue(), out var point)){
            Tile tile = tileManager.GetTileAtPosition(point);
            (int x, int y) = tileManager.GetCoordinatesOfTile(tile);
            Tile[] tiles = tileManager.GetNeighborTiles(x, y, includeSelf: true);

            LowlightSelected();

            bool canPlace = false;

            if(buildingStrategy != null){
                canPlace = buildingStrategy.CanPlace(tiles);
            }

            foreach(Tile t in tiles){
                if(t != null){
                    t.Highlight(canPlace);
                    selectedTiles.Add(t);
                }
            }
        }
    }

    public void LowlightSelected(){
        foreach(Tile t in selectedTiles){
            t.Lowlight();
        }
        selectedTiles.Clear();
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
