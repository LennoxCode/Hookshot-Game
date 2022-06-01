using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

/// <summary>
/// this script is responsible for spawning the coin object randomly in a given area
/// I opted to use this random spawner because it would make the creation of levels a lot easier by not
/// having to manually place coins everywhere and in addition it could spice up playthroughs in the future
/// if the seed is removed
/// this works using the scale and positon of the GameObject this script is attached to form the Range of random
/// generation. Afterwards the prefabs just have to be instantiated.
/// Additionally all the parameters can be changed in editor to make it customizable for each area
/// </summary>
public class CoinSpawnerArea : MonoBehaviour
{
    [SerializeField] private GameObject coinPrefab;
    [SerializeField] [Range(5, 60)] private int maxCoins;
    [SerializeField] private bool useSeed;
    [SerializeField] private int coinSeed;
    
    void Start()
    {
        if(useSeed)Random.InitState(coinSeed);
        else Random.InitState(DateTime.Now.Millisecond );
        Transform t = transform;
        for (int i = 0; i < maxCoins; i++)
        {
            Vector2 pos = t.position;
            Vector2 scale = t.localScale / 2;
            float x = Random.Range(pos.x - scale.x, pos.x + scale.x);
            float y = Random.Range(pos.y - scale.y, pos.y + scale.y);
            Instantiate(coinPrefab, new Vector3(x, y, 0), Quaternion.identity);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawCube(transform.position, transform.localScale);
        
    }
}
