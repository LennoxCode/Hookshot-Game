using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointManager : MonoBehaviour
{
    // delegates
    public static Action playerDeath;
    public static Action playerRespawn;

    // references
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private Rigidbody2D rb;

    // global variables
    private Coroutine currentCoroutine;


    private void Awake()
    {
        playerDeath = null;
        playerRespawn = null;
    }
    /// <summary>
    /// sets position of spawnpoint game object to given position
    /// </summary>
    public void SetSpawnPoint(Vector3 newPosition)
    {
        spawnPoint.position = newPosition;
    }

    /// <summary>
    /// invokes death and starts respawn coroutine
    /// </summary>
    public void KillPlayer(float delay = 0f)
    {
        if (currentCoroutine != null) return;

        playerDeath?.Invoke();
        currentCoroutine = StartCoroutine(RespawnRoutine(delay));
    }

    /// <summary>
    /// waits for given seconds, sets player to respawn point and invokes respawn
    /// </summary>
    private IEnumerator RespawnRoutine(float delay)
    {
        yield return new WaitForSeconds(delay);

        rb.velocity = Vector3.zero;
        transform.position = spawnPoint.position;
        playerRespawn?.Invoke();

        currentCoroutine = null;
    }
}
