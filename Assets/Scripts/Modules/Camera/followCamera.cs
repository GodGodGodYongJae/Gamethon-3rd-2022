using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//test 
public class followCamera : MonoBehaviour
{
    [SerializeField]
     Transform target;           // target 

    public float dist = 10.0f;           // 거리
    public float height = 5.0f;         // 높이
    public float dampRotate = 5.0f;  //회전 속도

    private Transform tr;             // 카메라 
    private Vector3 velocity = Vector3.zero;
    public float smoothTime = 1.3F;
    private void Awake()
    {
        Application.targetFrameRate = 60;
    }
    private void Start()
    {
        tr = GetComponent<Transform>();
    }

    void LateUpdate()
    {

        float cur_Y_Angle = Mathf.LerpAngle(tr.eulerAngles.y, target.eulerAngles.y, dampRotate * Time.deltaTime);
        //Mathf.LerpAngle(float s, float e, flaot t) = t시간 동안 s부터 e까지 각도를 변환하는 것.

        Quaternion rot = Quaternion.Euler(0, cur_Y_Angle, 0);

        tr.position = Vector3.SmoothDamp(transform.position, target.position - (rot * Vector3.forward * dist) + (Vector3.up * height), ref velocity, smoothTime);



        //tr.position = target.position - (rot * Vector3.forward * dist) + (Vector3.up * height);
        //타겟 위치 - 카메라위치 = 카메라가 타겟 뒤로 가야 타겟이 보이겠죠?

        tr.LookAt(EnemyFactoryMethod.Instance?.target);
    }
}
