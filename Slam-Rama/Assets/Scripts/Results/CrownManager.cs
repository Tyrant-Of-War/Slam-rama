using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class CrownManager : MonoBehaviour
{
    [SerializeField] RoundData Rounddata;
    int Player1Deaths;
    int Player2Deaths;
    int Player3Deaths;
    int Player4Deaths;
    int[] PlayerOrder;
    public GameObject[] Player1GO;
    public GameObject[] Player2GO;
    public GameObject[] Player3GO;
    public GameObject[] Player4GO;
    public GameObject Player1Text;
    public GameObject Player2Text;
    public GameObject Player3Text;
    public GameObject Player4Text;
    private void Start()
    {
        RemoveNonPlayers();
        OrderByDeaths();
    }

    public void OrderByDeaths()
    {
        for (int i = 0; i < PlayerInput.all.Count; i++)
        {
            foreach (var Deaths in Rounddata.Order[i])
            {
                switch (i)
                {
                    case 0:
                        Player1Deaths += Deaths;
                        break;
                    case 1:
                        Player2Deaths += Deaths;
                        break;
                    case 2:
                        Player3Deaths += Deaths;
                        break;
                    case 3:
                        Player4Deaths += Deaths;
                        break;
                }
            }
        }
    }
    public void RemoveNonPlayers()
    {
        switch (PlayerInput.all.Count)
        {
            case 0:
                break;
            case 1:
                Player2Deaths = -1;
                foreach (var Gameobjects in Player2GO)
                {
                    Gameobjects.SetActive(false);
                }
                Player3Deaths = -1;
                foreach (var Gameobjects in Player3GO)
                {
                    Gameobjects.SetActive(false);
                }
                Player4Deaths = -1;
                foreach (var Gameobjects in Player4GO)
                {
                    Gameobjects.SetActive(false);
                }
                break;
            case 2:
                Player3Deaths -= 1;
                foreach (var Gameobjects in Player3GO)
                {
                    Gameobjects.SetActive(false);
                }
                Player4Deaths -= 1;
                foreach (var Gameobjects in Player4GO)
                {
                    Gameobjects.SetActive(false);
                }
                break;
            case 3:
                Player4Deaths -= 1;
                foreach (var Gameobjects in Player4GO)
                {
                    Gameobjects.SetActive(false);
                }
                break;

        }
    }
    public void SetCrowns()
    {
        int playerCount = PlayerInput.all.Count; // Number of players in the game
        int[] playerWins = new int[playerCount];
        int[] playerDeaths = new int[playerCount];
        List<GameObject>[] playerCrowns = new List<GameObject>[playerCount];

        // Example: Initialize crown GameObjects for each player
        for (int i = 0; i < playerCount; i++)
        {
            playerCrowns[i] = GetPlayerCrowns(i); // Custom method to fetch crown GameObjects
        }

        // Calculate wins and store death counts (if deaths are already tracked for players)
        for (int i = 0; i < playerCount; i++)
        {
            foreach (bool win in Rounddata.PlayerWin[i])
            {
                if (win)
                {
                    playerWins[i]++;
                }
            }

            // Assign the deaths to the playerDeaths array
            playerDeaths[i] = GetPlayerDeaths(i); // Custom method to fetch deaths for player i
        }

        // Rank players based on wins and deaths
        var playerRankings = playerWins
            .Select((wins, index) => new { PlayerIndex = index, Wins = wins, Deaths = playerDeaths[index] })
            .OrderByDescending(p => p.Wins) // Higher wins come first
            .ThenBy(p => p.Deaths)         // Fewer deaths break ties
            .ToList();

        // Reset all crowns
        foreach (var crowns in playerCrowns)
        {
            foreach (var crown in crowns)
            {
                crown.SetActive(false);
            }
        }

        // Assign crowns based on player rankings and update TextMeshPro
        for (int i = 0; i < playerCount; i++)
        {
            int playerIndex = playerRankings[i].PlayerIndex;

            // Update ranking text for the player
            UpdatePlayerRankingText(playerIndex, i + 1); // i + 1 because ranks are 1-based

            // Assign crowns based on ranking
            if (i == 0) // First place
            {
                int crownCount = playerRankings[i].Deaths == 0 ? 3 : 2;
                ActivateCrowns(playerCrowns[playerIndex], crownCount);
            }
            else if (i == 1) // Second place
            {
                ActivateCrowns(playerCrowns[playerIndex], 1); // Second place always gets 1 crown
            }
            else if (i == 2 && playerCount >= 3) // Third place (only for 3+ players)
            {
                ActivateCrowns(playerCrowns[playerIndex], 1); // Third place gets 1 crown
            }
            else if (i == 3 && playerCount == 4) // Fourth place (only for 4 players)
            {
                // Last place gets no crowns
            }
        }
    }

    private void UpdatePlayerRankingText(int playerIndex, int rank)
    {
        switch (playerIndex)
        {
            case 0:
                Player1Text.GetComponent<TextMeshProUGUI>().text = $"Rank: {rank}";
                break;
            case 1:
                Player2Text.GetComponent<TextMeshProUGUI>().text = $"Rank: {rank}";
                break;
            case 2:
                Player3Text.GetComponent<TextMeshProUGUI>().text = $"Rank: {rank}";
                break;
            case 3:
                Player4Text.GetComponent<TextMeshProUGUI>().text = $"Rank: {rank}";
                break;
        }
    }


    // Helper method to activate a specific number of crowns for a player
    private void ActivateCrowns(List<GameObject> crowns, int count)
    {
        for (int i = 0; i < crowns.Count && i < count; i++)
        {
            crowns[i].SetActive(true);
        }
    }

    // Custom method to fetch crown GameObjects for a player (replace this with your actual implementation)
    private List<GameObject> GetPlayerCrowns(int playerIndex)
    {
        switch (playerIndex)
        {
            case 0: return Player1GO.ToList();
            case 1: return Player2GO.ToList();
            case 2: return Player3GO.ToList();
            case 3: return Player4GO.ToList();
            default: return new List<GameObject>();
        }
    }

    // Custom method to fetch deaths for a player (replace this with your actual implementation)
    private int GetPlayerDeaths(int playerIndex)
    {
        switch (playerIndex)
        {
            case 0: return Player1Deaths;
            case 1: return Player2Deaths;
            case 2: return Player3Deaths;
            case 3: return Player4Deaths;
            default: return int.MaxValue; // Default to max value for invalid index
        }
    }

}
