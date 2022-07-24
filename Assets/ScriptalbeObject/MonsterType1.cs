using UnityEngine;

[CreateAssetMenu(fileName = "MonsterData", menuName = "Scriptable Objects/MonsterType01", order = int.MaxValue)]
public class MonsterType1 : ScriptableObject
{
    [SerializeField]
    private new string name;
    public string Name { get { return name; } }
    [SerializeField]
    private int hp;
    //체력
    public int Hp { get { return hp; } }
    [SerializeField]
    private int attackPoint;

    //공격력
    public int AttackPoint { get { return attackPoint; } }

    [SerializeField]
    private float attackDelay;

    //공격 후 대기시간
    public float AttackDelay{ get { return attackDelay; } }
    [SerializeField]
    private int defencePoint;
    //방어력
    public float DefencePoint { get { return defencePoint; } }
    [SerializeField]
    private float moveSpeed;
    //이동속도
    public float MoveSpeed { get { return moveSpeed; } }
    [SerializeField]
    private float findRange;

    //탐색 범위
    public float FindRange { get { return findRange; } }
    [SerializeField]
    private float attackFindRange;
    // 플레이어가 어느정도 위치에 있어야 공격할건지
    // ex ) 한 칸 앞에서 공격 1, 두 칸 뒤에서 공격 2.. 
    public float AttackFindRange { get { return attackFindRange; } }
    [SerializeField]
    //공격 각도 
    private float attackRange; 
    public float AttackRange { get { return attackRange; } }
    [SerializeField]
    private float attackDistance;
    // 공격 indicator 길이ㄴ
    public float AttackDistance {  get { return attackDistance; } }
    [SerializeField]
    private float stopDistance;
    public float StopDistance { get { return stopDistance; } }
}
