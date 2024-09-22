using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>   
{
    public StageLevel StageLevel;
    public CardManager CardManager;
    public CardsHolder CardsHolder;

    public Card pair1;
    public Card pair2;




    public bool firstPair = true;   

    private void Start()
    {
        CardsHolder.StartingGame();
    }

    public void CheckIfItsMatch()
    {
        if(pair1.cardType == pair2.cardType)
        {
            Hit();

        }
        else
        {

            Miss();

        }
    }

    public void Hit()
    {
        PlayerManager.Instance.playerScore = PlayerManager.Instance.playerScore + 1;
        PlayerManager.Instance.playerCombo = PlayerManager.Instance.playerCombo + 1;


        pair1.cardUsed = true;
        pair2.cardUsed = true;  


        pair1 = null;
        pair2 = null;


    }

    public void Miss()
    {
        pair1.RotationDelaySeconds(1.2f);
        pair2.RotationDelaySeconds(1.2f);

        PlayerManager.Instance.playerCombo = 0;

    

        pair1 = null;
        pair2 = null;
    }

}
