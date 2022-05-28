using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class spawnCat : MonoBehaviour
{
    public GameObject cat;
    int catAmount = 0; 

    private int catIndex;
    private Vector2 bounds;
    public float respawnTime = 0.5f;
    private bool spawn = true;

    public GameObject camera;

    // Start is called before the first frame update
    void Start()
    {
        //camera.GetComponent<Camera>();
        bounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height,
            Camera.main.transform.position.z));
        StartCoroutine(startCatSpawn());
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "LoveCats")
        {
            spawn = true;
            Debug.Log("trigger start Cats");
        }
    }
    private void spawnCats()
    {
        GameObject enemy = Instantiate(cat) as GameObject;
        // spwan enemy left screen bounds in random y position
        enemy.transform.position = new Vector2(bounds.x * -2, Random.Range(-bounds.y, bounds.y));
        
        catAmount++;
        Debug.Log(catAmount);
        if (catAmount == 15)
        {
            spawn = false;
            Debug.Log("stop Spawn");
        }

        else if (catAmount == 1)
        {
            FindObjectOfType<AudioManager>().Play("Cat");

        }
    }

    IEnumerator startCatSpawn()
    {
        while (spawn)
        {
            yield return new WaitForSeconds(respawnTime);
            spawnCats();
        }
    }
}