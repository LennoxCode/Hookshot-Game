using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreController : MonoBehaviour
{
    // Start is called before the first frame update
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
        finalScore = Math.Min(secondsPassed - score, 0);
    }
    public void SaveScores()
    {
        int index = 0;
        foreach (ScoreBoardEntry sbe  in scoreBoard)
        {
            PlayerPrefs.SetString($"{index}_name", sbe.name);
            PlayerPrefs.SetInt($"{index}_score", sbe.score);
        }
        //scoreBoard.Add(new ScoreBoardEntry(name,finalScore));
        //scoreBoard.Sort((ScoreBoardEntry sbe1, ScoreBoardEntry sbe2) => sbe1.score - sbe2.score);
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
    public struct GameTime
    {
        public int minutes;
        public int seconds;
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
}
