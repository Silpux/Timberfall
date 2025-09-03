public class LumbermillPanel : WorkerBuildingPanel<LumbermillBuilding>{
    public override void Close(){
        PanelManager.Instance.CloseLumbermillPanel();
    }

    protected override void UpdateBuyWorkerButtons(){
        buyWoodenWorkerButtonBackground.sprite = Inventory.Instance.CanBuyLumbermillWorker(WorkerGrade.Wood, building.GetCostForNextWorker()) ? backgroundOkSprite : backgroundNoSprite;
        buyStoneWorkerButtonBackground.sprite = Inventory.Instance.CanBuyLumbermillWorker(WorkerGrade.Stone, building.GetCostForNextWorker()) ? backgroundOkSprite : backgroundNoSprite;
        buyIronWorkerButtonBackground.sprite = Inventory.Instance.CanBuyLumbermillWorker(WorkerGrade.Iron, building.GetCostForNextWorker()) ? backgroundOkSprite : backgroundNoSprite;
        buyDiamondWorkerButtonBackground.sprite = Inventory.Instance.CanBuyLumbermillWorker(WorkerGrade.Diamond, building.GetCostForNextWorker()) ? backgroundOkSprite : backgroundNoSprite;
    }
}
