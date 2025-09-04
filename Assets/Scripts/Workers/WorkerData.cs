using UnityEngine;

public abstract class WorkerData{
    public WorkerGrade Grade{get; private set;}

    public WorkerData(WorkerGrade grade){
        Grade = grade;
    }
}
