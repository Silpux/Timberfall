using UnityEngine;

[CreateAssetMenu(fileName = "Mine Strategy", menuName = "Building Strategies/Mine Strategy")]
public class MineStrategy : BuildStrategySO{
    public override bool CanPlace(Tile[] tiles){
        if(tiles.Length != 9){
            return false;
        }
        foreach(Tile t in tiles){
            if(t == null || t is not TileStone || !t.IsFree){
                return false;
            }
        }
        return Inventory.Instance.GetItemAmount(Resource) >= Amount;
    }
}
