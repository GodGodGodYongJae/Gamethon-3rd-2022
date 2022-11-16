using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
#if UNITY_EDITOR
using UnityEditor;
#endif

[CreateAssetMenu(menuName = "Item/ItemDataBase")]
public class ItemDataBase : ScriptableObject
{
    [SerializeField]
    private List<item> items;

    public IReadOnlyList<item> Items => items;

    public item FindItemBy(string itemcode) => items.FirstOrDefault(x => x.ItemCode == itemcode);
#if UNITY_EDITOR
    [ContextMenu("FindItems")]
    private void FindItems()
    {
        FindItemBy<item>();
    }

    private void FindItemBy<T>() where T : item
    {
        items = new List<item>();

        string[] guids = AssetDatabase.FindAssets($"t:{typeof(T)}");
        foreach (var guid in guids)
        {
            string assetParh = AssetDatabase.GUIDToAssetPath(guid);
            var item = AssetDatabase.LoadAssetAtPath<T>(assetParh);

            if (item.GetType() == typeof(T))
                items.Add(item);
            EditorUtility.SetDirty(this);
            AssetDatabase.SaveAssets();
        }
    }
#endif
}
