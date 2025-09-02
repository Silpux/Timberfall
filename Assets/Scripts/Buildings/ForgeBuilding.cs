using UnityEngine;

public class ForgeBuilding : ExchangeBuilding{
    public override void OnClick(){
        Debug.Log("Open forge");
        PanelManager.Instance.OpenForgePanel(this);
    }
    public override void Remove(){
        base.Remove();
        Destroy(gameObject);
    }
}
