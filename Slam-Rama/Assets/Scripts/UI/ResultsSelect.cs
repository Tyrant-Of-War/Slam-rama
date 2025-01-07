using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ResultsSelect : MonoBehaviour
{

    public void ReturnToLobby()
    {
        SceneManager.LoadScene("MainMenu");
    }

}
