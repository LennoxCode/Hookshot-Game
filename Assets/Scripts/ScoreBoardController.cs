using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// this class is responsible for the display of the scorboard
/// it works by fetching the scoreboard object form the scoremanager and instantiates up to ten prefabs
/// which contain the data of each scorebaord entry. these entries use layout elements to resize according
/// to the amount of other entries are present. this element is also responsible for the button
/// which adds a new score. if the buttons is pressed it is set inactive until the next level is loaded
/// </summary>
public class ScoreBoardController : MonoBehaviour
{
    [SerializeField] private InputField nameInput;
    [SerializeField] private Button submitButton;
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
        submitButton.gameObject.SetActive(false);
    }
}
