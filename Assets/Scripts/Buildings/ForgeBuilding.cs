using UnityEngine;

public class ForgeBuilding : ExchangeBuilding{
    public override void OnClick(){
        Debug.Log("Open blacksmith");
    }
    public override void Remove(){
        base.Remove();
        Destroy(gameObject);
    }
}
