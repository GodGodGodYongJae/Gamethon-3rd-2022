using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveSkilCoolTime : MonoBehaviour
{

    bool skillCoolDown;
    [SerializeField]
    Player player;

  void Start()
    {
        StartCoroutine(CoolTime(30));
    }
    IEnumerator CoolTime(float cool)
    {
        while (cool > 1.0f)
        {
            cool -= Time.deltaTime;
            UIManager.Instance.SkilCoolDown(1.0f / cool);
            yield return new WaitForFixedUpdate();
        }
        skillCoolDown = true;
    }

    public void OnSwordSkilBtn()
    {
        if (skillCoolDown.Equals(true) && player.isDeath.Equals(false))
        {
            CutSceneManager.Instance.OnScene(true, CutSceneManager.Events.SwordSkill, true);
            skillCoolDown = false;
            StartCoroutine(CoolTime(30));
        }
    }
}
