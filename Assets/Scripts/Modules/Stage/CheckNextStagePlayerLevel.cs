using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckNextStagePlayerLevel : MonoBehaviour
{
    [SerializeField]
    GameObject[] ShowClearObj;
    [SerializeField]
    Player player;
    private void OnEnable()
    {
        if (player.ChangeExp(EnemyGenerators.Instance.CurrentExp))
            ShowClearObj[0].SetActive(true);
        else
            ShowClearObj[1].SetActive(true);
    }

    public void OnCheck()
    {
        ShowClearObj[0].SetActive(false);
        ShowClearObj[1].SetActive(false);
        this.gameObject.SetActive(false);
    }
}
