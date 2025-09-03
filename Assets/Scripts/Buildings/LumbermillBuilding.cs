using UnityEngine;

public class LumbermillBuilding : WorkerBuilding<LumbermillWorker>{

    public override void OnClick(){
        Debug.Log("Open lumbermill");
        PanelManager.Instance.OpenLumbermillPanel(this);
    }

    public override bool AddWorker(WorkerGrade grade){
        if(Inventory.Instance.CanBuyLumbermillWorker(grade)){
            LumbermillWorker newWorker = Instantiate(workerPrefab, transform.position + Vector3.forward * 3, Quaternion.identity);
            newWorker.Grade = grade;
            newWorker.Building = this;
            Workers.Add(newWorker);
            return true;
        }
        return false;
    }

    public override int GetCostForNextWorker() => (1 << Workers.Count) * 100;
}
