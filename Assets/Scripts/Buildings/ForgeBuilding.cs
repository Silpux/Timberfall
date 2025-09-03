using UnityEngine;

public class ForgeBuilding : ExchangeBuilding{
    public override void OnClick(){
        PanelManager.Instance.OpenForgePanel(this);
    }
    public override void Remove(){
        base.Remove();
        Destroy(gameObject);
    }
}
