using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class CameraBuilding : MonoBehaviour{

    private Plane yZeroPlane;
    [SerializeField] private TileManager tileManager;
    private List<Tile> selectedTiles = new();

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
        
    }

    private void HandleUp(){
        
    }

    private void HandleLook(Vector2 delta){
        if(EventSystem.current != null && EventSystem.current.IsPointerOverGameObject()){
            return;
        }
        if(GetMouseGroundPoint(Mouse.current.position.ReadValue(), out var point)){
            Tile tile = tileManager.GetTileAtPosition(point);
            (int x, int y) = tileManager.GetCoordinatesOfTile(tile);
            Tile[] tiles = tileManager.GetNeighborTiles(x, y, includeSelf: true);

            for(int j = selectedTiles.Count - 1;j >=0;j--){
                for(int i= 0;i<tiles.Length;i++){
                    if(tiles[i] == selectedTiles[j]){
                        goto TileFound;
                    }
                }
                selectedTiles[j].Lowlight();
                selectedTiles.RemoveAt(j);
                TileFound:;
            }
            foreach(Tile t in tiles){
                if(t != null && !selectedTiles.Contains(t)){
                    t.Highlight();
                    selectedTiles.Add(t);
                }
            }
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
