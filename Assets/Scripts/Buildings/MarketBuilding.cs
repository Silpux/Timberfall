using UnityEngine;

public class MarketBuilding : ExchangeBuilding{
    public override void OnClick(){
        Debug.Log("Open market");
    }
    public override void Remove(){
        base.Remove();
        Destroy(gameObject);
    }
}
