using UnityEngine;

public class ForgeBuilding : ExchangeBuilding, ITargetable{
    [SerializeField] private Transform workerTargetPoint;
    public Transform TargetPoint => workerTargetPoint;
    public override void OnClick(){
        PanelManager.Instance.OpenForgePanel(this);
    }
    public override void Remove(){
        base.Remove();
        Destroy(gameObject);
    }

    public void AcceptResource(MineWorkerDataSO resource){
        Inventory.Instance.AddItem(resource.ItemData, resource.Amount);
    }
}
