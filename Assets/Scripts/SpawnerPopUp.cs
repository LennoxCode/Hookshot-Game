using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerPopUp : MonoBehaviour
{
    public int numberToSpawn;

    [SerializeField] private GameObject[] PopUpPrefab;

    public GameObject screen;
    
    // Start is called before the first frame update
    void Start()
    {
        spawnPopUp();
    }

    public void spawnPopUp()
    {
        GameObject toSpawn;
        MeshCollider collider = screen.GetComponent<MeshCollider>();

        float screenX, screenY;
        Vector2 pos;

        for (int i = 0; i < numberToSpawn; i++)
        {
            //int randomPopUp = Random.Range(0, PopUpPrefab.Length);
            toSpawn = PopUpPrefab[Random.Range(0, PopUpPrefab.Length)];
            screenX = Random.Range(collider.bounds.min.x, collider.bounds.max.x);
            screenY = Random.Range(collider.bounds.min.y, collider.bounds.max.y);
            pos = new Vector2(screenX, screenY);

            Instantiate(toSpawn, pos, toSpawn.transform.rotation);
        }
    }

    // Update is called once per frame
    void Update()
    {
        spawnPopUp();
    }
}
