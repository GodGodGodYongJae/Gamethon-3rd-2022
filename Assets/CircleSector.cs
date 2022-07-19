using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CircleSector : MonoBehaviour
{
    public Transform target;

    public float angleRange = 45f;
    public float distance = 5f;
    public bool isCollistion = false;

    Color _blue = new Color(0f, 0f, 1f, 0.2f);
    Color _red = new Color(1f, 0f, 0, 0.2f);

    Vector3 direction;
    float dotValue = 0f;

    private void Update()
    {
        if (EnemyFactoryMethod.Instance?.target)
            target = EnemyFactoryMethod.Instance.target.transform;
        dotValue = Mathf.Cos(Mathf.Deg2Rad * (angleRange / 2));
        direction = target.position - transform.position;
        if (direction.magnitude < distance)
        {
            if (Vector3.Dot(direction.normalized, transform.forward) > dotValue)
                isCollistion = true;
            else
                isCollistion = false;
        }
        else
            isCollistion = false;
    }

    private void OnDrawGizmos()
    {
        Handles.color = isCollistion ? _red : _blue;
        Handles.DrawSolidArc(transform.position, Vector3.up, transform.forward, angleRange/2,distance);
        Handles.DrawSolidArc(transform.position, Vector3.up, transform.forward, -angleRange / 2, distance);
    }
}
