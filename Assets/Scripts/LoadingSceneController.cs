using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingSceneController : MonoBehaviour
{

    static string nextScene;

    [SerializeField]
    Slider progressBar;
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

        AsyncOperation op = SceneManager.LoadSceneAsync(nextScene);
        op.allowSceneActivation = false;
        //op.allowSceneActivation = false;
        float timer = 0f;
        while (!op.isDone)
        {
            yield return null;
            print(op.progress);
            if (op.progress < 0.9)
                progressBar.value = op.progress;
            else
            {
                timer += Time.unscaledTime;
                progressBar.value = Mathf.Lerp(0.9f, 1, timer);
                if(progressBar.value >= 1f)
                {
                    op.allowSceneActivation = true;
                    yield break; 
                }
            }
        }

    }
}
