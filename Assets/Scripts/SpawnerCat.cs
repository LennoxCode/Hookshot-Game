using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnerCat : MonoBehaviour
{
    public GameObject cat;
    public Transform spawnPos;
    int catAmount = 0; 

    private int catIndex;
    private Vector2 bounds;
    public float respawnTime = 0.5f;
    private bool spawn = false;
    public int catWaves;

    public GameObject camera;

    // Start is called before the first frame update
    void Update()
    {
        //camera.GetComponent<Camera>();
        //bounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
            
        StartCoroutine(startCatSpawn());
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        //if (collider.gameObject.tag == "LoveCats")
        //{
            spawn = true;
            Debug.Log("trigger start Cats"+ catWaves);
        //}
    }
    private void spawnCats()
    {
        GameObject enemy = Instantiate(cat) as GameObject;
        // spwan enemy left screen bounds in random y position
        //enemy.transform.position = new Vector2(bounds.x * -2, Random.Range(-bounds.y, bounds.y));
        enemy.transform.position = spawnPos.position;
        
        catAmount++;
        //Debug.Log(catAmount);
        if (catAmount == 10)
        {
            spawn = false;
            Debug.Log("stop Spawn");
            catWaves--;
        }

        else if (catAmount == 1)
        {
            FindObjectOfType<AudioManager>().Play("Cat");

        }
    }

    IEnumerator startCatSpawn()
    {
        while (spawn && catWaves != 0)
        {
            yield return new WaitForSeconds(respawnTime);
            spawnCats();
        }
    }
}