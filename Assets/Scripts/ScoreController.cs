using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// This class is responsible for calculating and saving scores. the score calculation is subscribed to
/// the goal entered event and just divides the max possible score by the seconds passed minus the sum of the collected
/// coin values. In addition this class also holds a Scoreboard which concists of 10 scoreboard entries with
/// a name and and score attached. After finishing a level the player can add his score and the board is sorted.
/// I opted to use Playerprefs with string interpolation to save via scoreboard rank
/// and level id. I know this is not the best way but I found it simpler then adding all the boilerplate code
/// for proper IO management 
/// </summary>
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
        SceneController.instance.OnLevelLoaded += LoadScores;
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
        int levelIndex = SceneController.instance.currLevelIndex;
        int index = 0;
        foreach (ScoreBoardEntry sbe  in scoreBoard)
        {
            PlayerPrefs.SetString($"lvl_{levelIndex}_{index}_name", sbe.name);
            PlayerPrefs.SetInt($"lvl_{levelIndex}_{index}_score", sbe.score);
            index++;
        }
    }

    public void LoadScores()
    {
        int levelIndex = SceneController.instance.currLevelIndex;
        scoreBoard = new List<ScoreBoardEntry>();
        for (int i = 0; i < 10; i++)
        {
            if(!PlayerPrefs.HasKey($"lvl_{levelIndex}_{i}_name")) break;
            string name = PlayerPrefs.GetString($"lvl_{levelIndex}_{i}_name");
            int score = PlayerPrefs.GetInt($"lvl_{levelIndex}_{i}_score");
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
