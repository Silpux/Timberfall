using System;
using UnityEngine;

public class MineWorkerVisual : WorkerVisual<MineWorker>{

    public event Action OnGiveResourceFinished;

    public void GiveResourceFinishedAnimationEvent(){
        OnGiveResourceFinished?.Invoke();
    }

}
