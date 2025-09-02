using UnityEngine;

public class LumbermillBuilding : WorkerBuilding<LumbermillWorker>{

    public override void OnClick(){
        Debug.Log("Open lumbermill");
        PanelManager.Instance.OpenLumbermillPanel(this);
    }

    public override int GetCostForNextWorker() => (1 << Workers.Count) * 100;

    public override bool AddWorker(WorkerGrade grade){
        if(Inventory.Instance.CanBuyLumbermillWorker(grade)){
            LumbermillWorker newWorker = new LumbermillWorker(grade){
                Building = this
            };
            Workers.Add(newWorker);
            return true;
        }
        return false;
    }
}
