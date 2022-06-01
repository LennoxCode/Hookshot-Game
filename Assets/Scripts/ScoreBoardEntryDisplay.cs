using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// this class exists so the scoebaordGUIController can easily instantiate and change
/// the name and score of each entry
/// </summary>
public class ScoreBoardEntryDisplay : MonoBehaviour
{
   [SerializeField] private Text nameDisplay;
   [SerializeField] private Text ScoreDisplay;

   public void SetDisplay(ScoreController.ScoreBoardEntry sbe)
   {
      nameDisplay.text = sbe.name;
      ScoreDisplay.text = $"{sbe.score:00000}";
   }
}
