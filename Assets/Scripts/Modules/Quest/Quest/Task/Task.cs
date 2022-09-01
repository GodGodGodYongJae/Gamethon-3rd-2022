using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public enum TaskState 
{ 
    Inactive,
    Running,
    Complete
}

// Ui Update Code�� Event�� �����س����� Task�� ���¸� update���� ��� ������ �ʿ䰡 ������.
#region Events
public delegate void StateChangedHandler(Task task, TaskState currentState, TaskState prevState);
public delegate void SuccessChangeHandler(Task task, int currentSuccess, int prevSuccess);
#endregion


[CreateAssetMenu(menuName ="Quest/Task/Task",fileName ="Task_")]
public class Task : ScriptableObject
{
    [SerializeField]
    private Category category;

    [Header("Text")]
    [SerializeField]
    [Tooltip("����Ʈ �ڵ�")]
    private string codeName;
    [SerializeField]
    [Tooltip("����Ʈ ����")]
    private string description;

    [Header("Target")]
    [SerializeField]
    private TaskTarget[] targets;


    [Header("Action")]
    [SerializeField]
    [Tooltip("�׼ǽ���� ����Ǿ���� �����б�")]
    private TaskAction action;

    [Header("Setting")]
    [SerializeField]
    [Tooltip("�ʱ� ������ -- ���� �� ������ �����´ٴ���..")]
    private InitinalSuccessValue initalSuccessValue;
    [SerializeField]
    [Tooltip("�ʿ��� ���� Ƚ��")]
    private int needSuccessToComplete;
    [SerializeField]
    [Tooltip("Task�� ���������� ��� ����Ƚ���� ���� ���� ���ΰ�?")]
    // ������ 100���� ������ Task �ε�, Quest�� �Ϸ��ϱ� ���� 50���� ���� �� �� �����Ƿ�.
    private bool canReceiveReportsDuringCompletion;


    private TaskState state;
    private int currentSuccess;

    public string CodeName => codeName;
    public string Description => description;

    public event StateChangedHandler onStateChanged;
    public event SuccessChangeHandler onSuccessChanged;


    public TaskState State
    {
        get => state;
        set
        {
            var prevState = State;
            state = value;
            onStateChanged?.Invoke(this, state, prevState);
        }
    }

    public bool IsComplete => State == TaskState.Complete;
    // �� Task�� ���� Quest�� �������� �˱� ���� ����
    public Quest Owner { get; private set; }

    //Awake �Լ��� ����� Setup
    public void Setup(Quest owner)
    {
        Owner = owner;
    }
    public void Start()
    {
        State = TaskState.Running;
        if (initalSuccessValue)
            CurrentSuccess = initalSuccessValue.GetValue(this);
    }
    public void End()
    {
        onStateChanged = null;
        onSuccessChanged = null;

    }
    public int CurrentSuccess {
        get => currentSuccess;
        set
        {
            int prevSuccess = currentSuccess;
            currentSuccess = Mathf.Clamp(value, 0, needSuccessToComplete);
            if(currentSuccess != prevSuccess)
            {
                State = currentSuccess == needSuccessToComplete ? TaskState.Complete : TaskState.Running;
                onSuccessChanged?.Invoke(this, currentSuccess, prevSuccess);
            }
        }
    }

    public Category Category => category;
    public void ReceiveReport(int successCount)
    {
        // �� ���� ���ؼ� Current Success�� �Ѱ��ش�. 
        CurrentSuccess = action.Run(this, CurrentSuccess, successCount);
        QuestSystem.Instance.Save();
    }

    public void Complete()
    {
        CurrentSuccess = needSuccessToComplete;
    }

    // �ش� �׽�ũ�� ���� Ƚ���� ���� ���� ������� Ȯ���ϴ� �Լ� targets�ȿ� �ش��ϴ� �Լ��� �ִٸ� true �ƴ϶�� false�� ����ȯ��.
    public bool IsTarget(string category ,object target)
        => Category == category && 
        targets.Any(x => x.IsEqual(target))&&
        (!IsComplete || (IsComplete && canReceiveReportsDuringCompletion)); 
}
