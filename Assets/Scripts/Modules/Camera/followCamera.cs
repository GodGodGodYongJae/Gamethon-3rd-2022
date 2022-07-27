using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//test 
public class followCamera : MonoBehaviour
{
    [SerializeField]
     Transform target;           // target 

    public float dist = 10.0f;           // �Ÿ�
    public float height = 5.0f;         // ����
    public float dampRotate = 5.0f;  //ȸ�� �ӵ�

    private Transform tr;             // ī�޶� 
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
        //Mathf.LerpAngle(float s, float e, flaot t) = t�ð� ���� s���� e���� ������ ��ȯ�ϴ� ��.

        Quaternion rot = Quaternion.Euler(0, cur_Y_Angle, 0);

        tr.position = Vector3.SmoothDamp(transform.position, target.position - (rot * Vector3.forward * dist) + (Vector3.up * height), ref velocity, smoothTime);



        //tr.position = target.position - (rot * Vector3.forward * dist) + (Vector3.up * height);
        //Ÿ�� ��ġ - ī�޶���ġ = ī�޶� Ÿ�� �ڷ� ���� Ÿ���� ���̰���?

        tr.LookAt(EnemyFactoryMethod.Instance?.target);
    }
}
