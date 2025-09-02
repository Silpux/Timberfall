using UnityEngine;

public abstract class WorkerData{
    public WorkerGrade Grade{get; private set;}
    public WorkerBuildingBase Building{get;set;}

    public WorkerData(WorkerGrade grade){
        Grade = grade;
    }

    public void Remove(){
        Building.RemoveWorker(this);
        Building = null;
    }
}
