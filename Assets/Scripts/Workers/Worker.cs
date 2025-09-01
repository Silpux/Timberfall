using UnityEngine;

public class Worker{
    public WorkerGrade Grade{get; private set;}

    public Worker(WorkerGrade grade){
        Grade = grade;
    }
}
