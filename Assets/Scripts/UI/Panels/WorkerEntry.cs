using System;
using UnityEngine;
using UnityEngine.UI;

public class WorkerEntry : MonoBehaviour{

    [SerializeField] private Sprite woodenToolSprite;
    [SerializeField] private Sprite stoneToolSprite;
    [SerializeField] private Sprite ironToolSprite;
    [SerializeField] private Sprite diamondToolSprite;

    [SerializeField] private Image toolImage;

    private WorkerData worker;

    public event Action OnRemove;

    public void Close(){
        worker.Remove();
        OnRemove?.Invoke();
    }

    public void SetWorker(WorkerData worker){
        this.worker = worker;
        toolImage.sprite = worker.Grade switch{
            WorkerGrade.Wood => woodenToolSprite,
            WorkerGrade.Stone => stoneToolSprite,
            WorkerGrade.Iron => ironToolSprite,
            WorkerGrade.Diamond => diamondToolSprite,
            _ => woodenToolSprite,
        };
    }
}
