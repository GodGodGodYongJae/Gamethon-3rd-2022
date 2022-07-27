using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtnEvents : MonoBehaviour
{
    public void OnStageClear()
    {
        if (EnemyFactoryMethod.Instance.stageController.isLast)
        {
            ScenesManager.Instance.OnTitleScene();
        }
        else
        {
            CutSceneManager.Instance.OnScene(false, CutSceneManager.Events.StageClear, true);
            EnemyFactoryMethod.Instance.stageController.NextWave();
            this.gameObject.SetActive(false);
        }
    }
}
