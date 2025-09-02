using UnityEngine;

public class MineBuilding : WorkerBuilding<MineWorker>{
    public override void OnClick(){
        Debug.Log("Open Mine");
        PanelManager.Instance.OpenMinePanel(this);
    }
    public override bool AddWorker(WorkerGrade grade){
        if(Inventory.Instance.CanBuyMineWorker(grade)){
            MineWorker newWorker = new MineWorker(grade){
                Building = this
            };
            Workers.Add(newWorker);
            return true;
        }
        return false;
    }
    public override int GetCostForNextWorker() => (1 << Workers.Count) * 200;
}
