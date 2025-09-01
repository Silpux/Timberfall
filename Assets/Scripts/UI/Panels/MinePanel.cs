using UnityEngine;

public class MinePanel : BuildingPanel<MineBuilding>{
    public override void Close(){
        PanelManager.Instance.CloseMinePanel();
    }
}
