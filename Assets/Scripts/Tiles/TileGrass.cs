using UnityEngine;

public class TileGrass : Tile{

    private TreeObj tree;
    public TreeObj CurrentTree{get => tree; set{
        value.transform.parent = PlacementPoint;
        value.transform.localPosition = Vector3.zero;
        tree = value;
    }}

    public bool HasTree => CurrentTree != null;
    public override bool IsFree => base.IsFree && !HasTree;

}
