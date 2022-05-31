using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnerCat : MonoBehaviour
{
    public GameObject cat;
    public Transform spawnPos;
    private Vector2 bounds;
    [SerializeField][Range(1f,20f)] private float respawnTime = 2f;
    [SerializeField] private int catWaves;
    private bool clicked = false;
   
    
    private IEnumerator spawnCats()
    {
        // spwan enemy left screen bounds in random y position
        
        FindObjectOfType<AudioManager>().Play("Cat");
        for (int i = 0; i < 10; i++)
        {
            GameObject enemy = Instantiate(cat) as GameObject;
            enemy.transform.position = new Vector2( spawnPos.position.x, 
                spawnPos.position.y + Random.Range(-5, 5));
            yield return new WaitForSeconds(0.5f);
        }

    }

    IEnumerator startCatSpawn()
    {
        for (int i = 0; i < catWaves; i++)
        {
            Debug.Log("spawn cats");
            yield return new WaitForSeconds(respawnTime);
            yield return spawnCats();

        }
    }

    private void OnMouseDown()
    {
        if (clicked) return;
        clicked = true;
        StartCoroutine(startCatSpawn());
    }
}