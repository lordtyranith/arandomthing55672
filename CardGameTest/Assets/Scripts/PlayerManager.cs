using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public enum StageLevel {One, Two, Three, Four, Five}

[System.Serializable]
public class PlayerManager : Singleton<PlayerManager>
{
    public int playerScore = 0;
    public int playerCombo = 0;
    public StageLevel stageLevel;
    public int playerPlays = 0;
    public int playerTotalScore = 0;
    public string stage;

    public List<Card> ActiveCards = new List<Card>();
    public List<Card> turnedFaceUp = new List<Card>();

    public List<UniqueCard> cardsTypeActive = new List<UniqueCard>();
 



}


[System.Serializable]
public class PlayerData
{
    public int playerScore = 0;
    public int playerCombo = 0;
    public int playerPlays = 0;
    public int playerTotalScore = 0;
    public string stage;

    public List<Card> ActiveCards = new List<Card>();
    public List<Card> turnedFaceUp = new List<Card>();
    public List<UniqueCard> cardsTypeActive = new List<UniqueCard>();



    public PlayerData(int score, int combo, string level, int plays, int totalscore, List<Card> activeCards, List<Card> facedUp, List<UniqueCard> listPrint)
    {
        playerScore = score;
        playerCombo = combo;
        stage = level;
        playerPlays = plays;
        playerTotalScore = totalscore;
        ActiveCards = activeCards;
        turnedFaceUp = facedUp;
        cardsTypeActive = listPrint;

    }
}
