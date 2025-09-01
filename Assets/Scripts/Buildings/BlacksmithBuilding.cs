using UnityEngine;

public class BlacksmithBuilding : Building{
    public override void OnClick(){
        Debug.Log("Open blacksmith");
    }
    public override void Remove(){
        base.Remove();
        Destroy(gameObject);
    }
}
