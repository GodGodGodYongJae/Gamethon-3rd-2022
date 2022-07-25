using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class yAxis5 : MonoBehaviour
{
    CinemachineVirtualCamera vcam;
    public Vector3 targetPos;
    void Start()
    {
        vcam = GetComponent<CinemachineVirtualCamera>();
        var transposer = vcam.GetCinemachineComponent<CinemachineTransposer>();
        StartCoroutine(YAxisPlus(transposer, targetPos));
    }

    IEnumerator YAxisPlus(CinemachineTransposer trans,Vector3 target)
    {
        float t = 0;
        Vector3 Start = trans.m_FollowOffset;
        while (t <= 1)
        {
            t += Time.deltaTime * 1.0f;
            trans.m_FollowOffset = Vector3.Lerp(Start, target, t);

            yield return null;
        }
    }

}
