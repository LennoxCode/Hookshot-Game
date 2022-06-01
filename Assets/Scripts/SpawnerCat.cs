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
    private bool clicked;
    private void OnMouseDown()
    {
        if (clicked) return;
        clicked = true;
        StartCoroutine(startCatSpawn());
    }
    IEnumerator startCatSpawn()
    {
        for (int i = 0; i < catWaves; i++)
        {
            //Debug.Log("spawn CatWave");
            // Wait for individual Respawn Time
            yield return new WaitForSeconds(respawnTime);
            // Wait until the spawnCats is finished
            yield return spawnCats();
        }
    }
    private IEnumerator spawnCats()
    {
        AudioManager.instance.Play("Cat");
        for (int i = 0; i < 10; i++)
        {
            GameObject enemy = Instantiate(cat) as GameObject;
            // spwan enemy left screen bounds in random y position
            enemy.transform.position = new Vector2( spawnPos.position.x, 
                spawnPos.position.y + Random.Range(-5, 5));
            yield return new WaitForSeconds(0.5f);
        }
    }
}