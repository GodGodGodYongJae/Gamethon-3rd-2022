using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutSceneManager : Singleton<CutSceneManager>
{
    public enum Events {
    Death,
    SwordSkill,
    StageClear
    }

    [SerializeField]
    GameObject ObjManager;
    [SerializeField]
    List<GameObject>SceneEvents;

    public bool CineMachineStart;
  public void OnScene(bool stat,Events even, bool Objfalse = false)
    {
        CineMachineStart = stat;
        if (Objfalse)
        {
            ObjManager.SetActive(!stat);
        }
           
        SceneEvents[(int)even].SetActive(stat);
    }
   
}
