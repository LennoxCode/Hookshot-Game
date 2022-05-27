using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    public static Action playerEnteredGoal;
    private bool playerHasWon = false;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (playerHasWon) return;
        playerEnteredGoal?.Invoke();
        playerHasWon = true;
    }
}
