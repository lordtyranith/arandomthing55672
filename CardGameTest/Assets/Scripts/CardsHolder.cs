using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardsHolder : MonoBehaviour
{

    [SerializeField] List<Colum> colums = new List<Colum>();
    public List<Card> ActiveCards = new List<Card>(); 
    [SerializeField] List<Card> Cards = new List<Card>();

    public void ResetFilled()
    {
        foreach(Card card in Cards)
        {
            card.cardFilled = false;
        }
       
    }
    public void StartingGame()
    {
        ResetFilled();
        OrganizeColuns(GameManager.Instance.StageLevel);    
    }

    public void LoadingGame()
    {
        ResetFilled();
        OrganizeColuns(GameManager.Instance.StageLevel);
        LoadCardsOnTheTable();
        StartCoroutine(ContinueRound());
    }

    IEnumerator ContinueRound()
    {
        foreach (Card c in ActiveCards)
        {
            c.RotationCard();
        }

        yield return new WaitForSeconds(2f);

        foreach (Card c in ActiveCards)
        {
            c.RotationCard();
        }

        yield return new WaitForSeconds(1f);

        foreach(Card c in GameManager.Instance.cardTurnedRight)
        {
            c.RotationCard();
            c.cardUsed = true;  
        }


    }
    IEnumerator StartingRound()
    {
        foreach(Card c in ActiveCards)
        {
            c.RotationCard();
        }

        yield return new WaitForSeconds(3f);
        foreach (Card c in ActiveCards)
        {
            //  c.RotationCard();
            c.TurnCardFaceDown();
        }
    }

    public void OrganizeColuns(StageLevel stage)
    {
        EnableAllColuns();

        switch (stage)
        {
            case StageLevel.One:
                DisableColum(0);
                DisableColum(1);
                DisableColum(5);
                DisableColum(6);

                break;

            case StageLevel.Two:
                DisableColum(2);
                DisableColum(3);
                DisableColum(4);
                break;

            case StageLevel.Three:
                DisableColum(0);
                DisableColum(6);
                break;

            case StageLevel.Four:
                DisableColum(3);
                break;

            case StageLevel.Five:              
                break;
        }

        ShuffleCards();
    }

    public void DisableColum(int index)
    {
       Colum columToDisable = colums[index];

       foreach(Card card in columToDisable.cards)
       {
           card.gameObject.SetActive(false);
       }
    }

    public void EnableAllColuns()
    {
        foreach (Colum c in colums)
        {
            foreach(Card card in c.cards)
            {
                card.gameObject.SetActive(true);    
            }
        }
    }

    public void ShuffleCards()
    {
        ActiveCards.Clear();    

        foreach (Card item in Cards)
        {
            if (item.gameObject.activeSelf)
            {
                ActiveCards.Add(item);
            }
        }

        Shuffle(ActiveCards);
        int indexList = 0;

        List<int> indexSelected = new List<int>();  
        foreach(UniqueCard card in GameManager.Instance.CardManager.CardsList)
        {
            Debug.Log(card.CardName);   
            indexSelected.Add(indexList);
            indexList++;    
        }

        indexList = 0;

         foreach (Card item in ActiveCards)
         {
            if (!item.cardFilled)
            {   
                item.CallTypeCard(GameManager.Instance.CardManager.CardsList[indexSelected[0]]);
                ActiveCards[indexList + 1].CallTypeCard(GameManager.Instance.CardManager.CardsList[indexSelected[0]]);
                indexSelected.Remove(indexSelected[0]);
            }       
             indexList++;      
         }      
        StartCoroutine(StartingRound());
    }

    public void LoadCardsOnTheTable()
    {
        foreach (Card item in ActiveCards)
        {
            item.CallTypeCard(item.cardType);
        }
    }

    void Shuffle<T>(List<T> list)
    {
        int n = list.Count;
        for (int i = 0; i < n; i++)
        {
            int randomIndex = Random.Range(i, n);
            T temp = list[i];
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }
    }


}

[System.Serializable]
public class Colum
{
    public string name; 
    public List<Card> cards;    
}
