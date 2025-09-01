public class LumbermillPanel : BuildingPanel<LumbermillBuilding>{
    public override void Close(){
        PanelManager.Instance.CloseLumbermillPanel();
    }
}
