using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreController : MonoBehaviour
{
    [SerializeField] private int MAX_SCORE;
    [SerializeField] private Text scoreDisplay;
    public static ScoreController instance;
    public GameTime currTime { get; private set; }

    private List<ScoreBoardEntry> scoreBoard;
    public int score { get; private set; }
    private int secondsPassed;
    private int finalScore;
    private void Awake()
    {
        instance = this;
        DontDestroyOnLoad(this);
        LoadScores();
        

    }
    
    void Start()
    {
        SceneController.instance.OnLevelLoaded += ResetScore;
        secondsPassed = 0;
        StartCoroutine(IncrementTime());
        Goal.playerEnteredGoal += CalcFinalScore;

    }

    void Update()
    {
        var gameTime = currTime;
        gameTime.minutes = secondsPassed / 60;
        gameTime.seconds = secondsPassed % 60;
        currTime = gameTime;
    }

    public void CalcFinalScore()
    {
        finalScore = MAX_SCORE / Math.Max(secondsPassed - score, 1);
        scoreDisplay.text = $"{finalScore:00000}";

    }
    public void SaveScores()
    {
        int index = 0;
        foreach (ScoreBoardEntry sbe  in scoreBoard)
        {
            PlayerPrefs.SetString($"{index}_name", sbe.name);
            PlayerPrefs.SetInt($"{index}_score", sbe.score);
            index++;
        }
    }

    public void LoadScores()
    {
        scoreBoard = new List<ScoreBoardEntry>();
        for (int i = 0; i < 10; i++)
        {
            if(!PlayerPrefs.HasKey($"{i}_name")) break;
            string name = PlayerPrefs.GetString($"{i}_name");
            int score = PlayerPrefs.GetInt($"{i}_score");
            scoreBoard.Add(new ScoreBoardEntry(name, score));
        }
    }

    private void ResetScore()
    {
        score = 0;
        secondsPassed = 0;
    }
    public IEnumerator IncrementTime()
    {
        while (true)
        {
            secondsPassed += 1;
            yield return new WaitForSeconds(1.0f);
        }
    }

    public void IncrementScore(int amount)
    {
        score += amount;
    }

    public void AddScore(string playerName)
    {
        scoreBoard.Add(new ScoreBoardEntry(playerName, finalScore));
        scoreBoard.Sort((entry1, entry2) => entry2.score - entry1.score);
        if(scoreBoard.Count > 10)scoreBoard.RemoveAt(scoreBoard.Count - 1);
        SaveScores();
    }
    public List<ScoreBoardEntry> GetScoreBoard()
    {
        if(scoreBoard == null ) LoadScores();
        return scoreBoard;
    }
    public struct ScoreBoardEntry
    {
        public ScoreBoardEntry(string name, int score)
        {
            this.name = name;
            this.score = score;
        }

        public String name;
        public int score;
        
    }
    public struct GameTime
    {
        public int minutes;
        public int seconds;
    }

}
