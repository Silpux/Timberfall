using TMPro;
using UnityEngine;
using UnityEngine.UI;

public abstract class WorkerBuildingPanel<B> : BuildingPanel<B> where B : WorkerBuildingBase{

    [SerializeField] protected WorkerEntry workerEntryPrefab;
    [SerializeField] protected Transform workersListParent;
    [SerializeField] protected TextMeshProUGUI costText;

    [SerializeField] protected Image buyWoodenWorkerButtonBackground;
    [SerializeField] protected Image buyStoneWorkerButtonBackground;
    [SerializeField] protected Image buyIronWorkerButtonBackground;
    [SerializeField] protected Image buyDiamondWorkerButtonBackground;

    [SerializeField] protected Sprite backgroundOkSprite;
    [SerializeField] protected Sprite backgroundNoSprite;

    public override void SetBuilding(B building){
        ResetUI();
        this.building = building;
        FillWorkersUI();
        costText.text = building.GetCostForNextWorker().ToString();
        UpdateBuyWorkerButtons();
    }

    public override void RefreshUI() => SetBuilding(building);

    public void FillWorkersUI(){
        foreach(var w in building.Workers){
            WorkerEntry newEntry = Instantiate(workerEntryPrefab, workersListParent);
            newEntry.SetWorker(w);
            newEntry.OnRemove += RefreshUI;
        }
    }

    private void OnEnable(){
        Inventory.Instance.OnInventoryUpdate += UpdateBuyWorkerButtons;
    }

    private void OnDisable(){
        Inventory.Instance.OnInventoryUpdate -= UpdateBuyWorkerButtons;
    }

    protected abstract void UpdateBuyWorkerButtons();

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
