using System;
using UnityEngine;
using UnityEngine.UI;

public class LumbermillWorkerEntry : MonoBehaviour{

    [SerializeField] private Sprite woodenAxeSprite;
    [SerializeField] private Sprite stoneAxeSprite;
    [SerializeField] private Sprite ironAxeSprite;
    [SerializeField] private Sprite diamondAxeSprite;

    [SerializeField] private Image axeImage;
    
    private LumbermillWorker worker;

    public event Action OnRemove;

    public void OnClose(){
        worker.Remove();
        OnRemove?.Invoke();
    }

    public void SetWorker(LumbermillWorker worker){
        this.worker = worker;
        axeImage.sprite = worker.Grade switch{
            WorkerGrade.Wood => woodenAxeSprite,
            WorkerGrade.Stone => stoneAxeSprite,
            WorkerGrade.Iron => ironAxeSprite,
            WorkerGrade.Diamond => diamondAxeSprite,
            _ => woodenAxeSprite,
        };
    }
}
