using UnityEngine;

[CreateAssetMenu(fileName = "Lumbermill Strategy", menuName = "Building Strategies/Lumbermill Strategy")]
public class LumbermillStrategy : BuildStrategySO{
    public override bool CanPlace(Tile[] tiles){
        if(tiles.Length != 9){
            return false;
        }
        foreach(Tile t in tiles){
            if(t is not TileGrass || !t.IsFree){
                return false;
            }
        }
        return true;
    }
}
