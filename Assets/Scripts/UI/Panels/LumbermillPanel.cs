public class LumbermillPanel : WorkerBuildingPanel<LumbermillBuilding>{
    public override void Close(){
        PanelManager.Instance.CloseLumbermillPanel();
    }

    protected override void UpdateBuyWorkerButtons(){
        buyWoodenWorkerButtonBackground.sprite = Inventory.Instance.CanBuyLumbermillWorker(WorkerGrade.Wood) ? backgroundOkSprite : backgroundNoSprite;
        buyStoneWorkerButtonBackground.sprite = Inventory.Instance.CanBuyLumbermillWorker(WorkerGrade.Stone) ? backgroundOkSprite : backgroundNoSprite;
        buyIronWorkerButtonBackground.sprite = Inventory.Instance.CanBuyLumbermillWorker(WorkerGrade.Iron) ? backgroundOkSprite : backgroundNoSprite;
        buyDiamondWorkerButtonBackground.sprite = Inventory.Instance.CanBuyLumbermillWorker(WorkerGrade.Diamond) ? backgroundOkSprite : backgroundNoSprite;
    }
}
