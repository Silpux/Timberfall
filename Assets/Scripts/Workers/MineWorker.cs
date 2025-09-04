using UnityEngine;
using UnityEngine.AI;

public class MineWorker : Worker{

    private enum State{
        Idle = 0,
        GoingToForge,
        GoingToForgeStuck,
        GoingToMine,
        GoingToMineStuck
    }

    public override WorkerData WorkerData => new MineWorkerData(Grade);

    public ForgeBuilding CurrentForge{get; private set;}

    private MineWorkerDataSO currentResource;
    public override WorkerGrade Grade{get; set;}

    private State state;

    private void SetForgeTarget(){
        CurrentForge = null;
        ForgeBuilding forge = TileManager.Instance.GetClosestBuilding<ForgeBuilding>(transform);
        agent.ResetPath();
        if(forge != null){
            if(IsReachableTarget(forge.TargetPoint.position)){
                SetDestination(forge.TargetPoint.position);
                CurrentForge = forge;
                state = State.GoingToForge;
            }
            else{
                state = State.GoingToForgeStuck;
                Debug.Log("not reachable target");
            }
        }
        else{
            Debug.Log("Forge is null");
            state = State.GoingToForgeStuck;
        }
    }

    public void AcceptResource(MineWorkerDataSO resource){
        currentResource = resource;
        SetForgeTarget();
    }

    private void SetMineTarget(){
        CurrentForge = null;
        SetDestination(Building.TargetPoint.position);
    }

    private void Start(){
        state = State.Idle;
    }

    private void Update(){

        switch(state){
            case State.Idle:
                SetMineTarget();
                state = State.GoingToMine;
                break;
            case State.GoingToMine:
                if(agent.pathStatus != NavMeshPathStatus.PathComplete){
                    state = State.GoingToMineStuck;
                    break;
                }
                if(ReachedDestination()){
                    (Building as MineBuilding).AcceptWorker(this);
                }
                break;
            case State.GoingToMineStuck:
                if(!agent.pathPending){
                    if(agent.pathStatus == NavMeshPathStatus.PathComplete){
                        state = State.GoingToMine;
                    }
                    else{
                        agent.velocity = Vector3.zero;
                        SetMineTarget();
                    }
                }
                break;
            case State.GoingToForge:
                if(agent.pathStatus != NavMeshPathStatus.PathComplete || CurrentForge == null){
                    state = State.GoingToForgeStuck;
                    break;
                }
                if(ReachedDestination()){
                    CurrentForge.AcceptResource(currentResource);
                    currentResource = null;
                    state = State.Idle;
                }
                break;
            case State.GoingToForgeStuck:
                if(!agent.pathPending){
                    if(agent.pathStatus == NavMeshPathStatus.PathComplete && CurrentForge != null){
                        state = State.GoingToForge;
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
