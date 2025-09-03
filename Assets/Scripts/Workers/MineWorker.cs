using UnityEngine;

public class MineWorker : Worker{
    public override WorkerData WorkerData => new MineWorkerData(Building, grade);

    private WorkerGrade grade;
    public override WorkerGrade Grade{
        get => grade;
        set{
            grade = value;
        }
    }

    public void SetBuilding(MineBuilding building){
        Building = building;
    }
    public override void SetDestination(Vector3 position){
        agent.SetDestination(position);
    }
}
