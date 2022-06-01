using System;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// this is a simple scoped GUI class which retrieves the time struct from the game manager and displays it
/// in the HUD. String interpolation with :00 can be used to determine how many digits of a number should be displayed
/// which is neat as time is mostly shown with two digits 
/// </summary>
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
