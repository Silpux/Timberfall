using UnityEngine;

public class ForgePanel : ExchangeBuildingPanel<ForgeBuilding>{
    public override void Close(){
        PanelManager.Instance.CloseForgePanel();
    }
}
