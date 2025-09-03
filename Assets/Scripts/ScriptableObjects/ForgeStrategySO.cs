using UnityEngine;

[CreateAssetMenu(fileName = "Forge Strategy", menuName = "Building Strategies/Forge Strategy")]
public class ForgeStrategy : BuildStrategySO{
    public override bool CanPlace(Tile[] tiles){
        if(tiles.Length != 9){
            return false;
        }
        foreach(Tile t in tiles){
            if(t == null || t is TileStone || !t.IsFree){
                return false;
            }
        }
        return Inventory.Instance.GetItemAmount(Resource) >= Amount;
    }
}
