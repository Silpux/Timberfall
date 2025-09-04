using System.Collections.Generic;
using UnityEngine;

public class MineBuilding : WorkerBuilding<MineWorker>{
    private class WorkerEntry{
        public MineWorker Worker{get; set;}
        public float RemainingTime{get; set;}
    }

    [SerializeField] private MineWorkerDataSO woodenWorkerResource;
    [SerializeField] private MineWorkerDataSO stoneWorkerResource;
    [SerializeField] private MineWorkerDataSO ironWorkerResource;
    [SerializeField] private MineWorkerDataSO diamondWorkerResource;

    private MineWorkerDataSO GetResourceForGrade(WorkerGrade grade) => grade switch{
        WorkerGrade.Wood => woodenWorkerResource,
        WorkerGrade.Stone => stoneWorkerResource,
        WorkerGrade.Iron => ironWorkerResource,
        WorkerGrade.Diamond => diamondWorkerResource,
        _ => woodenWorkerResource
    };

    public override void OnClick(){
        PanelManager.Instance.OpenMinePanel(this);
    }
    public override bool AddWorker(WorkerGrade grade){
        if(Inventory.Instance.ConfirmByuingMineWorker(grade, GetCostForNextWorker())){
            SpawnWorker(grade);
            return true;
        }
        return false;
    }
    public override int GetCostForNextWorker() => (1 << Workers.Count) * 200;

    private List<WorkerEntry> activeWorkers = new List<WorkerEntry>();

    public void AcceptWorker(MineWorker worker){
        worker.gameObject.SetActive(false);
        activeWorkers.Add(new WorkerEntry{
            Worker = worker,
            RemainingTime = GetResourceForGrade(worker.Grade).WorkingTime
        });
    }

    private void Update(){
        for(int i = activeWorkers.Count-1; i>=0; i--){
            WorkerEntry entry = activeWorkers[i];
            entry.RemainingTime -= Time.deltaTime;
            if(entry.RemainingTime <= 0f){
                activeWorkers.RemoveAt(i);
                entry.Worker.gameObject.SetActive(true);
                entry.Worker.AcceptResource(GetResourceForGrade(entry.Worker.Grade));

                Vector3 dir = entry.Worker.transform.position - transform.position;
                dir.y = 0;
                entry.Worker.transform.rotation = Quaternion.LookRotation(dir, Vector3.up);
            }
        }
    }

}
