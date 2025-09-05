using System;
using UnityEngine;
using UnityEngine.AI;

public class LumbermillWorker : Worker{

    private enum State{
        Idle = 0,
        GoingToTree,
        CuttingTree,
        GoingToLumbermill,
        GoingToLumbermillStuck,
        WaitForAnimationFinish
    }

    public override event Action OnWalk;
    public override event Action OnIdle;
    public override event Action OnGiveResource;
    public event Action OnTreeHit;

    [SerializeField] private Transform axeHand;
    public Transform AxeHand => axeHand;
    [SerializeField] private float hitCooldown;
    private float currentHitCooldown;

    public LumbermillResource ResourcePrefab{get; set;}

    private LumbermillResource currentHoldingResource;

    public Axe axe{get; set;}
    public ItemDataSO ObtainedItem{get; set;}
    private int currentItemAmount;

    public override WorkerData WorkerData => new LumbermillWorkerData(Grade);
    public override WorkerGrade Grade{get; set;}

    private State state;
    private State CurrentState{
        get => state;
        set{
            if(state != value){
                switch(value){
                    case State.Idle:
                    case State.GoingToLumbermillStuck:
                    case State.CuttingTree:
                        Debug.Log("Invoke idle");
                        OnIdle?.Invoke();
                        break;
                    case State.GoingToTree:
                    case State.GoingToLumbermill:
                        Debug.Log("Invoke walk");
                        OnWalk?.Invoke();
                    break;
                }
            }
            state = value;
        }
    }


    private TreeObj targetTree;
    public TreeObj TargetTree{
        get => targetTree;
        private set{
            if(targetTree != null){
                targetTree.IsTargeted = false;
            }
            if(value != null){
                value.IsTargeted = true;
            }
            targetTree = value;
        }
    }

    private void Start(){
        SetTreeTarget();
    }

    protected void OnEnable(){
        (visual as LumbermillWorkerVisual).OnGiveResourceFinished += GiveResourceFinished;
        (visual as LumbermillWorkerVisual).OnHitFinished += HitFinished;
    }

    protected void OnDisable(){
        (visual as LumbermillWorkerVisual).OnGiveResourceFinished -= GiveResourceFinished;
        (visual as LumbermillWorkerVisual).OnHitFinished -= HitFinished;
    }

    private void HitFinished(){
        CurrentState = State.CuttingTree;
        targetTree.TakeDamage(this, axe.Damage);
        currentHitCooldown = hitCooldown;
    }

    private void SetTreeTarget(){
        TargetTree = null;
        TreeObj tree = TileManager.Instance.GetClosestFreeTree(transform);
        if(tree != null){
            Vector3 offsetPoint = GetOffsetPoint(transform.position, tree.transform.position, 0.75f);
            if(IsReachableTarget(offsetPoint)){
                SetDestination(offsetPoint);
                TargetTree = tree;
                CurrentState = State.GoingToTree;
            }
            else{
                CurrentState = State.Idle;
            }
        }
        else{
            CurrentState = State.Idle;
        }
    }

    private void MakeHit(){
        OnTreeHit?.Invoke();
        CurrentState = State.WaitForAnimationFinish;
    }

    private void GiveResource(){
        OnGiveResource?.Invoke();
        (Building as LumbermillBuilding).AcceptResource(ObtainedItem, currentItemAmount);
        Destroy(currentHoldingResource.gameObject);
        currentItemAmount = 0;
        CurrentState = State.WaitForAnimationFinish;
    }

    protected void GiveResourceFinished(){
        SetTreeTarget();
    }

    private void SetLumbermillTarget(){
        TargetTree = null;
        SetDestination(Building.TargetPoint.position);
    }

    public void AcceptWood(int amount){
        currentItemAmount = amount;
        LumbermillResource resource = Instantiate(ResourcePrefab, ResourceHand);
        resource.transform.localPosition = Vector3.zero;
        currentHoldingResource = resource;
        SetLumbermillTarget();
        CurrentState = State.GoingToLumbermill;
    }

    private void Update(){
        switch(CurrentState){
            case State.Idle:
                SetTreeTarget();
                break;
            case State.GoingToTree:
                if(agent.pathStatus != NavMeshPathStatus.PathComplete){
                    TargetTree = null;
                    CurrentState = State.Idle;
                    agent.ResetPath();
                    break;
                }
                if(ReachedDestination() && FaceTarget(TargetTree.transform)){
                    CurrentState = State.CuttingTree;
                    currentHitCooldown = 0;
                }
                break;
            case State.CuttingTree:
                currentHitCooldown -= Time.deltaTime;
                if(currentHitCooldown <= 0){
                    MakeHit();
                }
                break;
            case State.GoingToLumbermill:
                if(agent.pathStatus != NavMeshPathStatus.PathComplete){
                    CurrentState = State.GoingToLumbermillStuck;
                    break;
                }
                if(ReachedDestination() && FaceTarget(Building.transform)){
                    GiveResource();
                }
                break;
            case State.GoingToLumbermillStuck:
                if(!agent.pathPending){
                    if(agent.pathStatus == NavMeshPathStatus.PathComplete){
                        CurrentState = State.GoingToLumbermill;
                    }
                    else{
                        agent.velocity = Vector3.zero;
                        SetLumbermillTarget();
                    }
                }
                break;
            default:
                break;
        }
    }
    public override void Remove(){
        TargetTree.IsTargeted = false;
        base.Remove();
    }

    private Vector3 GetOffsetPoint(Vector3 agentPos, Vector3 targetPos, float radius){
        Vector3 dir = (agentPos - targetPos).normalized;
        Vector3 offsetPos = targetPos + dir * radius;

        if(NavMesh.SamplePosition(offsetPos, out NavMeshHit hit, 1.0f, NavMesh.AllAreas)){
            return hit.position;
        }
        return targetPos;
    }

    public override void SetDestination(Vector3 position){
        agent.SetDestination(position);
    }

    public override void Clear(){
        TargetTree = null;
    }
}
