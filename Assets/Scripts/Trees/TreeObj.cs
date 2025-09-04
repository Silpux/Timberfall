using UnityEngine;

public class TreeObj : MonoBehaviour{
    [SerializeField] private int woodAmount;
    public bool IsAvailable => !isTargeted && !isDead;
    private bool isTargeted;
    public bool IsTargeted{
        set => isTargeted = value;
    }
    private bool isDead = false;
    [field: SerializeField] public float HP{get; private set;}
    public void TakeDamage(LumbermillWorker worker, float amount){
        HP -= amount;
        if(HP <= 0){
            worker.AcceptWood(woodAmount);
            isDead = true;
            Destroy(gameObject);
        }
    }

}
