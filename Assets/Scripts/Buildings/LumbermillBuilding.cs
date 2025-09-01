using System.Collections.Generic;
using UnityEngine;

public class LumbermillBuilding : Building{

    public List<LumbermillWorker> Workers{get; private set;} = new();

    public override void OnClick(){
        Debug.Log("Open lumbermill");
        PanelManager.Instance.OpenLumbermillPanel(this);
    }

    public int GetCostForNextWorker(){
        if(Workers.Count == 0){
            return 100;
        }
        return (1 << Workers.Count) * 100;
    }

    public void AddWorker(WorkerGrade grade){
        LumbermillWorker newWorker = new LumbermillWorker(grade){
            Building = this
        };
        Workers.Add(newWorker);
    }

    public void RemoveWorker(LumbermillWorker worker){
        Workers.Remove(worker);
        worker.Building = null;
    }
}
