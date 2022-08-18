using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StatusData : MonoBehaviour
{
   public PlayFabData.Stat stat;

    [HideInInspector]
    public int StatNum;
    TextMeshProUGUI textpro;

    private void Awake()
    {
        textpro = GetComponent<TextMeshProUGUI>();
    }
    private void Update()
    {
        if (StatNum != PlayFabData.Instance.PlayerStatus[stat])
        {
            StatNum = PlayFabData.Instance.PlayerStatus[stat];
            textpro.text = StatNum.ToString();
        }
    }
}
