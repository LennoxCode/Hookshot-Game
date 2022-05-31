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
    [SerializeField] [Range(0.3f, 2f)]private float currentTimeToSpawn;
    private Boolean startSpawn;

    private Collider2D boxColl;
    // Start is called before the first frame update
    void Start()
    {
        //currentTimeToSpawn = timeToSpawn;
        boxColl = GetComponent<BoxCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        //startSpawn = true;
        Debug.Log("trigger start Spawn");
        boxColl.enabled = false;
        StartCoroutine(SpawnWindows());
        
    }
    // Update is called once per frame
    

    public void SpawnObject(int positionIndex)
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
           // Debug.Log("Spawn Error"+ ObjectSpawnPosition + positionIndex );
        }

        else if (positionIndex > 25)
        {
           
            ObjectSpawnPosition.x += positionIndex - 25;
            ObjectSpawnPosition.y += positionIndex;
            //ObjectSpawnPosition = ObjectSpawnPositionOrig;
            Instantiate(objectToSpawn, ObjectSpawnPosition, Quaternion.identity);

        }
        
        //FindObjectOfType<AudioManager>().Play("CheckpointStartWin95_2");
        //.Log("EnemyWin95");
    }

    IEnumerator SpawnWindows()
    {
        for (int i = 0; i < 25; i++)
        {
            SpawnObject(i);
            yield return new WaitForSeconds(currentTimeToSpawn);
        }
        
        
    }
}
