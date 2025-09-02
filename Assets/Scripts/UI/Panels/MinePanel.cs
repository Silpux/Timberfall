public class MinePanel : WorkerBuildingPanel<MineBuilding>{
    public override void Close(){
        PanelManager.Instance.CloseMinePanel();
    }

    protected override void UpdateBuyWorkerButtons(){
        buyWoodenWorkerButtonBackground.sprite = Inventory.Instance.CanBuyMineWorker(WorkerGrade.Wood) ? backgroundOkSprite : backgroundNoSprite;
        buyStoneWorkerButtonBackground.sprite = Inventory.Instance.CanBuyMineWorker(WorkerGrade.Stone) ? backgroundOkSprite : backgroundNoSprite;
        buyIronWorkerButtonBackground.sprite = Inventory.Instance.CanBuyMineWorker(WorkerGrade.Iron) ? backgroundOkSprite : backgroundNoSprite;
        buyDiamondWorkerButtonBackground.sprite = Inventory.Instance.CanBuyMineWorker(WorkerGrade.Diamond) ? backgroundOkSprite : backgroundNoSprite;
    }
}
