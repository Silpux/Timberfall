public class MinePanel : WorkerBuildingPanel<MineBuilding>{
    public override void Close(){
        PanelManager.Instance.CloseMinePanel();
    }
}
