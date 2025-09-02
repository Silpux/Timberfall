using System.Collections.Generic;

public abstract class WorkerBuildingBase : Building{
    public List<WorkerData> Workers{get;} = new();
    public abstract int GetCostForNextWorker();
    public abstract bool AddWorker(WorkerGrade grade);
    public void RemoveWorker(WorkerData worker){
        Workers.Remove(worker);
    }
    public override void Remove(){
        base.Remove();
        Destroy(gameObject);
    }
}
