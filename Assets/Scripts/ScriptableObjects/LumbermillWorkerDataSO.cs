using UnityEngine;

[CreateAssetMenu(fileName = "Lumbermill Resource", menuName = "Resources/Lumbermill Resource")]
public class LumbermillWorkerDataSO : WorkerDataSO{
    [SerializeField] private Axe axePrefab;
    public Axe AxePrefab => axePrefab;
    [SerializeField] private LumbermillResource resourcePrefab;
    public LumbermillResource ResourcePrefab => resourcePrefab;
}
