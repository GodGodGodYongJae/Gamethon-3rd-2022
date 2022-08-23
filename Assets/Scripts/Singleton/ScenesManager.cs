using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScenesManager : Singleton<ScenesManager>
{

    private void Start()
    {
        SoundManager.Inst.PlayBGM("Suspense - Percussion & Pads");
    }
    public void OnGameStartScene()
    {
        SoundManager.Inst.PlayBGM("Space Threat (Electronic Dramatic Version)");
        LoadingSceneController.LoadSene("InGame");
    }
    public void OnLobbyScene()
    {
        SoundManager.Inst.PlayBGM("Suspense - Percussion & Pads");
        LoadingSceneController.LoadSene("Lobby");
    }
    public void OnTitleScene()
    {
        LoadingSceneController.LoadSene("Title");
    }
}
