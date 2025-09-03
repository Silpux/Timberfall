using UnityEngine;

public interface IBuildingStrategy{
    bool CanPlace(Tile[] tiles);
    ItemDataSO Resource{get;}
    int Amount{get;}
    Building BuildingPrefab{get;}
}
