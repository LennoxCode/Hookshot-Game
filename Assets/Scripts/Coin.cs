using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// this a very simple script. if the player collects a coin by entering the trigger
/// the object is destroyed and the score is incremented
/// </summary>
public class Coin : MonoBehaviour
{
    
    [SerializeField] private int value;
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (!col.CompareTag("Player")) return;
        ScoreController.instance.IncrementScore(value);
        Destroy(gameObject);
    }


}
