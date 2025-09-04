using UnityEngine;

[CreateAssetMenu(fileName = "Mine Resource", menuName = "Mine Resource")]
public class MineWorkerDataSO : ScriptableObject{
    [SerializeField] private GameObject prefab;
    public GameObject Prefab => prefab;
    [SerializeField] private ItemDataSO itemData;
    public ItemDataSO ItemData => itemData;
    [SerializeField] private int amount;
    public int Amount => amount;
    [SerializeField] private float workingTime;
    public float WorkingTime => workingTime;
}
