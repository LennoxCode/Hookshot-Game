using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerEnemyErrorWin95 : MonoBehaviour
{
    //public Vector2 ObjectSpawnPosition;
    //public Vector2 ObjectSpawnPositionOrig;
    public GameObject objectToSpawn;
    public float timeToSpawn;
    private float currentTimeToSpawn;
    private int positionIndex;
    private Boolean startSpawn;

    // Start is called before the first frame update
    void Start()
    {
        //currentTimeToSpawn = timeToSpawn;
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        startSpawn = true;
        Debug.Log("trigger start Spawn");
    }
    // Update is called once per frame
    void Update()
    {
        if (startSpawn)
        {
            SpawnObject();
        }
        /*
        if (startSpawn && currentTimeToSpawn > 0)
        {
            SpawnObject();
            Debug.Log("Test1");

            //currentTimeToSpawn -= Time.deltaTime;
        }
        else
        {
            SpawnObject();
            Debug.Log("Test2");
            currentTimeToSpawn = timeToSpawn;
        }
        */
    }

    public void SpawnObject()
    {
        var ObjectSpawnPosition = new Vector2(transform.position.x , transform.position.y);
        //var ObjectSpawnPosition = 0;
        //var ObjectSpawnPositionOrig = ObjectSpawnPosition;
        if (positionIndex == 0)
        {
            Instantiate(objectToSpawn, ObjectSpawnPosition, Quaternion.identity);
            FindObjectOfType<AudioManager>().Play("EnemyErrorWin95");
            positionIndex++;
        }
        else if (positionIndex > 0 || positionIndex <= 25)
        {
            ObjectSpawnPosition.x += positionIndex;
            ObjectSpawnPosition.y += positionIndex;
            Instantiate(objectToSpawn, ObjectSpawnPosition, Quaternion.identity);
            positionIndex++;
            Debug.Log("Spawn Error"+ ObjectSpawnPosition + positionIndex );
        }

        else if (positionIndex > 25)
        {
            positionIndex = 0;
            ObjectSpawnPosition.x += positionIndex +15;
            //ObjectSpawnPosition = ObjectSpawnPositionOrig;
            Instantiate(objectToSpawn, ObjectSpawnPosition, Quaternion.identity);

        }
        
        //FindObjectOfType<AudioManager>().Play("CheckpointStartWin95_2");
        //.Log("EnemyWin95");
    }
}
