using TMPro;
using UnityEngine;

public abstract class BuildingPanel<B> : MonoBehaviour where B : WorkerBuilding{

    protected B building;
    [SerializeField] private WorkerEntry workerEntryPrefab;
    [SerializeField] private GameObject workersListParent;
    [SerializeField] private TextMeshProUGUI costText;

    public void SetBuilding(B building){
        ResetUI();
        this.building = building;
        FillWorkersUI();
        costText.text = building.GetCostForNextWorker().ToString();
    }

    public void RefreshUI() => SetBuilding(building);

    public void FillWorkersUI(){
        foreach(var w in building.Workers){
            WorkerEntry newEntry = Instantiate(workerEntryPrefab, workersListParent.transform);
            newEntry.SetWorker(w);
            newEntry.OnRemove += RefreshUI;
        }
    }

    public void ResetUI(){
        foreach(Transform child in workersListParent.transform){
            Destroy(child.gameObject);
        }
    }

    public abstract void Close();

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
