using UnityEngine;

public interface IBuildingStrategy{
    bool CanPlace(Tile[] tiles);
    int Price{get;}
    GameObject BuildingPrefab{get;}
}
