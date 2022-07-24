using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract class EnemyGenerator
{   
   public struct EnermyDesign
    {
        public string EnemyName;
        public Vector3 pos;
        public Vector3 Rotate;
    }

    protected List<EnermyDesign> enermies = new List<EnermyDesign>();

    List<EnermyDesign> Get()
    {
        return enermies;
    }

    public abstract void CreateEnemy();
  
}

