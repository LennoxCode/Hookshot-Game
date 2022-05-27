using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreBoardController : MonoBehaviour
{
    [SerializeField] private GameObject scoreEntryPrefab;
    private GameObject[] scoreEnryGUIElements;
   
    private void Start()
    {
        scoreEnryGUIElements = new GameObject[10];
    }

    public void DisplayScoreBoard()
    {
        List<ScoreController.ScoreBoardEntry> scoreBoard = ScoreController.instance.GetScoreBoard();
        foreach (ScoreController.ScoreBoardEntry sbe in scoreBoard)
        {
            GameObject newGUIEntry = Instantiate(scoreEntryPrefab);
            newGUIEntry.GetComponent<ScoreBoardEntryDisplay>().SetDisplay(sbe);
        }
    }
}
