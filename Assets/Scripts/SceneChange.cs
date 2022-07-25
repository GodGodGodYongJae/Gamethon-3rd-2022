using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneChange : MonoBehaviour
{
    // Start is called before the first frame update
    Button btn; 
    void Start()
    {
        btn = GetComponent<Button>();
        btn.onClick.AddListener(ScenesManager.Instance.OnTitleScene);
    }

}
