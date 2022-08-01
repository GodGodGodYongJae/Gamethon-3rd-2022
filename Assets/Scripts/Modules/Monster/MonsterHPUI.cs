using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonsterHPUI : MonoBehaviour
{
    float timer;
    [SerializeField]
    GameObject HPGrid;
    [SerializeField]
    Sprite[] HpSprite;
    // Start is called before the first frame update
   

    public void OnDamageEvent(int curHp, int MaxHp)
    {
        timer = 0;
        this.transform.localScale = new Vector3(1, 1, 1);
        for (int i = 9; i > -1; i--)
        {
            if (curHp < MaxHp * (i * 0.1f))
            {
                HPGrid.transform.GetChild(i).GetComponent<Image>().sprite = HpSprite[0];
            }
            else if (curHp >= MaxHp * (i * 0.1f))
            {
                HPGrid.transform.GetChild(i).GetComponent<Image>().sprite = HpSprite[1];
            }
        }
    }

    private void OnEnable()
    {
       
    }
    void Start()
    {
        this.transform.localScale = new Vector3(1, 1, 1);

    }

    // Update is called once per frame
    void Update()
    {
        if (EnemyFactoryMethod.Instance.target != this.transform.parent.parent)
        {
            timer += Time.deltaTime * 1.0f;
            if (timer <= 10)
            {
                this.transform.localScale = new Vector3(0, 1, 1);
            }
        }

    }
}
