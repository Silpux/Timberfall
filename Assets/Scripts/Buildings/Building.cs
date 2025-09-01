using System.Collections.Generic;
using UnityEngine;

public abstract class Building : MonoBehaviour{
    public List<Tile> OccupiedTiles{get;} = new(9);
    public abstract void OnClick();
    public virtual void Remove(){
        foreach(Tile t in OccupiedTiles){
            t.HasBuilding = false;
        }
    }
}
