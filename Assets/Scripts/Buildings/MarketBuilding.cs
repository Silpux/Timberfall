using UnityEngine;

public class MarketBuilding : ExchangeBuilding{
    public override void OnClick(){
        PanelManager.Instance.OpenMarketPanel(this);
    }
    public override void Remove(){
        base.Remove();
        Destroy(gameObject);
    }
}
