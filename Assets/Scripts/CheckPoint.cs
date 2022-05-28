using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject other = collision.gameObject;

        if(other.CompareTag("Player"))
        {
            other.GetComponent<CheckPointManager>().SetSpawnPoint(transform.position);

            Debug.Log("Checkpoint");
            FindObjectOfType<AudioManager>().Play("CheckpointStartWin95");
        }
    }
}
