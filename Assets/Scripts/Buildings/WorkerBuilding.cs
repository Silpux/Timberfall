using UnityEngine;

public abstract class WorkerBuilding<W> : WorkerBuildingBase where W : Worker{

    [SerializeField] protected W workerPrefab;
    protected virtual W SpawnWorker(WorkerGrade grade){
        W newWorker = Instantiate(workerPrefab, workerSpawnPosition.position, Quaternion.identity);
        newWorker.Grade = grade;
        newWorker.Building = this;
        Workers.Add(newWorker);
        return newWorker;
    }
}
