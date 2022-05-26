using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private int value;
    private void OnCollisionEnter(Collision collision)
    {
        ScoreController.instance.IncrementScore(value);
        Destroy(gameObject);
    }
}
