using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointManager : MonoBehaviour
{
    public static Action playerDeath;
    public static Action playerRespawn;

    [SerializeField] private Transform spawnPoint;
    [SerializeField] private Rigidbody2D rb;

    private Coroutine currentCoroutine;

    public void SetSpawnPoint(Vector3 newPosition)
    {
        spawnPoint.position = newPosition;
    }

    public void KillPlayer(float delay = 0f)
    {
        if (currentCoroutine != null) return;

        playerDeath?.Invoke();
        currentCoroutine = StartCoroutine(RespawnRoutine(delay));
    }

    private IEnumerator RespawnRoutine(float delay)
    {
        yield return new WaitForSeconds(delay);

        rb.velocity = Vector3.zero;
        transform.position = spawnPoint.position;
        playerRespawn?.Invoke();

        currentCoroutine = null;
    }
}
