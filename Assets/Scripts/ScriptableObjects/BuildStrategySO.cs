using UnityEngine;

public abstract class BuildStrategySO : ScriptableObject, IBuildingStrategy{
    [SerializeField] private Building buildingPrefab;
    public abstract bool CanPlace(Tile[] tiles);
    [SerializeField] private int price;
    public int Price => price;
    public Building BuildingPrefab => buildingPrefab;
}
