using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]

public class CardManager
{
    public string CardName;
    public string CardID;
    public Material CardImage;

}

[CreateAssetMenu(fileName = "CardManager", menuName = "ScriptableObjects/CardManager")]

public class CardList : ScriptableObject
{
    public List<CardManager> listCards;



}

