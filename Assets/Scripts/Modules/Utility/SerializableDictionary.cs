using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;


[System.Serializable]
public class SerializableDictionary<TKey, TValue> : Dictionary<TKey, TValue>, ISerializationCallbackReceiver
{
    [SerializeField]
    private List<TKey> keys = new List<TKey>();
    [SerializeField]
    private List<TValue> values = new List<TValue>();
    
   
    public void OnAfterDeserialize()
    {
        this.Clear();
        if(keys.Count != values.Count)
            throw new System.Exception(string.Format("역 직렬화 후 {0} 키와 {1} 값이 있습니다. 키 및 벨류 유형이 모두 직렬화 가능한지 확인해주세요."));
        for (int i = 0; i < keys.Count; i++)
            this.Add(keys[i], values[i]);
    }

    public void OnBeforeSerialize()
    {
        Clear();

        if (keys.Count != values.Count)
        {
            throw new SerializationException($"there are {keys.Count} keys and {values.Count} " +
                "values after deserialization. Make sure that both key and value types are serializable.");
        }

        for (var i = 0; i < keys.Count; i++)
        {
            Add(keys[i], values[i]);
        }
        //keys.Clear();
        //values.Clear();
        //foreach (KeyValuePair<TKey, TValue> pair in this)
        //{
        //    keys.Add(pair.Key);
        //    values.Add(pair.Value);
        //}
    }
}
