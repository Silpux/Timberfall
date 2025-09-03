using UnityEngine;
using UnityEngine.AI;

public abstract class Worker : MonoBehaviour{
    
    protected NavMeshAgent agent;
    public WorkerBuildingBase Building{get; set;}
    public abstract WorkerGrade Grade{get; set;}
    public abstract WorkerData WorkerData{get;}
    public abstract void SetDestination(Vector3 position);
    public abstract void Clear();
    public virtual void Remove(){
        Building.RemoveWorker(this);
    }
    protected virtual void Awake(){
        agent = GetComponent<NavMeshAgent>();
    }
    protected bool IsReachableTarget(Vector3 targetPos){
        NavMeshPath path = new NavMeshPath();
        if(agent.CalculatePath(targetPos, path)){
            return path.status == NavMeshPathStatus.PathComplete;
        }
        return false;
    }

}
