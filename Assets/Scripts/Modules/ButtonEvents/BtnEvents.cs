using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtnEvents : MonoBehaviour
{
    public void OnStageClear()
    {
        if (EnemyFactoryMethod.Instance.stageController.isLast)
        {
            ScenesManager.Instance.OnLobbyScene();
        }
        else
        {
            CutSceneManager.Instance.OnScene(false, CutSceneManager.Events.StageClear, true);
            EnemyFactoryMethod.Instance.stageController.NextWave();
            this.gameObject.SetActive(false);

        }
    }

    public void OnStartGame()
    {
        ScenesManager.Instance.OnGameStartScene();
    }

    public void OnGiveUpButton()
    {
        ScenesManager.Instance.OnLobbyScene();
    }

    public void OnPauseButton(GameObject OpenUI)
    {
        OpenUI.SetActive(true);
        Time.timeScale = 0.0f;
    }

    public void OnExitPause()
    {
        this.gameObject.SetActive(false);
        Time.timeScale = 1.0f;
    }


}
