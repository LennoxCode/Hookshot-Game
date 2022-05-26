using System;
using UnityEngine;
using UnityEngine.UI;

public class HUDController : MonoBehaviour
{
    [SerializeField] private Text scoreDisplay;
    [SerializeField] private Text timeDisplay;

    private void Start()
    {
        scoreDisplay.text = "0";
        timeDisplay.text = "00:00";
    }

    private void LateUpdate()
    {
        ScoreController.GameTime currTime = ScoreController.instance.currTime;
        scoreDisplay.text = "" +  ScoreController.instance.score;
        timeDisplay.text = $"{currTime.minutes:00}:{currTime.seconds:00}";
    }
}
