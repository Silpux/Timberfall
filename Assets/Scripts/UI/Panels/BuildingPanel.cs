using UnityEngine;

public abstract class BuildingPanel<B> : MonoBehaviour where B : Building{
    protected B building;
    public abstract void SetBuilding(B building);
    public abstract void RefreshUI();
    public abstract void ResetUI();
    public abstract void Close();
    public virtual void RemoveBuilding() => Close();
}
