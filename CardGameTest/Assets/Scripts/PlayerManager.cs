using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum StageLevel {One, Two, Three, Four, Five}


public class PlayerManager : Singleton<PlayerManager>
{
    public int playerScore;
    public int playerCombo;
   // public int playerMatch;
    public StageLevel stageLevel;
    public int playerPlays;




}
