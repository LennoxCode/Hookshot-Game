using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Checkpoint");
        FindObjectOfType<AudioManager>().Play("CheckpointStartWin95");
    }
}
