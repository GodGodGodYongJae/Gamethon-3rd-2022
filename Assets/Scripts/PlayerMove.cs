using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    Transform transform;

    public float speed = 2000.5f;
    private void Start()
    {
        transform = GetComponent<Transform>();
    }
    public void MoveMent(Vector3 dir,Transform target)
    {
        transform.RotateAround(target.transform.position, dir, speed * Time.deltaTime);
        //Camera.main.transform.RotateAround(target.transform.position, dir, speed * Time.deltaTime);
    }
}
