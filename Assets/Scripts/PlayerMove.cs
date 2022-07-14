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

    private IEnumerator MoveToPosition(Vector3 target)
    {
        //Debug.Log("MoveToPosition");
        float t = 0;
        Vector3 start = transform.position;

        while (t <= 1)
        {
            t += Time.fixedDeltaTime / MoveSpeed;
            rb.MovePosition(Vector3.Lerp(start, target, t));

            yield return null;
        }
    }

    private void RayCastTest(Vector3 prevPos)
    {
        Vector3 currentPosition = transform.position;
        Vector3 direction = prevPos - currentPosition;
        Ray ray = new Ray(currentPosition, direction);
        RaycastHit hit;
        if (!Physics.Raycast(ray, out hit, direction.magnitude))
            StartCoroutine("MoveToPosition", prevPos);
        else
            StartCoroutine("MoveToPosition", hit.point);
    }
    public void Movement(bitFlags.PlayerMoveDirection pd,Transform target)
    {
        Vector3 tr = Vector3.zero;
        if (pd.HasFlag(bitFlags.PlayerMoveDirection.Left) || pd.HasFlag(bitFlags.PlayerMoveDirection.Right))
        {
            Vector3 dir = Vector3.zero;
            if (pd == bitFlags.PlayerMoveDirection.Left)
                dir = Vector3.up;
            else if(pd == bitFlags.PlayerMoveDirection.Right)
                dir = Vector3.down;

            float distance = Vector3.Distance(transform.position, target.position);
            curSpeed = orbitSpeed / Mathf.Sqrt(distance);

            Quaternion q = Quaternion.AngleAxis(curSpeed, dir);
             tr = q * (rb.transform.position - target.position) + target.position;
            //rb.MovePosition(q * (rb.transform.position - target.position) + target.position);
        }
        else if(pd.HasFlag(bitFlags.PlayerMoveDirection.Front) || pd.HasFlag(bitFlags.PlayerMoveDirection.Back))
        {
            if (pd == bitFlags.PlayerMoveDirection.Front)
                tr = transform.position + transform.forward * MoveSpeed;
             //rb.MovePosition(transform.position + transform.forward *  MoveSpeed);
            else if (pd == bitFlags.PlayerMoveDirection.Back)
                tr = transform.position - transform.forward * MoveSpeed;
            //rb.MovePosition(transform.position - transform.forward *  MoveSpeed);
          
        }
        //RayCastTest(tr);
        StartCoroutine("MoveToPosition", tr);


    }

}
