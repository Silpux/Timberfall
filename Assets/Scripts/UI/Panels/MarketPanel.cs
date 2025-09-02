using UnityEngine;

public class MarketPanel : ExchangeBuildingPanel<MarketBuilding>{
    public override void Close(){
        PanelManager.Instance.CloseMarketPanel();
    }
}
