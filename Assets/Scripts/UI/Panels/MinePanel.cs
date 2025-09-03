public class MinePanel : WorkerBuildingPanel<MineBuilding>{
    public override void Close(){
        PanelManager.Instance.CloseMinePanel();
    }

    protected override void UpdateBuyWorkerButtons(){
        buyWoodenWorkerButtonBackground.sprite = Inventory.Instance.CanBuyMineWorker(WorkerGrade.Wood, building.GetCostForNextWorker()) ? backgroundOkSprite : backgroundNoSprite;
        buyStoneWorkerButtonBackground.sprite = Inventory.Instance.CanBuyMineWorker(WorkerGrade.Stone, building.GetCostForNextWorker()) ? backgroundOkSprite : backgroundNoSprite;
        buyIronWorkerButtonBackground.sprite = Inventory.Instance.CanBuyMineWorker(WorkerGrade.Iron, building.GetCostForNextWorker()) ? backgroundOkSprite : backgroundNoSprite;
        buyDiamondWorkerButtonBackground.sprite = Inventory.Instance.CanBuyMineWorker(WorkerGrade.Diamond, building.GetCostForNextWorker()) ? backgroundOkSprite : backgroundNoSprite;
    }
}
