using System;
using UnityEngine;

public class LumbermillWorkerVisual : WorkerVisual<LumbermillWorker>{

    public event Action OnHit;
    private const string TREE_HIT_ANIMATION = "TreeHit";

    private void TreeHit(){
        animator.SetTrigger(TREE_HIT_ANIMATION);
    }

    public void HitFinishedAnimationEvent(){
        OnHit?.Invoke();
    }

    public event Action OnGiveResourceFinished;

    public void GiveResourceFinishedAnimationEvent(){
        OnGiveResourceFinished?.Invoke();
    }
    
    protected override void OnEnable(){
        base.OnEnable();
        worker.OnTreeHitStart += TreeHit;
    }
    protected override void OnDisable(){
        base.OnDisable();
        worker.OnTreeHitStart -= TreeHit;
    }
}
