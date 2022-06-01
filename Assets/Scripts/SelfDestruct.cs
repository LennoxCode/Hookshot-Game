using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// counts up time and then destroys this game object
/// </summary>
public class SelfDestruct : MonoBehaviour
{
    [SerializeField] private float timeToDestruct;

    private float timer = 0f;

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer >= timeToDestruct) Destroy(gameObject);
    }
}
