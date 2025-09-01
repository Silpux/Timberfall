public abstract class BuildingPanel<B> : Panel where B : Building{
    protected B building;
    public abstract void SetBuilding(B building);
    public abstract void RemoveBuilding();
}
