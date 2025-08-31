using UnityEngine;


[CreateAssetMenu(fileName = "Market Strategy", menuName = "Building Strategies/Market Strategy")]
public class MarketStrategy : BuildStrategySO{
    public override bool CanPlace(Tile[] tiles){
        if(tiles.Length != 9){
            return false;
        }
        foreach(Tile t in tiles){
            if(t is not TileGrass and not TileDirt){
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
