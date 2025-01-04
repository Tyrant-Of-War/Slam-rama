using System.Collections.Generic;
using UnityEngine;

public class CardSetup : MonoBehaviour
{
    public Transform[] SetPositions;
    public GameObject[] Cards;
    public List<GameObject> currentCards;
    private void Awake()
    {
        SetCurrentCards();
        Setup();
    }
    public void Setup()
    {
        for (int i = 0; i < currentCards.Count; i++)
        {
            currentCards[i].transform.position = SetPositions[i].transform.position;
            currentCards[i].SetActive(true);
        }
    }
    public void SetCurrentCards()
    {
        List<int> avalableIndex = new List<int>();
        for (int i = 0; i < Cards.Length; i++)
        {
            avalableIndex.Add(i);
        }
        for (int i = 0; i < 3; i++)
        {
            int index = Random.Range(0, avalableIndex.Count);
            currentCards.Add(Cards[avalableIndex[index]]);
            avalableIndex.Remove(index);
        }
    }
}
