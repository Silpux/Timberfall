using UnityEngine;

public abstract class WorkerData{
    public WorkerGrade Grade{get; private set;}
    public WorkerBuildingBase Building{get;set;}

    public WorkerData(WorkerBuildingBase building, WorkerGrade grade){
        Building = building;
        Grade = grade;
    }
}
