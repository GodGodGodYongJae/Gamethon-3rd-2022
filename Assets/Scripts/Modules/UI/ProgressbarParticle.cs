using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressbarParticle : MonoBehaviour
{

    [SerializeField]
    Slider slider;
    float tagetProgress = 0;

    public void IncrementProgress(float newProgress)
    {
        tagetProgress = slider.value + newProgress;
    }
}
