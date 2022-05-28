using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
