using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerMove : MonoBehaviour
{
    Transform transform;
    Rigidbody rb;
    Transform target;
    public UnityEvent<Transform> AttackEvents;
    //전진 백 스피드
    public float MoveSpeed;
    //사이드 스피드
    public float orbitSpeed;
    public float curSpeed;
    public float attackDistance;
    public float distance;
    //private float radius;

    //이동 Late
    public float rollSpeed = 10f;

    public void OnAttackEvents()
    {
 
        AttackEvents?.Invoke(this.target);
    }
    private void Start()
    {

        transform = GetComponent<Transform>();
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        if(target != null)
        distance = Vector3.Distance(transform.position, target.position);
    }

    private IEnumerator MoveToPosition(Vector3 target)
    {
        //Debug.Log("MoveToPosition");
        float t = 0;
        Vector3 start = transform.position;

        while (t <= 1)
        {
            t += Time.deltaTime * rollSpeed;
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
        this.target = target;
        //float distance = Vector3.Distance(transform.position, target.position);
     

        if (pd.HasFlag(bitFlags.PlayerMoveDirection.Left) || pd.HasFlag(bitFlags.PlayerMoveDirection.Right))
        {
            Vector3 dir = Vector3.zero;
            if (pd == bitFlags.PlayerMoveDirection.Left)
                dir = Vector3.up;
            else if(pd == bitFlags.PlayerMoveDirection.Right)
                dir = Vector3.down;

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
        else if(pd.HasFlag(bitFlags.PlayerMoveDirection.Attack))
        {
            if(attackDistance < distance)
            {
                // 대쉬
                tr = transform.position + transform.forward * (MoveSpeed + 1.5f);
            }
            else
            {
                OnAttackEvents();
                return;
            }
        }

        if(pd != bitFlags.PlayerMoveDirection.None)
        RayCastTest(tr);
        //StartCoroutine("MoveToPosition", tr);


    }

}
