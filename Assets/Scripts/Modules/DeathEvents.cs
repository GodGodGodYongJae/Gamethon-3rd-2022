using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathEvents : MonoBehaviour
{
    private void OnEnable()
    {
        EnemyFactoryMethod.Instance.ShowDeathEnemy();
    }
    private void OnDisable()
    {
        EnemyFactoryMethod.Instance.EmptyDeathEnemy();
    }

}
