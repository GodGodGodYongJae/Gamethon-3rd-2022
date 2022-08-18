using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueController : MonoBehaviour
{
    [System.Serializable]
    public class BackGorundList : SerializableDictionary<string, Sprite> { };
    public BackGorundList e_backgroundList = new BackGorundList();

    [System.Serializable]
    public class SilatList : SerializableDictionary<string, Sprite> { };
    public SilatList e_SiluatList = new SilatList();


    [SerializeField]
    GameObject OnGame;

    [SerializeField]
    Image Bg;
    [SerializeField]
    Image Siluat;
    
    public void ChangeBG(string _key)
    {
        foreach (var item in e_backgroundList)
        {
            if(item.Key == _key)
            {
                Bg.sprite = item.Value;
            }
        }
        ChangeSiluat(_key);
    }
    public void ComplateDialogue()
    {
        OnGame.SetActive(true);
        this.gameObject.SetActive(false);
    }

    private void ChangeSiluat(string _key)
    {
        bool isCheck = false;
        foreach(var item in e_SiluatList)
        {
            if(item.Key == _key)
            {
                Siluat.sprite = item.Value;
                Siluat.color = new Color(255, 255, 255, 255);
                isCheck = true;
            }
        }
        if (isCheck.Equals(false))
        {
            Siluat.sprite = null;
            Siluat.color = new Color(0, 0, 0, 0);
        }
            
    }
}
