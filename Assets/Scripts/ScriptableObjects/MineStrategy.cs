using UnityEngine;

[CreateAssetMenu(fileName = "Mine Strategy", menuName = "Building Strategies/Mine Strategy")]
public class MineStrategy : BuildStrategySO{
    public override bool CanPlace(Tile[] tiles){
        if(tiles.Length != 9){
            return false;
        }
        foreach(Tile t in tiles){
            if(t is not TileStone){
                return false;
            }
        }
        return true;
    }
}
