using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        AddSkill();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void AddSkill()
    {
        gameObject.AddComponent<P_Skill_AttackSpeed>();
    }
}
