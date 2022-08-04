


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
    public Transform LastTarget;
    [SerializeField]
    private GameObject Objects;
    [SerializeField]
    private GameObject DeathObjs;

    public StageController stageController;

    public Player player;
    protected override void Awake()
    {
        base.Awake();
        e_enemyPrefabDictionary.OnBeforeSerialize();
        stageController = GetComponent<StageController>();

    }
    private void Start()
    {
        //for (int i = 0; i < 1; i++)
        //{
        //    CreateEnemy("CrabHydra", new Vector3(3 * 2, 0, 3 * 2), Quaternion.identity);
        //    CreateEnemy("Golem", new Vector3(3 * i, 0, 3 * i),Quaternion.identity);
        //    //Dummy
        //}
      

    }

  
  
    public GameObject CreateEnemy(string keyName,Vector3 pos, Quaternion quaternion)
    {
        GameObject obj;
        if (!e_enemyPrefabDictionary.ContainsKey(keyName))
        {
            Debug.LogError("[EnemyFactoryMethod] 해당 오브젝트 풀을 찾을 수 없습니다. " + keyName);
            return null;
        }
        else
        {
            obj = e_enemyPrefabDictionary[keyName];
            obj = Instantiate(obj, pos, quaternion);
            obj.transform.parent = Objects.transform;
            MonsterList.Add(obj);
            //MonsterList.Add(obj.transform);

        }
        if (target == null)
        {
            target = MonsterList[0].transform;
        }
        return obj;
    }
    //IEnumerator RateTarget()
    //{
    //   //yield return new WaitForSeconds(0.5f);
    //   // target = MonsterList[0].transform;
    //}

    public void ShowDeathEnemy()
    {
        for (int i = 0; i < DeathObjs.transform.childCount; i++)
        {
            DeathObjs.transform.GetChild(i).gameObject.SetActive(true);
        }
        
    }

    public void EmptyDeathEnemy()
    {
        for (int i = 0; i < DeathObjs.transform.childCount; i++)
        {
            Destroy(DeathObjs.transform.GetChild(i).gameObject);
        }
    }

    public string GetTargetName(Transform _target)
    {
        Enemy ObjEnemy = _target.GetComponent<Enemy>();
        return ObjEnemy.Type.name;
    }
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
                LastTarget = target;
                MonsterList.Remove(obj);
                // 0개가 되면 우선 임시적으로 다시 생성해주고 있음.
                //GameObject empty = e_enemyPrefabDictionary["Dummy"];
                //MonsterList.Add(Instantiate(empty));
                // // StartCoroutine("RateTarget");
                //target = MonsterList[0].transform;
                target = null;
                ObjEnemy.StartCoroutine(ObjEnemy.OnRateDestory(DeathObjs));
                stageController.NextWave();
                return;
            }

            ObjEnemy.StartCoroutine(ObjEnemy.OnRateDestory(DeathObjs));
            //Destroy(obj);
            return;

        }
        ObjEnemy.StartCoroutine(ObjEnemy.OnRateDestory(DeathObjs));
        //Destroy(obj);
        MonsterList.Remove(obj);
    }


}
