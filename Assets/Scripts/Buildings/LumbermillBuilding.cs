using UnityEngine;

public class LumbermillBuilding : WorkerBuilding<LumbermillWorker>{

    [SerializeField] private LumbermillWorkerDataSO woodenWorkerDamage;
    [SerializeField] private LumbermillWorkerDataSO stoneWorkerDamage;
    [SerializeField] private LumbermillWorkerDataSO ironWorkerDamage;
    [SerializeField] private LumbermillWorkerDataSO diamondWorkerDamage;

    private LumbermillWorkerDataSO GetResourceForGrade(WorkerGrade grade) => grade switch{
        WorkerGrade.Wood => woodenWorkerDamage,
        WorkerGrade.Stone => stoneWorkerDamage,
        WorkerGrade.Iron => ironWorkerDamage,
        WorkerGrade.Diamond => diamondWorkerDamage,
        _ => woodenWorkerDamage
    };

    public override void OnClick(){
        PanelManager.Instance.OpenLumbermillPanel(this);
    }

    public override bool AddWorker(WorkerGrade grade){
        if(Inventory.Instance.ConfirmByuingLumbermillWorker(grade, GetCostForNextWorker())){
            LumbermillWorker newWorker = SpawnWorker(grade);
            LumbermillWorkerDataSO data = GetResourceForGrade(grade);
            newWorker.Damage = data.Damage;
            newWorker.ObtainedItem = data.ItemData;
            return true;
        }
        return false;
    }

    public void AcceptResource(ItemDataSO item, int amount){
        Inventory.Instance.AddItem(item, amount);
    }

    public override int GetCostForNextWorker() => (1 << Workers.Count) * 100;
}
