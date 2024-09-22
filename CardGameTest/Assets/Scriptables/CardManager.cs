using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class UniqueCard
{
    public string CardName;
    public string CardID;
    public Material CardMaterial;

}
[CreateAssetMenu(fileName = "UniqueCard", menuName = "CardsToList")]


public class CardManager : ScriptableObject
{
    public List<UniqueCard> CardsList;



}