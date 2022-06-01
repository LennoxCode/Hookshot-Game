using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// this script is attached to the goal with a trigger collider attached. if the player enters this collider
/// it is deactivated and an event fires which will lead to other classes finishing the game
/// </summary>
public class Goal : MonoBehaviour
{
    public static Action playerEnteredGoal;
    private bool playerHasWon = false;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (playerHasWon || !col.gameObject.CompareTag("Player")) return;
        playerEnteredGoal?.Invoke();
        AudioManager.instance.Play("Win");
        playerHasWon = true;
    }
}
