using UnityEngine;

public abstract class Tile : MonoBehaviour{

    [SerializeField] private Material highlightMaterial;
    private Material defaultMaterial;
    [SerializeField] private MeshRenderer meshRenderer;

    private void Awake(){
        defaultMaterial = meshRenderer.materials[0];
    }

    public void Highlight(){
        Material[] mats = meshRenderer.materials;
        mats[0] = highlightMaterial;
        meshRenderer.materials = mats;
    }

    public void Lowlight(){
        Debug.Log("Lowlight");
        Material[] mats = meshRenderer.materials;
        mats[0] = defaultMaterial;
        meshRenderer.materials = mats;
    }

}
