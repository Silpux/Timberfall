using UnityEngine;

public abstract class WorkerBuilding<W> : WorkerBuildingBase where W : Worker{

    [SerializeField] protected W workerPrefab;

}
