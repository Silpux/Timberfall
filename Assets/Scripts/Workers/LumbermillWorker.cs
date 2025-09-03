using UnityEngine;
using UnityEngine.AI;

public class LumbermillWorker : Worker{

    private enum State{
        Idle = 0,
        GoingToTree,
        CuttingTree,
        GoingToLumbermill,
    }

    [SerializeField] private float hitCooldown;
    private float currentHitCooldown;

    public override WorkerData WorkerData => new LumbermillWorkerData(Building, grade);
    private WorkerGrade grade;
    public override WorkerGrade Grade{
        get => grade;
        set{
            grade = value;
        }
    }

    private State state;

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
        state = State.Idle;
        SetTreeTarget();
    }

    private void SetTreeTarget(){
        TreeObj tree = TileManager.Instance.GetClosestFreeTree(transform);
        if(tree != null){
            Vector3 offsetPoint = GetOffsetPoint(transform.position, tree.transform.position, 1f);
            SetDestination(offsetPoint);
            TargetTree = tree;
            state = State.GoingToTree;
        }
        else{
            state = State.Idle;
        }
    }

    private void SetLumbermillTarget(){
        TargetTree = null;
        SetDestination(Building.WorkerTarget.position);
    }

    private void Update(){
        switch(state){
            case State.Idle:
                SetTreeTarget();
                break;
            case State.GoingToTree:
                if(ReachedDestination() && FaceTarget(TargetTree.transform)){
                    state = State.CuttingTree;
                    currentHitCooldown = 3;
                }
                break;
            case State.CuttingTree:
                currentHitCooldown -= Time.deltaTime;
                if(currentHitCooldown <= 0){
                    SetLumbermillTarget();
                    state = State.GoingToLumbermill;
                }
                break;
            case State.GoingToLumbermill:
                if(ReachedDestination()){
                    SetTreeTarget();
                    state = State.Idle;
                }
                break;
            default:
                break;
        }
    }

    private bool FaceTarget(Transform target, float rotationSpeed = 5f){
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

    private bool ReachedDestination(){
        bool reached =  !agent.pathPending &&
            agent.remainingDistance <= agent.stoppingDistance &&
            (!agent.hasPath || agent.velocity.sqrMagnitude == 0f);
            return reached;
    }

    private Vector3 GetOffsetPoint(Vector3 agentPos, Vector3 targetPos, float radius){
        Vector3 dir = (agentPos - targetPos).normalized;
        Vector3 offsetPos = targetPos + dir * radius;

        if(NavMesh.SamplePosition(offsetPos, out NavMeshHit hit, 1.0f, NavMesh.AllAreas)){
            return hit.position;
        }
        return targetPos;
    }

    public void SetBuilding(LumbermillBuilding building){
        Building = building;
    }

    public override void SetDestination(Vector3 position){
        agent.SetDestination(position);
    }
}
