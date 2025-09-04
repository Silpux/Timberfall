using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class MineWorker : Worker{

    private enum State{
        Idle = 0,
        GoingToForge,
        GoingToForgeStuck,
        GoingToMine,
        GoingToMineStuck,
        WaitForAnimationFinish
    }

    public override WorkerData WorkerData => new MineWorkerData(Grade);

    public ForgeBuilding CurrentForge{get; private set;}

    private MineWorkerDataSO currentResource;
    public override WorkerGrade Grade{get; set;}

    private MineResource currentHoldingResource;


    public override event Action OnWalk;
    public override event Action OnIdle;
    public override event Action OnGiveResource;

    private State state;
    private State CurrentState{
        get => state;
        set{
            if(state != value){
                switch(value){
                    case State.Idle:
                    case State.GoingToForgeStuck:
                    case State.GoingToMineStuck:
                        OnIdle?.Invoke();
                        break;
                    case State.GoingToForge:
                    case State.GoingToMine:
                        OnWalk?.Invoke();
                    break;
                }
            }
            state = value;
        }
    }

    protected void GiveResourceFinished(){
        CurrentState = State.Idle;
    }

    private void SetForgeTarget(){
        CurrentForge = null;
        ForgeBuilding forge = TileManager.Instance.GetClosestBuilding<ForgeBuilding>(transform);
        agent.ResetPath();
        if(forge != null){
            if(IsReachableTarget(forge.TargetPoint.position)){
                SetDestination(forge.TargetPoint.position);
                CurrentForge = forge;
                CurrentState = State.GoingToForge;
            }
            else{
                CurrentState = State.GoingToForgeStuck;
                Debug.Log("not reachable target");
            }
        }
        else{
            Debug.Log("Forge is null");
            CurrentState = State.GoingToForgeStuck;
        }
    }

    public void AcceptResource(MineWorkerDataSO resource){
        currentResource = resource;
        MineResource mineResource = Instantiate(resource.Prefab, ResourceHand);
        mineResource.transform.localPosition = Vector3.zero;
        currentHoldingResource = mineResource;
        SetForgeTarget();
    }

    private void SetMineTarget(){
        CurrentForge = null;
        SetDestination(Building.TargetPoint.position);
    }

    private void GiveResource(){
        OnGiveResource?.Invoke();
        CurrentForge.AcceptResource(currentResource);
        Destroy(currentHoldingResource.gameObject);
        currentResource = null;
        CurrentState = State.WaitForAnimationFinish;
    }

    private void Start(){
        CurrentState = State.Idle;
    }
    protected void OnEnable(){
        (visual as MineWorkerVisual).OnGiveResourceFinished += GiveResourceFinished;
    }

    protected void OnDisable(){
        (visual as MineWorkerVisual).OnGiveResourceFinished -= GiveResourceFinished;
    }

    private void Update(){

        switch(CurrentState){
            case State.Idle:
                SetMineTarget();
                CurrentState = State.GoingToMine;
                break;
            case State.GoingToMine:
                if(agent.pathStatus != NavMeshPathStatus.PathComplete){
                    CurrentState = State.GoingToMineStuck;
                    break;
                }
                if(ReachedDestination() && FaceTarget(Building.transform)){
                    (Building as MineBuilding).AcceptWorker(this);
                }
                break;
            case State.GoingToMineStuck:
                if(!agent.pathPending){
                    if(agent.pathStatus == NavMeshPathStatus.PathComplete){
                        CurrentState = State.GoingToMine;
                    }
                    else{
                        agent.velocity = Vector3.zero;
                        SetMineTarget();
                    }
                }
                break;
            case State.GoingToForge:
                if(agent.pathStatus != NavMeshPathStatus.PathComplete || CurrentForge == null){
                    CurrentState = State.GoingToForgeStuck;
                    break;
                }
                if(ReachedDestination() && FaceTarget(CurrentForge.transform)){
                    GiveResource();
                }
                break;
            case State.GoingToForgeStuck:
                if(!agent.pathPending){
                    if(agent.pathStatus == NavMeshPathStatus.PathComplete && CurrentForge != null){
                        CurrentState = State.GoingToForge;
                    }
                    else{
                        agent.velocity = Vector3.zero;
                        SetForgeTarget();
                    }
                }
                break;
            default:
                break;
        }
    }

    public override void SetDestination(Vector3 position){
        agent.SetDestination(position);
    }

    public override void Clear(){

    }

}
