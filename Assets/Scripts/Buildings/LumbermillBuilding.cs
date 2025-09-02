using UnityEngine;

public class LumbermillBuilding : WorkerBuilding<LumbermillWorkerData>{

    public override void OnClick(){
        Debug.Log("Open lumbermill");
        PanelManager.Instance.OpenLumbermillPanel(this);
    }

    public override int GetCostForNextWorker() => (1 << Workers.Count) * 100;

    public override bool AddWorker(WorkerGrade grade){
        if(Inventory.Instance.CanBuyLumbermillWorker(grade)){
            LumbermillWorkerData newWorker = new LumbermillWorkerData(grade){
                Building = this
            };
            Workers.Add(newWorker);
            return true;
        }
        return false;
    }
}
