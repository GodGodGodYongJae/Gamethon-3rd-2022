
using TMPro;
using UnityEngine;


public class PlayFabShowEconomy : MonoBehaviour
{

     TextMeshProUGUI Text;
    [SerializeField]
    string VC;
    int account = 0;
    // Start is called before the first frame update
    private void Awake()
    {
        Text = GetComponent<TextMeshProUGUI>();
  
    }

    private void Update()
    {
        if(account != PlayFabData.Instance.WhatAccountShow(VC))
        {
            account = PlayFabData.Instance.WhatAccountShow(VC);
            Text.text = account.ToString();
        }
    }


}
