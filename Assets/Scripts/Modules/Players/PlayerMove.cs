using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerMove : MonoBehaviour
{
    new Transform transform;
    Rigidbody rb;
    Transform target;

   
    //전진 백 스피드
    public float MoveSpeed;
    //사이드 스피드
    public float orbitSpeed;
    public float curSpeed;
    public float distance;
    //private float radius;

    //파티클 매니저 생기기전까진 임시적으로 Move 에서 관리
    private IEnumerator corutine;

    //이동 Late
    public float rollSpeed = 10f;


    private void Start()
    {
        transform = GetComponent<Transform>();
        rb = GetComponent<Rigidbody>();
        playerAnim  = GetComponent<PlayerAnimationController>();
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

        // Unit이 서로 낑겼을 때 Nan이 나오므로 예외처리 
        if (!float.IsNaN(target.x) && !float.IsNaN(target.y) && !float.IsNaN(target.z))
        {
            while (t <= 1)
            {
                t += Time.deltaTime * rollSpeed;
                rb.MovePosition(Vector3.Lerp(start, target, t));
                yield return null;
            }
            if(DashCheck == bitFlags.PlayerMoveDirection.Dash && isRayColision)
            {
                PlayerAttack pa = GetComponent<PlayerAttack>();
                pa.OnAttackEvent(this.target);
                isRayColision = false;
                DashCheck = bitFlags.PlayerMoveDirection.None;

            }
          
        }
    }
    bitFlags.PlayerMoveDirection DashCheck;
    bool isRayColision;
    PlayerAnimationController playerAnim;
    private void RayCastTest(Vector3 prevPos)
    {
        Vector3 currentPosition = transform.position;
        Vector3 direction = prevPos - currentPosition;
        Ray ray = new Ray(currentPosition, direction);
        RaycastHit hit;
        if (!Physics.Raycast(ray, out hit, direction.magnitude))
            corutine = MoveToPosition(prevPos);
        //StartCoroutine("MoveToPosition", prevPos);
        else
        {
            isRayColision = true;
            corutine = MoveToPosition(hit.point);
            playerAnim.isDashAtk();
        }
        //StartCoroutine("MoveToPosition", hit.point);
        StartCoroutine(corutine);
    }

    public void Jump()
    {
        //transform.position = new Vector3(transform.position.x, transform.position.y + 3, transform.position.z);
        Vector3 tr = Vector3.zero;
        tr = transform.position - transform.forward * 10;
        tr += transform.up * 5;
        RayCastTest(tr);
    }
    public void Movement(bitFlags.PlayerMoveDirection pd,Transform target)
    {
        Vector3 tr = Vector3.zero;
        this.target = target;
        //float distance = Vector3.Distance(transform.position, target.position);
        if (corutine != null) StopCoroutine(corutine);

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
        else if(pd.HasFlag(bitFlags.PlayerMoveDirection.Dash))
        {
             tr = transform.position + transform.forward * (MoveSpeed + 1.5f);
            DashCheck = bitFlags.PlayerMoveDirection.Dash;
        }

        if(pd != bitFlags.PlayerMoveDirection.None)
        RayCastTest(tr);
        //StartCoroutine("MoveToPosition", tr);


    }

}
