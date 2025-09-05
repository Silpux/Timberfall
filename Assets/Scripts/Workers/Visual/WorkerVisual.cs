using UnityEngine;

public abstract class WorkerVisual<W> : WorkerVisualBase where W : Worker{

    [SerializeField] protected W worker;

    protected const string IS_WALKING_ANIMATION = "IsWalking";
    protected const string INTERACT_ANIMATION = "Interact";

    protected Animator animator;

    protected virtual void Awake(){
        animator = GetComponent<Animator>();
    }

    protected virtual void Walk(){
        animator.SetBool(IS_WALKING_ANIMATION, true);
    }

    protected virtual void Idle(){
        animator.SetBool(IS_WALKING_ANIMATION, false);
    }

    protected virtual void GiveResource(){
        animator.SetTrigger(INTERACT_ANIMATION);
    }

    protected virtual void OnEnable(){
        worker.OnIdle += Idle;
        worker.OnWalk += Walk;
        worker.OnGiveResource += GiveResource;
    }

    protected virtual void OnDisable(){
        worker.OnIdle -= Idle;
        worker.OnWalk -= Walk;
        worker.OnGiveResource -= GiveResource;
    }

}
