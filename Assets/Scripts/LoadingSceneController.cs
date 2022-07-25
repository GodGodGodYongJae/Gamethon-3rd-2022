using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingSceneController : MonoBehaviour
{

    static string nextScene;

    [SerializeField]
    Image progressBar;
    public static void LoadSene(string sceneName)
    {
        nextScene = sceneName;
        SceneManager.LoadScene("Loading");
    }
    void Start()
    {
        Time.timeScale = 1f;
        StartCoroutine(LoadSceneProcess());
    }
   IEnumerator LoadSceneProcess()
    {
        yield return new WaitForSeconds(1);
        AsyncOperation op = SceneManager.LoadSceneAsync(nextScene);
        //op.allowSceneActivation = false;
        //float timer = 0f;
        while (!op.isDone)
        {
            yield return null;
            if (op.progress < 1.0f)
                progressBar.fillAmount = op.progress;
            else
                yield break;
        }

    }
}
