using UnityEngine;

[CreateAssetMenu(fileName ="MonsterData",menuName ="Scriptable Objects/MonsterType01",order = int.MaxValue)]
public class MonsterType1 : ScriptableObject
{
    [SerializeField]
    private new string name;
    public string Name { get { return name; } }
    [SerializeField]
    private int hp;
    public int Hp { get { return hp; } }
}
