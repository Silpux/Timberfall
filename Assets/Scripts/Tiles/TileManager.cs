using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using UnityEngine;

public class TileManager : MonoBehaviour{

    public static TileManager Instance => instance;

    private static TileManager instance;

    private Tile[,] tilesArray;
    private List<TileGrass> grassTiles = new();
    [SerializeField] private float tileSize = 1f;

    private int width;
    private int height;
    private Vector3 minPos;

    private void Awake(){

        if(instance != null){
            Destroy(gameObject);
            return;
        }

        instance = this;

        InitializeTiles();
    }

    private void InitializeTiles(){

        Tile[] childTiles = GetComponentsInChildren<Tile>();

        if(childTiles.Length == 0){
            Debug.LogError("No tiles!");
            return;
        }

        float minX = float.MaxValue;
        float minZ = float.MaxValue;

        float maxX = float.MinValue;
        float maxZ = float.MinValue;

        foreach(Tile t in childTiles){

            Vector3 pos = t.transform.position;

            if (pos.x < minX) minX = pos.x;
            if (pos.x > maxX) maxX = pos.x;
            if (pos.z < minZ) minZ = pos.z;
            if (pos.z > maxZ) maxZ = pos.z;
        }

        minPos = new Vector3(minX, 0, minZ);

        width = Mathf.RoundToInt((maxX - minX) / tileSize) + 1;
        height = Mathf.RoundToInt((maxZ - minZ) / tileSize) + 1;

        tilesArray = new Tile[width, height];

        foreach(Tile t in childTiles){

            int xIndex = Mathf.RoundToInt((t.transform.position.x - minX) / tileSize);
            int zIndex = Mathf.RoundToInt((t.transform.position.z - minZ) / tileSize);

            if(t is TileGrass grass){
                grassTiles.Add(grass);
            }

            tilesArray[xIndex, zIndex] = t;
        }

    }

    public (int x, int y) GetCoordinatesOfTile(Tile tile){
        for (int i = 0; i < tilesArray.GetLength(0); i++){
            for (int j = 0; j < tilesArray.GetLength(1); j++){
                if (tilesArray[i,j] == tile){
                    return (i, j);
                }
            }
        }
        return (-1, -1);
    }

    public Tile[] GetNeighborTiles(int x, int y, bool includeSelf = false){
        int width = tilesArray.GetLength(0);
        int height = tilesArray.GetLength(1);

        Tile[] neighbors = new Tile[includeSelf ? 9 : 8];

        int i = -1;
        for(int dx = -1;dx <= 1;dx++){
            for(int dy = -1;dy <=1;dy++){

                if(dx == 0 && dy == 0 && !includeSelf){
                    continue;
                }
                i++;
                int nx = x + dx;
                int nz = y + dy;

                if(nx >= 0 && nx < width && nz >= 0 && nz < height){
                    neighbors[i] = tilesArray[nx, nz];
                }
                else{
                    neighbors[i] = null;
                }
            }
        }

        return neighbors;
    }

    public TileGrass GetTileForTree(){
        if(grassTiles.Count == 0) return null;
        TileGrass tileGrass = grassTiles[UnityEngine.Random.Range(0, grassTiles.Count)];
        (int x, int y) = GetCoordinatesOfTile(tileGrass);

        foreach(Tile tile in GetNeighborTiles(x, y)){
            if(tile is TileGrass grass && !grass.IsFree){
                return null;
            }
        }
        return tileGrass;
    }

    public Tile GetTileAtPosition(Vector3 worldPos){
        int xIndex = Mathf.RoundToInt((worldPos.x - minPos.x) / tileSize);
        int zIndex = Mathf.RoundToInt((worldPos.z - minPos.z) / tileSize);

        if(xIndex >= 0 && xIndex < width && zIndex >= 0 && zIndex < height){
            return tilesArray[xIndex, zIndex];
        }
        return null;
    }

}
