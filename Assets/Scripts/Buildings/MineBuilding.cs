using UnityEngine;

public class MineBuilding : WorkerBuilding<MineWorker>{
    public override void OnClick(){
        PanelManager.Instance.OpenMinePanel(this);
    }
    public override bool AddWorker(WorkerGrade grade){
        if(Inventory.Instance.ConfirmByuingMineWorker(grade, GetCostForNextWorker())){
            SpawnWorker(grade);
            return true;
        }
        return false;
    }
    public override int GetCostForNextWorker() => (1 << Workers.Count) * 200;
}
