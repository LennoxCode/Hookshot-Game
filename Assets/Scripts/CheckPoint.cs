using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    // when player touches this Game Object
    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject other = collision.gameObject;

        if(other.CompareTag("Player"))
        {
            // set checkpoint position to this position
            other.GetComponent<CheckPointManager>().SetSpawnPoint(transform.position);

            // play sound
            Debug.Log("Checkpoint");
            FindObjectOfType<AudioManager>().Play("CheckpointStartWin95");
            // disable this checkpoint
            gameObject.SetActive(false);
        }
    }
}
