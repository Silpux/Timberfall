using System.Collections.Generic;
using UnityEngine;

public abstract class WorkerBuildingBase : Building, ITargetable{
    public List<Worker> Workers{get;} = new();
    public IEnumerable<WorkerData> WorkerDatas{
        get{
            foreach(Worker w in Workers){
                yield return w.WorkerData;
            }
        }
    }
    [SerializeField] protected Transform workerSpawnPosition;
    public Transform TargetPoint => workerSpawnPosition;
    public abstract int GetCostForNextWorker();
    public abstract bool AddWorker(WorkerGrade grade);
    public void RemoveWorker(Worker worker){
        Workers.Remove(worker);
        Destroy(worker.gameObject);
    }
    public override void Remove(){
        for(int i = Workers.Count-1;i>=0;i--){
            Workers[i].Clear();
            RemoveWorker(Workers[i]);
        }
        base.Remove();
        Destroy(gameObject);
    }
}
