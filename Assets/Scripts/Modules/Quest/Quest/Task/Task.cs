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

// Ui Update Code를 Event에 연결해놓으면 Task의 상태를 update에서 계속 추적할 필요가 없어짐.
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
    [Tooltip("퀘스트 코드")]
    private string codeName;
    [SerializeField]
    [Tooltip("퀘스트 설명")]
    private string description;

    [Header("Target")]
    [SerializeField]
    private TaskTarget[] targets;


    [Header("Action")]
    [SerializeField]
    [Tooltip("액션실행시 실행되어야할 로직분기")]
    private TaskAction action;

    [Header("Setting")]
    [SerializeField]
    [Tooltip("초기 성공값 -- 현재 내 래밸을 가져온다던가..")]
    private InitinalSuccessValue initalSuccessValue;
    [SerializeField]
    [Tooltip("필요한 성공 횟수")]
    private int needSuccessToComplete;
    [SerializeField]
    [Tooltip("Task가 끝났음에도 계속 성공횟수를 보고 받을 것인가?")]
    // 아이템 100개를 모으는 Task 인데, Quest를 완료하기 전에 50개를 버릴 수 도 있으므로.
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
    // 이 Task를 가진 Quest가 누구인지 알기 위한 변수
    public Quest Owner { get; private set; }

    //Awake 함수를 대신할 Setup
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
        // 두 값을 더해서 Current Success에 넘겨준다. 
        CurrentSuccess = action.Run(this, CurrentSuccess, successCount);
        QuestSystem.Instance.Save();
    }

    public void Complete()
    {
        CurrentSuccess = needSuccessToComplete;
    }

    // 해당 테스크가 성공 횟수를 보고 받을 대상인지 확인하는 함수 targets안에 해당하는 함수가 있다면 true 아니라면 false를 반한환다.
    public bool IsTarget(string category ,object target)
        => Category == category && 
        targets.Any(x => x.IsEqual(target))&&
        (!IsComplete || (IsComplete && canReceiveReportsDuringCompletion)); 
}
