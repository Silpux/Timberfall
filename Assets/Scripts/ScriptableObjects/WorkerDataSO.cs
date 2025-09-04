using UnityEngine;

public abstract class WorkerDataSO : ScriptableObject{
    [SerializeField] private ItemDataSO itemData;
    public ItemDataSO ItemData => itemData;
}
