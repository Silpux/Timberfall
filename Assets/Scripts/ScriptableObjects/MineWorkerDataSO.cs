using UnityEngine;

[CreateAssetMenu(fileName = "Mine Resource", menuName = "Resources/Mine Resource")]
public class MineWorkerDataSO : WorkerDataSO{
    [SerializeField] private GameObject prefab;
    public GameObject Prefab => prefab;
    [SerializeField] private int amount;
    public int Amount => amount;
    [SerializeField] private float workingTime;
    public float WorkingTime => workingTime;
}
