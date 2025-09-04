using UnityEngine;

public class TileGrass : Tile{

    private TreeObj tree;
    public TreeObj CurrentTree{get => tree; set{
        value.transform.parent = PlacementPoint;
        value.transform.localPosition = Vector3.zero;
        value.transform.rotation = Quaternion.Euler(0f, Random.Range(0f, 360f), 0f);
        tree = value;
    }}

    public bool HasTree => CurrentTree != null;
    public override bool IsFree => base.IsFree && !HasTree;

}
