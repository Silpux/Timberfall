using System.Collections;
using UnityEngine;

public class TreeSpawner : MonoBehaviour{

    [SerializeField] private TreeObj[] treePrefabs;
    [SerializeField] private float spawnInterval = 60f;

    [SerializeField] private TileManager tileManager;

    private void Start(){
        StartCoroutine(SpawnTreeRoutine());
    }

    private IEnumerator SpawnTreeRoutine(){
        while (true){

            TileGrass tile = tileManager.GetTileForTree();
            if(tile != null && tile.IsFree){
                TreeObj treePrefab = treePrefabs[Random.Range(0, treePrefabs.Length)];

                TreeObj newTree = Instantiate(treePrefab);
                tile.CurrentTree = newTree;
            }
            
            yield return new WaitForSeconds(spawnInterval);
        }
    }

}
