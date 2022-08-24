using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour
{
    public void OnEffectSound(string name)
    {
        SoundManager.Inst.PlaySFX(name);
    }
}
