using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Item/Item",fileName ="Item_")]
public class item : ScriptableObject
{
    [SerializeField]
    private string _itemCode;
    [SerializeField]
    private Sprite _image; 
    [SerializeField]
    [Header("Text")]
    [TextArea]
    private string _itemDisplayScription;
    [SerializeField]
    private ItemTask _task;
    [SerializeField]
    [Header("Options")]
    private bool _isConsumables;
    [SerializeField]
    private bool _isAuto;
    private string InstanceId;

    public string ItemCode => _itemCode;
    public bool isConsumables => _isConsumables;

    public Sprite Image => _image;
    public void ReportItem(string instanceId)
    {
        InstanceId = instanceId;
        if (_isAuto.Equals(true)) UseItem();
    }

    public void UseItem()
    {
        
        if (_isConsumables.Equals(true))
        {
            PlayFabData.Instance.ConsumeItem(()=> { _task.Run(); }, InstanceId, 1);
        }
        else _task.Run();
    }


}
