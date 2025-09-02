using TMPro;
using UnityEngine;

public abstract class WorkerBuildingPanel<B> : BuildingPanel<B> where B : WorkerBuilding{

    [SerializeField] private WorkerEntry workerEntryPrefab;
    [SerializeField] private Transform workersListParent;
    [SerializeField] private TextMeshProUGUI costText;

    public override void SetBuilding(B building){
        ResetUI();
        this.building = building;
        FillWorkersUI();
        costText.text = building.GetCostForNextWorker().ToString();
    }

    public override void RefreshUI() => SetBuilding(building);

    public void FillWorkersUI(){
        foreach(var w in building.Workers){
            WorkerEntry newEntry = Instantiate(workerEntryPrefab, workersListParent);
            newEntry.SetWorker(w);
            newEntry.OnRemove += RefreshUI;
        }
    }

    public override void ResetUI(){
        foreach(Transform child in workersListParent){
            Destroy(child.gameObject);
        }
    }

    public override void RemoveBuilding(){
        Close();
        building.Remove();
    }

    public void AddWoodWorker() => BuyWorker(WorkerGrade.Wood);
    public void AddStoneWorker() => BuyWorker(WorkerGrade.Stone);
    public void AddIronWorker() => BuyWorker(WorkerGrade.Iron);
    public void AddDiamondWorker() => BuyWorker(WorkerGrade.Diamond);

    public void BuyWorker(WorkerGrade grade){
        if(building.AddWorker(grade)){
            RefreshUI();
        }
    }
}
