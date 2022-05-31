using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnerCookie : MonoBehaviour
{
    [SerializeField] GameObject[] cookiePrefab;
    [SerializeField] float timeToSpawn = 1.0f;
    private Boolean startToSpawn = false;


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnCookie());
    }

    public void OnTriggerEnter2D(Collider2D collider)
    {
        startToSpawn = true;
        //StartCoroutine(SpawnCookie());
        Debug.Log("True");

    }

    public void OnTriggerExit2D(Collider2D collider)
    {
        startToSpawn = false;
        Debug.Log("False");
    }

    IEnumerator SpawnCookie()
    {
        while (startToSpawn)
        {
            var range = Random.Range(-5, +5);
            var position = new Vector2(transform.position.x + range, transform.position.y);
            GameObject gameObject = Instantiate(cookiePrefab[Random.Range(0, cookiePrefab.Length)], position,
                Quaternion.identity);
            yield return new WaitForSeconds(timeToSpawn);
            Destroy(gameObject, 10.0f);
        }
    }
}

