using UnityEngine;

[CreateAssetMenu(fileName = "Blacksmith Strategy", menuName = "Building Strategies/Blacksmith Strategy")]
public class BlacksmithStrategy : BuildStrategySO{
    public override bool CanPlace(Tile[] tiles){
        if(tiles.Length != 9){
            return false;
        }
        foreach(Tile t in tiles){
            if(t is TileStone){
                return false;
            }
            if(t is TileGrass g){
                if(g.HasTree){
                    return false;
                }
            }
        }
        return true;
    }
}
