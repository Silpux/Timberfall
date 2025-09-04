using System;
using UnityEngine;
using UnityEngine.AI;

public abstract class Worker : MonoBehaviour{
    
    public abstract event Action OnWalk;
    public abstract event Action OnIdle;
    public abstract event Action OnGiveResource;

    [SerializeField] protected WorkerVisualBase visual;
    
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
        agent.speed *= UnityEngine.Random.Range(0.9f, 1.1f);
    }
    protected bool IsReachableTarget(Vector3 targetPos){
        NavMeshPath path = new NavMeshPath();
        if(agent.CalculatePath(targetPos, path)){
            return path.status == NavMeshPathStatus.PathComplete;
        }
        Debug.Log("CalculatePath false");
        return false;
    }
    protected bool ReachedDestination(){
        return !agent.pathPending &&
            agent.remainingDistance <= agent.stoppingDistance &&
            (!agent.hasPath || agent.velocity.sqrMagnitude == 0f);
    }

}
