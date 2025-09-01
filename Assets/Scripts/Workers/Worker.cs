using UnityEngine;

public class Worker{
    public WorkerGrade Grade{get; private set;}
    public WorkerBuilding Building{get;set;}

    public Worker(WorkerGrade grade){
        Grade = grade;
    }

    public void Remove(){
        Building.RemoveWorker(this);
        Building = null;
    }
}
