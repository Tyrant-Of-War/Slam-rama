using System.Collections.Generic;
using UnityEngine;

public class CardSetup : MonoBehaviour
{
    public Transform[] SetPositions;
    public GameObject[] Cards;
    public List<GameObject> currentCards;
    private void Awake()
    {
        currentCards = new List<GameObject>();
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
        if (currentCards.Count > 0) { foreach (var card in currentCards) { card.gameObject.SetActive(false); } }
        currentCards.Clear();
        List<int> avalableIndex = new List<int>();
        for (int i = 0; i < Cards.Length; i++)
        {
            avalableIndex.Add(i);
        }
        for (int i = 0; i < 3; i++)
        {
            int randomIndex = Random.Range(0, avalableIndex.Count);
            currentCards.Add(Cards[avalableIndex[randomIndex]]);
            avalableIndex.RemoveAt(randomIndex);
        }
    }
}
