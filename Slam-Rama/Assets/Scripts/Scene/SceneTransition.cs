using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    // I too stupid to comment this one
    AsyncOperation asyncOperation;
    public bool start = false;

    void Start()
    {
        asyncOperation = SceneManager.LoadSceneAsync("Lobby");
        asyncOperation.allowSceneActivation = false;
        StartCoroutine(WaitAndLoadTutorialLevel());
    }

    IEnumerator WaitAndLoadTutorialLevel()
    {
        yield return null;
        while (asyncOperation.isDone == false)
        {
            Debug.Log("Loading progress: " + (asyncOperation.progress * 100) + "%");
            if (asyncOperation.progress > 0.89 && start)
            {
                asyncOperation.allowSceneActivation = true;
            }
            yield return new WaitForSeconds(1.0f);
        }
    }
    public void Setstart()
    {
        start = true;
    }

    public void Quit()
    {
        Application.Quit();
    }
}
