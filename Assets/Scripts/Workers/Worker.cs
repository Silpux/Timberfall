using UnityEngine;

public abstract class Worker : MonoBehaviour{
    
    public WorkerBuildingBase Building{get; set;}
    public abstract WorkerGrade Grade{get; set;}
    public abstract WorkerData WorkerData{get;}
    public void Remove(){
        Building.RemoveWorker(this);
    }

}
