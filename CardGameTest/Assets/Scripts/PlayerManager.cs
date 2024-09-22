using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum DifficultyLevel {Easy, Medium, Hard}


public class PlayerManager : Singleton<PlayerManager>
{
    public int playerScore;
    public int playerCombo;
    public int playerMatch;
    public DifficultyLevel difficultyLevel;
    public int playerPlays;




}
