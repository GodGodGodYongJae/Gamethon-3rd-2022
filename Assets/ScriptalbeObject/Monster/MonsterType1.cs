using UnityEngine;

[CreateAssetMenu(fileName = "MonsterData", menuName = "Scriptable Objects/MonsterType01", order = int.MaxValue)]
public class MonsterType1 : ScriptableObject
{
    [SerializeField]
    private new string name;
    public string Name { get { return name; } }
    [SerializeField]
    private int hp;
    //ü��
    public int Hp { get { return hp; } }
    [SerializeField]
    private int attackPoint;

    //���ݷ�
    public int AttackPoint { get { return attackPoint; } }

    [SerializeField]
    private float attackDelay;

    //���� �� ���ð�
    public float AttackDelay{ get { return attackDelay; } }
    [SerializeField]
    private int defencePoint;
    //����
    public float DefencePoint { get { return defencePoint; } }
    [SerializeField]
    private float moveSpeed;
    //�̵��ӵ�
    public float MoveSpeed { get { return moveSpeed; } }
    [SerializeField]
    private float findRange;

    //Ž�� ����
    public float FindRange { get { return findRange; } }
    [SerializeField]
    private float attackFindRange;
    // �÷��̾ ������� ��ġ�� �־�� �����Ұ���
    // ex ) �� ĭ �տ��� ���� 1, �� ĭ �ڿ��� ���� 2.. 
    public float AttackFindRange { get { return attackFindRange; } }
    [SerializeField]
    //���� ���� 
    private float attackRange; 
    public float AttackRange { get { return attackRange; } }
    [SerializeField]
    private float attackDistance;
    // ���� indicator ���̤�
    public float AttackDistance {  get { return attackDistance; } }
    [SerializeField]
    private float stopDistance;
    public float StopDistance { get { return stopDistance; } }
}
