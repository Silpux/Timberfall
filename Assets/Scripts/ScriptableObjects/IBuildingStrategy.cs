using UnityEngine;

public interface IBuildingStrategy{
    bool CanPlace(Tile[] tiles);
    int Price{get;}
    Building BuildingPrefab{get;}
}
