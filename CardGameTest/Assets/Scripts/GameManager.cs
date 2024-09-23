using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Xml;
using UnityEditor.SceneManagement;
using UnityEngine;
using static UnityEditor.Progress;

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

    bool gameStarted = false;   

    public void StartingLevel()
    {
        cardTurnedRight.Clear();
        CardsHolder.StartingGame();
        PlayerManager.Instance.playerPlays = 0;
        PlayerManager.Instance.playerCombo = 0;
        PlayerManager.Instance.playerScore = 0;
        UIManager.Instance.UpdatingUI();
        gameStarted = true ;
    }

    public void LoadingSavedGame()
    {
        UIManager.Instance.UpdatingUI();
        CardsHolder.LoadingGame();
        gameStarted = true;
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

        if(first.cardType.CardName == second.cardType.CardName)
        {
            first.TriggerScaleFeedback();
            second.TriggerScaleFeedback();
            cardTurnedRight.Add(first);
            cardTurnedRight.Add(second);
            Hit();

        }
        else
        {
            Miss();
            yield return new WaitForSeconds(1f);

            first.TurnCardFaceDown();
            second.TurnCardFaceDown();
            yield return new WaitForSeconds(1f);
            first.cardUsed = false;
            second.cardUsed = false;
        }
        isComparing = false;
        SaveGame();

    }


    public void LoadingGame()
    {
        LoadGame();
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
        if (gameStarted)
        {
            PlayerVictory();
            PlayerLose();

        }

    }

  
    public void PlayerVictory()
    {
        if(!gameOver)
        {
            if (cardTurnedRight.Count == CardsHolder.ActiveCards.Count)
            {            
                SoundManager.Instance.PlayWin();
                NextStage();
            }
        }
  
    }

    public void PlayerLose()
    {
        if (!gameOver)
        {
            if (PlayerManager.Instance.playerPlays == CardsHolder.ActiveCards.Count * 2)
            {
                SoundManager.Instance.PlayLose();
            }
        }
 
    }


    public void SaveGame()
    {
        List<UniqueCard> cards = new List<UniqueCard>();    

        foreach(Card c in CardsHolder.ActiveCards)
        {
            UniqueCard newCard = c.cardType;

            cards.Add(newCard);
        }

        PlayerData data = new PlayerData(PlayerManager.Instance.playerScore, PlayerManager.Instance.playerCombo, GameManager.Instance.StageLevel.ToString(), PlayerManager.Instance.playerPlays, PlayerManager.Instance.playerTotalScore,
        CardsHolder.ActiveCards, cardTurnedRight, cards);
        SaveSystem.SavePlayerData(data);

    }

    public void LoadGame()
    {
        PlayerData data = SaveSystem.LoadPlayerData();

        if (data != null)
        {
            PlayerManager.Instance.playerScore = data.playerScore;
            PlayerManager.Instance.playerCombo = data.playerCombo;
            PlayerManager.Instance.stageLevel = (StageLevel)System.Enum.Parse(typeof(StageLevel), data.stage);
            PlayerManager.Instance.playerTotalScore = data.playerTotalScore;
            CardsHolder.ActiveCards = data.ActiveCards;

            int indexOf = 0;
            foreach(Card c in CardsHolder.ActiveCards)
            {
                c.cardType = data.cardsTypeActive[indexOf];
                indexOf++;  
            }

            cardTurnedRight = data.turnedFaceUp;    
        }
    }


    public void NextStage()
    {
       foreach(Card c in CardsHolder.ActiveCards)
       {
            c.TurnCardFaceDown();
            c.cardUsed = false;
       }
       StartCoroutine(NextStageLoading());  
    }

    IEnumerator NextStageLoading()
    {

        gameOver = false;
        UIManager.Instance.StartFadeIn();
        cardTurnedRight.Clear();
        PlayerManager.Instance.playerPlays = 0;
        PlayerManager.Instance.playerCombo = 0;
        PlayerManager.Instance.playerScore = 0;
        yield return new WaitForSeconds(2f);
      
        
        PlayerManager.Instance.stageLevel = GetNextStage(StageLevel);
        StageLevel = PlayerManager.Instance.stageLevel;
        yield return new WaitForSeconds(2f);

        UIManager.Instance.UpdatingUI();

        CardsHolder.StartingGame();
        gameStarted = true;

    }
    public StageLevel GetNextStage(StageLevel currentStage)
    {
        switch (currentStage)
        {
            case StageLevel.One:
                return StageLevel.Two;
            case StageLevel.Two:
                return StageLevel.Three;
            case StageLevel.Three:
                return StageLevel.Four;
            case StageLevel.Four:
                return StageLevel.Five;
            case StageLevel.Five:
                return StageLevel.One; 
            default:
                return currentStage; 
        }

    }
}
