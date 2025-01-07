using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialExit : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        PlayerPrefs.SetInt("Tutorial_Completed", 1);

        SceneManager.LoadScene("LoadingScreen");
    }
}
