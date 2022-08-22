using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IPAScript : MonoBehaviour
{
    public int Dia = 0;
    public int Ruby = 0;
    public int Gas = 0;
    public void Rewar()
    {

        PlayFabData.Instance.AddAccountData("DM", Dia);
        PlayFabData.Instance.AddAccountData("RU", Ruby);
        PlayFabData.Instance.AddAccountData("ST", Gas);
        PlayFabData.Instance.GetAccountData();
    }
    public void Failed()
    {
        print("½ÇÆÐ");
    }
}
