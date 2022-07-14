using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    Transform transform;
    Rigidbody rb;

    //전진 백 스피드
    public float MoveSpeed;
    //사이드 스피드
    public float orbitSpeed;
    public float curSpeed;
    //private float radius;

    private void Start()
    {

        transform = GetComponent<Transform>();
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
    }
    public void Movement(bitFlags.PlayerMoveDirection pd,Transform target)
    {
        if(pd.HasFlag(bitFlags.PlayerMoveDirection.Left) || pd.HasFlag(bitFlags.PlayerMoveDirection.Right))
        {
            Vector3 dir = Vector3.zero;
            if (pd == bitFlags.PlayerMoveDirection.Left)
                dir = Vector3.up;
            else if(pd == bitFlags.PlayerMoveDirection.Right)
                dir = Vector3.down;

            float distance = Vector3.Distance(transform.position, target.position);
            curSpeed = orbitSpeed / Mathf.Sqrt(distance);

            Quaternion q = Quaternion.AngleAxis(curSpeed, dir);
            rb.MovePosition(q * (rb.transform.position - target.position) + target.position);
        }
        else if(pd.HasFlag(bitFlags.PlayerMoveDirection.Front) || pd.HasFlag(bitFlags.PlayerMoveDirection.Back))
        {
            if (pd == bitFlags.PlayerMoveDirection.Front)
             rb.MovePosition(transform.position + transform.forward * Time.deltaTime * MoveSpeed);
            else if (pd == bitFlags.PlayerMoveDirection.Back)
            rb.MovePosition(transform.position - transform.forward * Time.deltaTime * MoveSpeed);
        }
        

    }

}
