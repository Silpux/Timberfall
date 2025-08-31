using System.Collections;
using UnityEngine;

public class TreeSpawner : MonoBehaviour{

    [SerializeField] private TreeObj[] treePrefabs;
    [SerializeField] private float spawnInterval = 60f;

    [SerializeField] private TileManager tileManager;

    private void Start(){
        tileManager = FindObjectOfType<TileManager>();
        if(tileManager == null){
            Debug.LogError("TileManager not found in scene!");
            return;
        }

        StartCoroutine(SpawnTreeRoutine());
    }

    private IEnumerator SpawnTreeRoutine(){
        while (true){

            TileGrass tile = tileManager.GetTileForTree();
            if(tile != null){
                TreeObj treePrefab = treePrefabs[Random.Range(0, treePrefabs.Length)];

                Vector3 spawnPos = tile.transform.position;
                TreeObj newTree = Instantiate(treePrefab, spawnPos, Quaternion.identity);

                tile.CurrentTree = newTree;
            }
            
            yield return new WaitForSeconds(spawnInterval);
        }
    }

}
