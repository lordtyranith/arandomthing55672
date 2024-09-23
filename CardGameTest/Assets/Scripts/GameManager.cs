using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>   
{
    public StageLevel StageLevel;
    public CardManager CardManager;
    public CardsHolder CardsHolder;

    public Queue<Card> cardsQueue = new Queue<Card>();
    public List<Card> cardTurnedRight = new List<Card>();   

    bool isComparing = false;
    bool gameOver = false;

    public bool firstPair = true;   

    private void Start()
    {
        cardTurnedRight.Clear();    
        CardsHolder.StartingGame();
        PlayerManager.Instance.playerPlays = 0;
        PlayerManager.Instance.playerCombo = 0;
        PlayerManager.Instance.playerScore = 0;
        UIManager.Instance.UpdatingUI();

    }



    public void StartCompair()
    {
        if (cardsQueue.Count >= 2)
        {
           
           StartCoroutine(comparison());   
         
        }
    }



    IEnumerator comparison()
    {
        yield return new WaitUntil(() => isComparing == false);

        isComparing = true;
        Card first = cardsQueue.Dequeue();
        Card second = cardsQueue.Dequeue();

        yield return new WaitUntil(() => second.isRotating == false);

        if(first.cardType == second.cardType)
        {
            first.TriggerScaleFeedback();
            second.TriggerScaleFeedback();
            Hit();
            cardTurnedRight.Add(first);
            cardTurnedRight.Add(second);


        }
        else
        {
            Miss();
            yield return new WaitForSeconds(1f);
            first.TurnCardFaceDown();
            second.TurnCardFaceDown();
        }
        isComparing = false;

    }




    public void Hit()
    {
        PlayerManager.Instance.playerScore = PlayerManager.Instance.playerScore + 1;
        PlayerManager.Instance.playerCombo = PlayerManager.Instance.playerCombo + 1;
        SoundManager.Instance.PlayCorrect();
        UIManager.Instance.UpdatingUI();


    }

    public void Miss()
    {
        PlayerManager.Instance.playerCombo = 0;
        SoundManager.Instance.PlayWrong();
        UIManager.Instance.UpdatingUI();


    }

    private void Update()
    {
        PlayerVictory();
        PlayerLose();

        // StartCoroutine(CheckIfGameEnds());
    }

    IEnumerator CheckIfGameEnds()
    {
        yield return new WaitForSeconds(0.1f);
        PlayerVictory();
        PlayerLose();

    }
    public void PlayerVictory()
    {
        if(!gameOver)
        {
            if (cardTurnedRight.Count == CardsHolder.ActiveCards.Count)
            {
                // Victory Match
                SoundManager.Instance.PlayWin();
                gameOver = true;
            }
        }
  
    }

    public void PlayerLose()
    {
        if (!gameOver)
        {
            if (PlayerManager.Instance.playerPlays == CardsHolder.ActiveCards.Count * 2)
            {
                // Lose match
                SoundManager.Instance.PlayLose();
            }
        }
 
    }

}
