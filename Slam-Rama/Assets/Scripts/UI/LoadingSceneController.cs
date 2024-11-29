using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingSceneController : MonoBehaviour
{
    AsyncOperation asyncOperation;
    [SerializeField] RoundData roundData;
    // The loading bar UI object
    public Image LoadingBar;

    void Start()
    {
        switch (roundData.roundType)
        {
            case RoundData.RoundType.Random:

                break;
            case RoundData.RoundType.Clock:
                asyncOperation = SceneManager.LoadSceneAsync("Clock");
                break;
            case RoundData.RoundType.Castle:
                asyncOperation = SceneManager.LoadSceneAsync("Castle");
                break;
            case RoundData.RoundType.Witch:
                asyncOperation = SceneManager.LoadSceneAsync("couldren");
                break;
            case RoundData.RoundType.Boxing:
                asyncOperation = SceneManager.LoadSceneAsync("Boxing");
                break;

        }
        // Loads just the boxing scene for now
        asyncOperation = SceneManager.LoadSceneAsync("Boxing");
        // Halts the destination scene from loading until it has fully loaded
        asyncOperation.allowSceneActivation = false;
        // Starts the coroutine for the laoding screen
        StartCoroutine(WaitAndLoadTutorialLevel());
    }

    IEnumerator WaitAndLoadTutorialLevel()
    {
        yield return null;
        // Loops until the operation has finished
        while (asyncOperation.isDone == false)
        {
            Debug.Log("Loading progress: " + (asyncOperation.progress * 100) + "%");
            // Updated loading bar based on the loading progress
            LoadingBar.fillAmount = asyncOperation.progress * 100;
            // Checks if the loading has reached a certain threshold
            if (asyncOperation.progress > 0.89)
            {
                // Allows the scene to load
                asyncOperation.allowSceneActivation = true;
            }
            yield return new WaitForSeconds(3.0f);
        }
    }
}
