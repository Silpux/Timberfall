using System.Collections.Generic;

public abstract class WorkerBuildingBase : Building{
    public List<Worker> Workers{get;} = new();
    public abstract int GetCostForNextWorker();
    public abstract bool AddWorker(WorkerGrade grade);
    public void RemoveWorker(Worker worker){
        Workers.Remove(worker);
    }
    public override void Remove(){
        base.Remove();
        Destroy(gameObject);
    }
}
