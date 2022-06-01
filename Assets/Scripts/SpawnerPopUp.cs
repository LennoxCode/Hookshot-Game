using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerPopUp : MonoBehaviour
{
    public int numberToSpawn;

    [SerializeField] private GameObject[] PopUpPrefab;
    //public GameObject page;
    [SerializeField] private float startToSpawn;
    private Collider2D boxColl;
    
    // Start is called before the first frame update
    void Start()
    {
        boxColl = GetComponent<BoxCollider2D>();
    }

    public void OnTriggerEnter2D(Collider2D collider)
    {
        Debug.Log("trigger start Spawn Cookie");
        boxColl.enabled = false;
        //startToSpawn = true;
        StartCoroutine(spawnPopUp());
    }


    public void SpawnObject(int i)
    {
        GameObject toSpawn;
        toSpawn = PopUpPrefab[Random.Range(0, PopUpPrefab.Length)];
        Transform t = transform;
        var pos = new Vector2(transform.position.x, transform.position.y); 
        // Note Scale Divided by 2 is the amount of Pixel left and right
        float x = Random.Range(pos.x - 15, pos.x + 15);
        float y = Random.Range(pos.y - 5, pos.y +5);
        Instantiate(toSpawn, new Vector3(x, y, 0), Quaternion.identity); 
        //Instantiate(toSpawn, pos, toSpawn.transform.rotation);
    }
    IEnumerator spawnPopUp()
    {
        for (int i = 0; i < numberToSpawn; i++)
        {
            SpawnObject(i);
            yield return new WaitForSeconds(startToSpawn);
            Destroy(gameObject, startToSpawn);
        }
    }
    // Update is called once per frame
   
}
