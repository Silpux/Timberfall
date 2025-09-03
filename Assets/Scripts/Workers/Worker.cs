using UnityEngine;
using UnityEngine.AI;

public abstract class Worker : MonoBehaviour{
    
    protected NavMeshAgent agent;
    public WorkerBuildingBase Building{get; set;}
    public abstract WorkerGrade Grade{get; set;}
    public abstract WorkerData WorkerData{get;}
    public abstract void SetDestination(Vector3 position);
    public void Remove(){
        Building.RemoveWorker(this);
    }
    protected virtual void Awake(){
        agent = GetComponent<NavMeshAgent>();
    }

}
