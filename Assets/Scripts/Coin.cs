using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    
    [SerializeField] private int value;
    private void OnTriggerEnter2D(Collider2D col)
    {
        ScoreController.instance.IncrementScore(value);
        Destroy(gameObject);
    }


}
