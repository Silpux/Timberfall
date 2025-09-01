using UnityEngine;

public class MarketBuilding : Building{
    public override void OnClick(){
        Debug.Log("Open market");
    }
    public override void Remove(){
        base.Remove();
        Destroy(gameObject);
    }
}
