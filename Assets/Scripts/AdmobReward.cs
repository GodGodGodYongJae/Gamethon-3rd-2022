using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AdmobReward : MonoBehaviour
{

    public UnityEvent Action;
    // Start is called before the first frame update
   public void Respawn()
    {
        UIManager.Instance.CharaterRespwan();
    }
    
}
