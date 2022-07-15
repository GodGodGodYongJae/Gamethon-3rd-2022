using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract class EnemyGenerator
{   
   public class EnermyDesign
    {
        public Enemy Enemy;
        public Vector3 pos;
        public Vector3 Rotate;
    }
    List<EnermyDesign> enermies = new List<EnermyDesign>();

    List<EnermyDesign> Get()
    {
        return enermies;
    }

    public abstract void CreateEnemy();
  
}

