public class LumbermillPanel : WorkerBuildingPanel<LumbermillBuilding>{
    public override void Close(){
        PanelManager.Instance.CloseLumbermillPanel();
    }
}
