using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScenesManager : Singleton<ScenesManager>
{
    public void OnGameStartScene()
    {
        LoadingSceneController.LoadSene("InGame");
    }
    public void OnTitleScene()
    {
        LoadingSceneController.LoadSene("Title");
    }
}
