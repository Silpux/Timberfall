using UnityEngine;

public abstract class WorkerBuilding<W> : WorkerBuildingBase where W : Worker{

    [SerializeField] protected W workerPrefab;
    protected virtual void SpawnWorker(WorkerGrade grade){
        W newWorker = Instantiate(workerPrefab, workerSpawnPosition.position, Quaternion.identity);
        newWorker.Grade = grade;
        newWorker.Building = this;
        Workers.Add(newWorker);
    }
}
