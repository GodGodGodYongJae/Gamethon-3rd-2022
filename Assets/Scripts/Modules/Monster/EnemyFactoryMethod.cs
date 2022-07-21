


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFactoryMethod : Singleton<EnemyFactoryMethod>
{
    [System.Serializable]
    public class EnemiesList : SerializableDictionary<string, GameObject> { };

    public EnemiesList e_enemyPrefabDictionary = new EnemiesList();
    public List<GameObject> MonsterList = new List<GameObject>();

    public Transform target;


    protected override void Awake()
    {
        base.Awake();
        e_enemyPrefabDictionary.OnBeforeSerialize();

    }
    private void Start()
    {
        for (int i = 0; i < 1; i++)
        {
            CreateEnemy("Dummy", new Vector3(3 * i, 0, 3 * i),Quaternion.identity);
        }
        

    }
  
    public void CreateEnemy(string keyName,Vector3 pos, Quaternion quaternion)
    {
        if (!e_enemyPrefabDictionary.ContainsKey(keyName))
        {
            Debug.LogError("[EnemyFactoryMethod] 해당 오브젝트 풀을 찾을 수 없습니다. " + keyName);
            return;
        }
        else
        {
            GameObject obj = e_enemyPrefabDictionary[keyName];
            MonsterList.Add(Instantiate(obj,pos, quaternion));
            //MonsterList.Add(obj.transform);
        }
        if (target == null)
        {
            target = MonsterList[0].transform;
        }
    }
    //IEnumerator RateTarget()
    //{
    //   //yield return new WaitForSeconds(0.5f);
    //   // target = MonsterList[0].transform;
    //}
    public void DeleteEnemy(GameObject obj)
    {
        Enemy ObjEnemy = obj.GetComponent<Enemy>();
        if(target == obj.transform)
        {
            if (MonsterList.Count - 1 > 0)
            {
                MonsterList.Remove(obj);
                //StartCoroutine("RateTarget");
                target = MonsterList[0].transform;
            }
            else
            {
                MonsterList.Remove(obj);
                // 0개가 되면 우선 임시적으로 다시 생성해주고 있음.
                GameObject empty = e_enemyPrefabDictionary["Dummy"];
                MonsterList.Add(Instantiate(empty));
                //StartCoroutine("RateTarget");
                target = MonsterList[0].transform;
            }

            ObjEnemy.StartCoroutine(ObjEnemy.OnRateDestory());
            //Destroy(obj);
            return;

        }
        ObjEnemy.StartCoroutine(ObjEnemy.OnRateDestory());
        //Destroy(obj);
        MonsterList.Remove(obj);
    }


}
