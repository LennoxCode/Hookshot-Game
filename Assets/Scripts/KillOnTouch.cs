using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillOnTouch : MonoBehaviour
{
    [SerializeField] private float deathDelay;
  
    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject other = collision.gameObject;

        if (other.CompareTag("Player"))
        {
            other.GetComponent<CheckPointManager>().KillPlayer(deathDelay);
            AudioManager.instance.Play("KillOnTouch");

        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject other = collision.gameObject;

        if (other.CompareTag("Player"))
        {
            other.GetComponent<CheckPointManager>().KillPlayer(deathDelay);
            AudioManager.instance.Play("KillOnTouch");

        }
    }
}
