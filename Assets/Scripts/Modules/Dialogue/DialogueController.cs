using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueController : MonoBehaviour
{
    [System.Serializable]
    public class BackGorundList : SerializableDictionary<string, Sprite> { };
    public BackGorundList e_backgroundList = new BackGorundList();

    [SerializeField]
    GameObject OnGame;

    [SerializeField]
    Image Bg;
    
    public void ChangeBG(string _key)
    {
        foreach (var item in e_backgroundList)
        {
            if(item.Key == _key)
            {
                Bg.sprite = item.Value;
            }
        }
    }
    public void ComplateDialogue()
    {
        OnGame.SetActive(true);
        this.gameObject.SetActive(false);
    }
}
