using UnityEngine;

public class LumbermillBuilding : WorkerBuilding<LumbermillWorker>{

    public override void OnClick(){
        PanelManager.Instance.OpenLumbermillPanel(this);
    }

    public override bool AddWorker(WorkerGrade grade){
        if(Inventory.Instance.ConfirmByuingLumbermillWorker(grade, GetCostForNextWorker())){
            SpawnWorker(grade);
            return true;
        }
        return false;
    }

    public override int GetCostForNextWorker() => (1 << Workers.Count) * 100;
}
