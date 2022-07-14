using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    Transform transform;
    Rigidbody rb;
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
    public void MoveMent(Vector3 dir,Transform target)
    {



        //float distance = (transform.position - target.position).magnitude;
        ////Debug.Log(distance);
        //radius = Vector3.Distance(transform.position, target.position);
        //curSpeed = orbitSpeed / Mathf.Sqrt(radius);
        ////transform.RotateAround(target.position, dir, curSpeed * Time.deltaTime * 180 / (2 * Mathf.PI * radius));

        float distance = Vector3.Distance(transform.position, target.position);
        curSpeed = orbitSpeed / Mathf.Sqrt(distance); 
        
        Quaternion q = Quaternion.AngleAxis(curSpeed, dir);
        rb.MovePosition(q * (rb.transform.position - target.position) + target.position);
        //rb.MoveRotation(rb.transform.rotation * q);


        ////transform.RotateAround(target.transform.position, dir, speed * Time.deltaTime);
        ////Camera.main.transform.RotateAround(target.transform.position, dir, speed * Time.deltaTime);
    }
}
