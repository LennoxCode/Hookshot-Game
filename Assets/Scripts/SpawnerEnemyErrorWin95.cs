using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerEnemyErrorWin95 : MonoBehaviour
{
    public GameObject objectToSpawn;
    public float timeToSpawn;
    [SerializeField] [Range(0.3f, 2f)]private float currentTimeToSpawn;
    private Collider2D boxColl;
    // Start is called before the first frame update
    void Start()
    {
        boxColl = GetComponent<BoxCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        Debug.Log("trigger start Spawn WIN ERROR");
        boxColl.enabled = false;
        StartCoroutine(SpawnWindows());
    }
    
    public void SpawnObject(int positionIndex)
    {
        var ObjectSpawnPosition = new Vector2(transform.position.x , transform.position.y);
        if (positionIndex == 0)
        {
            Instantiate(objectToSpawn, ObjectSpawnPosition, Quaternion.identity);
            FindObjectOfType<AudioManager>().Play("EnemyErrorWin95");
            positionIndex++;
        }
        if (positionIndex > 0 && positionIndex < 25)
        {
            ObjectSpawnPosition.x += positionIndex*2;
            ObjectSpawnPosition.y += positionIndex;
            Debug.Log(ObjectSpawnPosition);
            Instantiate(objectToSpawn, ObjectSpawnPosition, Quaternion.identity);
            positionIndex++;
           // Debug.Log("Spawn Error"+ ObjectSpawnPosition + positionIndex );
        }
        else if ( positionIndex >= 25)
        {
            Debug.Log("new row");
            Debug.Log(positionIndex);
            ObjectSpawnPosition.x += positionIndex*2 - 20.0f;
            ObjectSpawnPosition.y += positionIndex - 25.0f;
            Instantiate(objectToSpawn, ObjectSpawnPosition, Quaternion.identity);
            positionIndex++;
        }
    }

    IEnumerator SpawnWindows()
    {
        for (int i = 0; i < 45; i++)
        {
            SpawnObject(i);
            yield return new WaitForSeconds(currentTimeToSpawn);
        }
        
        
    }
}
