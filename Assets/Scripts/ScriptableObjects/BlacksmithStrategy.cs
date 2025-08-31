using UnityEngine;

[CreateAssetMenu(fileName = "Blacksmith Strategy", menuName = "Building Strategies/Blacksmith Strategy")]
public class BlacksmithStrategy : BuildStrategySO{
    public override bool CanPlace(Tile[] tiles){
        if(tiles.Length != 9){
            return false;
        }
        foreach(Tile t in tiles){
            if(t == null || t is TileStone || !t.IsFree){
                return false;
            }
        }
        return true;
    }
}
