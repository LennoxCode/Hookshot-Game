using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerRandom : MonoBehaviour
{
    public int numberToSpawn;
    public List<GameObject> spawnPool;

    public GameObject GameObject;
    // Start is called before the first frame update
    void Start()
    {
        spawnRandomObject();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void spawnRandomObject()
    {
        int randomItem = 0;
        GameObject toSpawn;
        MeshCollider c = GameObject.GetComponent<MeshCollider>();

        float screenX, ScreenY;
        Vector2 pos;
        
        for (int i = 0; i < numberToSpawn; i++)
        {
            randomItem = Random.Range(0, spawnPool.Count);
            toSpawn = spawnPool[randomItem];
            
            screenX = Random.Range(c.bounds.min.x, c.bounds.max.x);
            ScreenY = Random.Range(c.bounds.min.y, c.bounds.max.y);
            pos = new Vector2(screenX, ScreenY);

            Instantiate(toSpawn, pos, toSpawn.transform.rotation);
            Debug.Log("Spawn Random");
        }
    }
}
