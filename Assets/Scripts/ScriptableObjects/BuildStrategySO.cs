using UnityEngine;

public abstract class BuildStrategySO : ScriptableObject, IBuildingStrategy{
    [SerializeField] private Building buildingPrefab;
    public Building BuildingPrefab => buildingPrefab;
    public abstract bool CanPlace(Tile[] tiles);
    [SerializeField] private ItemDataSO resource;
    public ItemDataSO Resource => resource;
    [SerializeField] private int amount;
    public int Amount => amount;
}
