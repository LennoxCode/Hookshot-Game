using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerEnemyErrorWin95 : MonoBehaviour
{
    public Vector2 ObjectSpawnPosition;
    public Vector2 ObjectSpawnPositionOrig;
    public GameObject objectToSpawn;
    public float timeToSpawn;
    private float currentTimeToSpawn;
    private int positionIndex;

    // Start is called before the first frame update
    void Start()
    {
        currentTimeToSpawn = timeToSpawn;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentTimeToSpawn > 0)
        {
            currentTimeToSpawn -= Time.deltaTime;
        }
        else
        {
            SpawnObject();
            currentTimeToSpawn = timeToSpawn;
        }
        
    }

    public void SpawnObject()
    {
        if (positionIndex == 0)
        {
            Instantiate(objectToSpawn, ObjectSpawnPosition, Quaternion.identity);
            FindObjectOfType<AudioManager>().Play("EnemyErrorWin95");
            positionIndex++;
        }
        else if (positionIndex > 0 || positionIndex <= 5)
        {
            ObjectSpawnPosition.x += 1;
            ObjectSpawnPosition.y += -1;
            Instantiate(objectToSpawn, ObjectSpawnPosition, Quaternion.identity);
            Debug.Log("Spawn Error");
        }

        else if (positionIndex > 5)
        {
            positionIndex = 0;
            ObjectSpawnPosition = ObjectSpawnPositionOrig;
            Instantiate(objectToSpawn, ObjectSpawnPosition, Quaternion.identity);

        }
        
        //FindObjectOfType<AudioManager>().Play("CheckpointStartWin95_2");
        //.Log("EnemyWin95");
    }
}
