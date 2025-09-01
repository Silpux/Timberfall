using UnityEngine;

public class LumbermillWorker : Worker{

    public LumbermillBuilding Building{get; set;}
    public LumbermillWorker(WorkerGrade grade) : base(grade){
    }

    public void Remove(){
        Building.RemoveWorker(this);
    }
}
