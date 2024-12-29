using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InGameUI : MonoBehaviour
{

    [SerializeField] GameObject PlayerBust;
    [SerializeField] GameObject WinnerScreen;
    [SerializeField] GameObject MainUI;
    [SerializeField] GameObject[] Player1Lives;
    [SerializeField] GameObject[] Player2Lives;
    [SerializeField] GameObject[] Player3Lives;
    [SerializeField] GameObject[] Player4Lives;
    private GameObject CurrentLife;

    [SerializeField] List<PlayerData> playerDatas = new List<PlayerData>();

    [SerializeField] List<TextMeshProUGUI> damageNumbers = new List<TextMeshProUGUI>();

    string playerDamage;

    public void ReduceLife(int PlayerNum)
    {
        switch (PlayerNum)
        {
            case 1:
                ReducePlayer1Life();
                break;
            case 2:
                ReducePlayer2Life();
                break;
            case 3:
                ReducePlayer3Life();
                break;
            case 4:
                ReducePlayer4Life();
                break;
        }
    }
    public void ReducePlayer1Life()
    {
        foreach (var life in Player1Lives)
        {
            if (life.activeInHierarchy)
            {
                CurrentLife = life;
            }
        }
        if (CurrentLife != null)
        {
            CurrentLife.SetActive(false);
        }
    }
    public void IncreasePlayer1Life() { }

    public void ReducePlayer2Life()
    {
        foreach (var life in Player2Lives)
        {
            if (life.activeInHierarchy)
            {
                CurrentLife = life;
            }
        }
        if (CurrentLife != null)
        {
            CurrentLife.SetActive(false);
        }
    }
    public void IncreasePlayer2Life() { }
    public void ReducePlayer3Life()
    {
        foreach (var life in Player3Lives)
        {
            if (life.activeInHierarchy)
            {
                CurrentLife = life;
            }
        }
        if (CurrentLife != null)
        {
            CurrentLife.SetActive(false);
        }
    }
    public void IncreasePlayer3Life() { }
    public void ReducePlayer4Life()
    {
        foreach (var life in Player4Lives)
        {
            if (life.activeInHierarchy)
            {
                CurrentLife = life;
            }
        }
        if (CurrentLife != null)
        {
            CurrentLife.SetActive(false);
        }
    }
    public void IncreasePlayer4Life()
    {

    }

    public void UpdatePlayerDamage()
    {
        for (int i = 0; i < playerDatas.Count; i++)
        {
            playerDamage = playerDatas[i].damage.ToString();

            if (playerDamage.Length < 2)
            {
                playerDamage = "00" + playerDamage;
            }
            else if (playerDamage.Length < 3)
            {
                playerDamage = "0" + playerDamage;
            }

            damageNumbers[i].text = playerDamage;
        }
    }

}
