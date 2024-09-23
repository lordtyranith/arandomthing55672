using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum StageLevel {One, Two, Three, Four, Five}


public class PlayerManager : Singleton<PlayerManager>
{
    public int playerScore;
    public int playerCombo;
    public StageLevel stageLevel;
    public int playerPlays;

    public int playerTotalScore;



}
