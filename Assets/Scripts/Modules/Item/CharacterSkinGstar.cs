using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSkinGstar : MonoBehaviour
{
    public Material[] met = new Material[2];

    private void Start()
    {
        if (PlayFabData.Instance.isSkin)
            gameObject.GetComponent<SkinnedMeshRenderer>().material = met[0];
    }

    public void ChangeSkin()
    {
        if (PlayFabData.Instance.isSkin)
            gameObject.GetComponent<SkinnedMeshRenderer>().material = met[0];
    }
}
