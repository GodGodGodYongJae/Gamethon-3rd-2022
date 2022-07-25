using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutSceneManager : Singleton<CutSceneManager>
{
    [SerializeField]
     GameObject DeathScene;
  public void OnDeathScene(bool stat)
    {
        DeathScene.SetActive(stat);
    }
}
