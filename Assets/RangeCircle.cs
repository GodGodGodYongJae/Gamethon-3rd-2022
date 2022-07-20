using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeCircle
{
    Transform user;
    public RangeCircle(Transform user,float angle,float distance)
    {
        this.user = user;
        anlgeRange = angle;
        this.distance = distance;
    }

    float anlgeRange;
    float distance;

    Vector3 direction;
    float dotValue = 0f;

    public bool isCollisition(Transform target)
    {
        dotValue = Mathf.Cos(Mathf.Deg2Rad * (anlgeRange / 2));
        direction = target.position - user.position;

        if(direction.magnitude < distance)
        {
            if (Vector3.Dot(direction.normalized, user.forward) > dotValue)
                return true;
            else
                return false;
        }
        return false;
    }



}
