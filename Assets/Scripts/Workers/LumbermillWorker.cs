using UnityEngine;

public class LumbermillWorker : Worker{
    public override WorkerData WorkerData => new LumbermillWorkerData(Building, grade);
    private WorkerGrade grade;
    public override WorkerGrade Grade{
        get => grade;
        set{
            grade = value;
        }
    }

    public void SetBuilding(LumbermillBuilding building){
        Building = building;
    }

}
