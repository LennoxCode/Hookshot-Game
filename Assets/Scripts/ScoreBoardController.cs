using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreBoardController : MonoBehaviour
{
    [SerializeField] private InputField nameInput;
    [SerializeField] private GameObject scoreEntryPrefab;
    private List<GameObject> scoreEnryGUIElements;
    private bool addedScore = false;
   
    private void Start()
    {
        scoreEnryGUIElements = new List<GameObject>();
        SceneController.instance.OnLevelLoaded += () => nameInput.gameObject.SetActive(true);
    }

    public void DisplayScoreBoard()
    {
        foreach (var VARIABLE in scoreEnryGUIElements) Destroy(VARIABLE);
        List<ScoreController.ScoreBoardEntry> scoreBoard = ScoreController.instance.GetScoreBoard();
        foreach (ScoreController.ScoreBoardEntry sbe in scoreBoard)
        {
            GameObject newGUIEntry = Instantiate(scoreEntryPrefab, transform);
            newGUIEntry.GetComponent<ScoreBoardEntryDisplay>().SetDisplay(sbe);
            scoreEnryGUIElements.Add(newGUIEntry);
        }
    }

    public void OnAddFinished()
    {
        ScoreController.instance.AddScore(nameInput.text);
        DisplayScoreBoard();
        nameInput.gameObject.SetActive(false);
    }
}
