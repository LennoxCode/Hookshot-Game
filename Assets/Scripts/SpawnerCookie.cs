using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerCookie : MonoBehaviour
{
    [SerializeField] GameObject[] cookiePrefab;

    //[SerializeField] float posMin;
   // [SerializeField] float posMax;
    [SerializeField] float timeToSpawn = 1.0f;


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnCookie());
    }
    IEnumerator SpawnCookie()

    {
        while (true)
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

