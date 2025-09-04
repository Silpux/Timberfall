using System.Collections.Generic;
using UnityEngine;

public class TileManager : Singleton<TileManager>{

    private Tile[,] tilesArray;
    private List<TileGrass> grassTiles = new();
    [SerializeField] private float tileSize = 1f;

    private int width;
    private int height;
    private Vector3 minPos;

    protected override void Awake(){
        base.Awake();
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
            t.Position = new Vector2(xIndex, zIndex);

        }

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

        int tileX = (int)tileGrass.Position.x;
        int tileY = (int)tileGrass.Position.y;

        foreach(Tile tile in GetNeighborTiles(tileX, tileY)){
            if(tile is TileGrass grass && !grass.IsFree){
                return null;
            }
        }
        return tileGrass;
    }

    public TreeObj GetClosestFreeTree(Transform transform){
        TreeObj closest = null;
        float minDist = float.MaxValue;
        Vector2 pointPosXZ = new Vector2(transform.position.x, transform.position.z);
        foreach(TileGrass tileGrass in grassTiles){
            if(tileGrass.HasTree && !tileGrass.CurrentTree.IsTargeted){
                Vector2 treePosXZ = new Vector2(tileGrass.CurrentTree.transform.position.x, tileGrass.CurrentTree.transform.position.z);
                float dist = Vector2.Distance(treePosXZ, pointPosXZ);
                if(dist < minDist){
                    closest = tileGrass.CurrentTree;
                    minDist = dist;
                }
            }
        }
        return closest;
    }

    public B GetClosestBuilding<B>(Transform transform) where B : Building{
        B closest = null;
        float minDist = float.MaxValue;
        Vector2 pointPosXZ = new Vector2(transform.position.x, transform.position.z);
        for(int i=0;i<tilesArray.GetLength(0);i++){
            for(int j = 0;j<tilesArray.GetLength(1);j++){
                if(tilesArray[i,j] != null){
                    Building building = tilesArray[i,j].CurrentBuilding;
                    if(building is B tileBuilding){
                        Vector2 buildingPosXZ = new Vector2(building.transform.position.x, building.transform.position.z);
                        float dist = Vector2.Distance(buildingPosXZ, pointPosXZ);
                        if(dist < minDist){
                            closest = tileBuilding;
                            minDist = dist;
                        }
                    }
                }
            }
        }
        return closest;
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
