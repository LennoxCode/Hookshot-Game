using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreController : MonoBehaviour
{
    // Start is called before the first frame update
    public static ScoreController instance;
    public GameTime currTime { get; private set; }
   
    public int score { get; private set; }
    private int secondsPassed;
    private void Awake()
    {
        instance = this;
        DontDestroyOnLoad(this);   
        
    }

    void Start()
    {
        secondsPassed = 0;
        StartCoroutine(IncrementTime());
    }

    void Update()
    {
        var gameTime = currTime;
        gameTime.minutes = secondsPassed / 60;
        gameTime.seconds = secondsPassed % 60;
        currTime = gameTime;
    }

    public void SaveScore()
    {
        
    }

    public void GetScores()
    {
        
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
}
