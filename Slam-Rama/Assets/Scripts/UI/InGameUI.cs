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
            if (life.active)
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
            if (life.active)
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
            if (life.active)
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
            if (life.active)
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

}
