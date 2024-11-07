using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingSceneController : MonoBehaviour
{
    AsyncOperation asyncOperation;
    public Image LoadingBar;
    void Start()
    {
        asyncOperation = SceneManager.LoadSceneAsync("Movement Test");
        asyncOperation.allowSceneActivation = false;
        StartCoroutine(WaitAndLoadTutorialLevel());
    }

    IEnumerator WaitAndLoadTutorialLevel()
    {
        yield return null;
        while (asyncOperation.isDone == false)
        {
            Debug.Log("Loading progress: " + (asyncOperation.progress * 100) + "%");
            LoadingBar.fillAmount = asyncOperation.progress * 100;
            if (asyncOperation.progress > 0.89)
            {
                asyncOperation.allowSceneActivation = true;
            }
            yield return new WaitForSeconds(3.0f);
        }
    }
}
