using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckNextStagePlayerLevel : MonoBehaviour
{
    [SerializeField]
    GameObject[] ShowClearObj;
    [SerializeField]
    Player player;

    private GameObject thisObj;

    BtnEvents btnEvents;
    private bool isHeal;
    private void Awake()
    {
        isHeal = false;
        btnEvents = GetComponent<BtnEvents>();
    }
    private void OnEnable()
    {
        thisObj = this.gameObject;

        PlayFabData.Instance.AddAccountData("DM", EnemyGenerators.Instance.CurrentGold);

        if (EnemyGenerators.Instance.FindNextStage(EnemyGenerators.Instance.CurrentChapter, EnemyGenerators.Instance.CurrentStage))
        {
            if (player.ChangeExp(EnemyGenerators.Instance.CurrentExp).Equals(false))
            {
                if (player.Health < (player.maxHealth * 0.3) && isHeal.Equals(false))
                    ShowClearObj[2].SetActive(true);
                else
                    ShowClearObj[0].SetActive(true);
         
            }
            else
                ShowClearObj[1].SetActive(true);
        }
        else
            ShowClearObj[3].SetActive(true);
    }


    public void OnPlayerHeal()
    {
        isHeal = true;
        float heal = player.maxHealth * 0.5f;
        player.Heal((int)heal);
    }
    public void OnCheck()
    {
        btnEvents.OnStageClear();
        ShowClearObj[0].SetActive(false);
        ShowClearObj[1].SetActive(false);
        ShowClearObj[2].SetActive(false);
        thisObj.SetActive(false);
    }
}
