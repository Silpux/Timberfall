using UnityEngine;

public abstract class Tile : MonoBehaviour{

    [SerializeField] private Material highlightOkMaterial;
    [SerializeField] private Material highlightNoMaterial;
    private Material defaultMaterial;
    [SerializeField] private MeshRenderer meshRenderer;
    [SerializeField] private Transform placementPoint;

    public Vector2 Position{get; set;}

    public Transform PlacementPoint => placementPoint;

    public bool HasBuilding {get; set;}

    public virtual bool IsFree => !HasBuilding;

    private void Awake(){
        defaultMaterial = meshRenderer.materials[0];
    }

    public void Highlight(bool ok){
        SetMaterial(ok ? highlightOkMaterial : highlightNoMaterial);
    }

    public void Lowlight(){
        SetMaterial(defaultMaterial);
    }

    private void SetMaterial(Material mat){
        Material[] mats = meshRenderer.materials;
        mats[0] = mat;
        meshRenderer.materials = mats;
    }

}
