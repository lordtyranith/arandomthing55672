using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : Singleton<UIManager>   
{
    [SerializeField] TextMeshProUGUI scoreCounter;
    [SerializeField] TextMeshProUGUI ComboCounter;
    [SerializeField] TextMeshProUGUI movesCounter;
    [SerializeField] TextMeshProUGUI stageLevel;
    [SerializeField] TextMeshProUGUI totalScore;


    public void UpdatingUI()
    {
        scoreCounter.text = PlayerManager.Instance.playerScore.ToString();
        ComboCounter.text = PlayerManager.Instance.playerCombo.ToString();
        movesCounter.text = PlayerManager.Instance.playerPlays.ToString();
        totalScore.text = PlayerManager.Instance.playerTotalScore.ToString();
        stageLevel.text = PlayerManager.Instance.stageLevel.ToString();  
        
    }
}
