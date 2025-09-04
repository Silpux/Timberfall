using UnityEngine;

[CreateAssetMenu(fileName = "Lumbermill Resource", menuName = "Resources/Lumbermill Resource")]
public class LumbermillWorkerDataSO : WorkerDataSO{
    [SerializeField] private float damage;
    public float Damage => damage;
}
