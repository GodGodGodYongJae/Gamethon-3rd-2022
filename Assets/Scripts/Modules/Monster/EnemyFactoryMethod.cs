using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFactoryMethod : MonoBehaviour
{

    public SerializableDictionary<string, GameObject> monsterList;
    public List<Enemy> enermy;
    private void Start()
    {
        monsterList.OnBeforeSerialize();
    

    }

    // 이거 어차피 Serialzlied로 빼서 패턴을 컴포넌트화 시켜보자.
    //EnemyGenerator[] enemyGenerators = null;

    //private void Start()
    //{
    //    enemyGenerators = new EnemyGenerator[2];
    //    enemyGenerators[0] = new PattenGenerator_A();
    //    enemyGenerators[1] = new PattenGenerator_B();
    //}

    //public void DoMakeTypeA()
    //{
    //    enemyGenerators[0].CreateEnemy();
    //    List<Enemy> enemies = enemyGenerators[0].GetEnemies();
    //    foreach (Enemy enemy in enemies)
    //    {

    //    }
    //}

    //public void DoMakeTypeB()
    //{
    //    enemyGenerators[1].CreateEnemy();
    //    List<Enemy> enemies = enemyGenerators[1].GetEnemies();

    //}
}
