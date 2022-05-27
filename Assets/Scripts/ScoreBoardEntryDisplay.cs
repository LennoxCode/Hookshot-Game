using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
