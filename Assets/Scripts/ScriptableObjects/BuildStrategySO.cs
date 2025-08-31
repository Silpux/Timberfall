using UnityEngine;

public abstract class BuildStrategySO : ScriptableObject, IBuildingStrategy{
    [SerializeField] private GameObject buildingPrefab;
    public abstract bool CanPlace(Tile[] tiles);
    [SerializeField] private int price;
    public int Price => price;
    public GameObject BuildingPrefab => buildingPrefab;
}
