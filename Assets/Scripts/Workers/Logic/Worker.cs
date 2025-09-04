using System;
using UnityEngine;
using UnityEngine.AI;

public abstract class Worker : MonoBehaviour{
    
    public abstract event Action OnWalk;
    public abstract event Action OnIdle;
    public abstract event Action OnGiveResource;

    [SerializeField] protected WorkerVisualBase visual;
    
    [SerializeField] private Transform resourceHand;
    public Transform ResourceHand => resourceHand;
    
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
    
    protected bool FaceTarget(Transform target, float rotationSpeed = 5f){
        Vector3 direction = target.position - transform.position;
        direction.y = 0f;

        if(direction.sqrMagnitude < 0.001f){
            return true;
        }

        Vector3 norm = direction.normalized;
        float dot = Vector3.Dot(transform.forward, norm);
        if(dot > 0.99f){
            return true;
        }

        Quaternion targetRotation = Quaternion.LookRotation(direction);

        transform.rotation = Quaternion.Slerp(
            transform.rotation,
            targetRotation,
            Time.deltaTime * rotationSpeed
        );
        return false;
    }

}
