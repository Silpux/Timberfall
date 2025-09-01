using TMPro;
using UnityEngine;

public class LumbermillPanel : MonoBehaviour{

    private LumbermillBuilding lumbermillBuilding;

    [SerializeField] private LumbermillWorkerEntry workerEntryPrefab;

    [SerializeField] private GameObject workersListParent;

    [SerializeField] private TextMeshProUGUI costText;

    public void SetLumbermillBuilding(LumbermillBuilding building){
        ResetUI();
        lumbermillBuilding = building;
        FillWorkersUI();
        costText.text = lumbermillBuilding.GetCostForNextWorker().ToString();
    }

    public void FillWorkersUI(){
        foreach(var w in lumbermillBuilding.Workers){
            LumbermillWorkerEntry newEntry = Instantiate(workerEntryPrefab, workersListParent.transform);
            newEntry.SetWorker(w);
            newEntry.OnRemove += () => SetLumbermillBuilding(lumbermillBuilding);
        }
    }

    public void ResetUI(){
        foreach(Transform child in workersListParent.transform){
            Destroy(child.gameObject);
        }
    }

    public void AddWoodAxeWorker() => BuyWorker(WorkerGrade.Wood);
    public void AddStoneAxeWorker() => BuyWorker(WorkerGrade.Stone);
    public void AddIronAxeWorker() => BuyWorker(WorkerGrade.Iron);
    public void AddDiamondAxeWorker() => BuyWorker(WorkerGrade.Diamond);

    public void BuyWorker(WorkerGrade grade){
        if(Inventory.Instance.ConfirmBuyingLumbermillWorker(grade)){
            lumbermillBuilding.AddWorker(grade);
            SetLumbermillBuilding(lumbermillBuilding);
        }
    }

}
