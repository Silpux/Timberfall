using System;
using UnityEngine;

public class LumbermillWorkerVisual : WorkerVisual<LumbermillWorker>{

    public event Action OnHitFinished;
    private const string TREE_HIT_ANIMATION = "TreeHit";

    private void TreeHit(){
        animator.SetTrigger(TREE_HIT_ANIMATION);
    }

    public void HitFinishedAnimationEvent(){
        OnHitFinished?.Invoke();
    }

    public event Action OnGiveResourceFinished;

    public void GiveResourceFinishedAnimationEvent(){
        OnGiveResourceFinished?.Invoke();
    }
    
    protected override void OnEnable(){
        base.OnEnable();
        worker.OnTreeHit += TreeHit;
    }
    protected override void OnDisable(){
        base.OnDisable();
        worker.OnTreeHit -= TreeHit;
    }
}
