using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>   
{
    public StageLevel StageLevel;
    public CardManager CardManager;
    public CardsHolder CardsHolder;

    public List<Card> cardTurnedPair1 = new List<Card>();    
    public List<Card> cardTurnedPair2 = new List<Card>();    

    public Queue<Card> cardsQueue = new Queue<Card>();
    public Card pair1;
    public Card pair2;

    bool isComparing = false;


    public bool firstPair = true;   

    private void Start()
    {
        CardsHolder.StartingGame();
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
            Hit();
        }
        else
        {

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
 
    }

    public void Miss()
    {
        PlayerManager.Instance.playerCombo = 0;
    }


}
