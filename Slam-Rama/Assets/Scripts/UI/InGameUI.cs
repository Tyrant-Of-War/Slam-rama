using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InGameUI : MonoBehaviour
{
    [SerializeField] GameObject MainUI;
    [SerializeField] GameObject[] Player1Lives;
    [SerializeField] GameObject[] Player2Lives;
    [SerializeField] GameObject[] Player3Lives;
    [SerializeField] GameObject[] Player4Lives;
    private GameObject CurrentLife;

    [SerializeField] List<PlayerData> playerDatas = new List<PlayerData>();

    [SerializeField] List<TextMeshProUGUI> damageNumbers = new List<TextMeshProUGUI>();

    [SerializeField] List<Image> playerPowerUps = new List<Image>();

    [SerializeField] List<Sprite> powerUpSprites = new List<Sprite>();

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

            CurrentLife = null;
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

            CurrentLife = null;
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

            CurrentLife = null;
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

            CurrentLife = null;
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

    public void UpdatePowerUp()
    {
        for (int i = 0; i < playerDatas.Count; i++)
        {
            if (playerDatas[i].itemID == 0) 
            {
                playerPowerUps[i].enabled = false;
            }
            else if (playerDatas[i].itemID == 1)
            {
                playerPowerUps[i].enabled = true;
                playerPowerUps[i].sprite = powerUpSprites[0];
            }
            else if (playerDatas[i].itemID == 2)
            {
                playerPowerUps[i].enabled = true;
                playerPowerUps[i].sprite = powerUpSprites[1];
            }
            else if (playerDatas[i].itemID == 3)
            {
                playerPowerUps[i].enabled = true;
                playerPowerUps[i].sprite = powerUpSprites[2];
            }
            else if (playerDatas[i].itemID == 4)
            {
                playerPowerUps[i].enabled = true;
                playerPowerUps[i].sprite = powerUpSprites[3];
            }
            else if (playerDatas[i].itemID == 5)
            {
                playerPowerUps[i].enabled = true;
                playerPowerUps[i].sprite = powerUpSprites[4];
            }
            else if (playerDatas[i].itemID == 6)
            {
                playerPowerUps[i].enabled = true;
                playerPowerUps[i].sprite = powerUpSprites[5];
            }
        }
    }

}
