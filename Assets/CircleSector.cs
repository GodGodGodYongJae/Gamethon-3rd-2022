using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
public class CircleSector : MonoBehaviour
{
    public Transform target;

    public float angleRange = 160f;
    public float distance = 2f;
    public bool isCollistion = false;

    Color _blue = new Color(0f, 0f, 1f, 0.2f);
    Color _red = new Color(1f, 0f, 0, 0.2f);

    Vector3 direction;
    float dotValue = 0f;

    [Range(3, 256)]
    public int numSegments = 128;
    private void Start()
    {
        target = this.transform;
    }
    private void Update()
    {
        Vector3 interV = target.position - transform.position;

        // target�� �� ������ �Ÿ��� radius ���� �۴ٸ�
        if (interV.magnitude <= distance)
        {
            // 'Ÿ��-�� ����'�� '�� ���� ����'�� ����
            float dot = Vector3.Dot(interV.normalized, transform.forward);
            // �� ���� ��� ���� �����̹Ƿ� ���� ����� cos�� ���� ���ؼ� theta�� ����
            float theta = Mathf.Acos(dot);
            // angleRange�� ���ϱ� ���� degree�� ��ȯ
            float degree = Mathf.Rad2Deg * theta;

            // �þ߰� �Ǻ�
            if (degree <= angleRange / 2f)
                isCollistion = true;
            else
                isCollistion = false;

        }
        else
            isCollistion = false;
    }


  public bool isCollisition()
    {
        dotValue = Mathf.Cos(Mathf.Deg2Rad * (angleRange / 2));
        direction = target.position - transform.position;

        if (direction.magnitude < distance)
        {
            if (Vector3.Dot(direction.normalized, transform.forward) > dotValue)
            {
                isCollistion = true;
                return true;
            }
                
            else
            {
                isCollistion = false;
                return false;
            }
                
        }
        else
            return false;
    }
    public void DrawCircle(GameObject container)
    {

        var line = container.AddComponent<LineRenderer>();
        line.SetColors(_red, _red);
        line.useWorldSpace = false;
        //line.SetWidth(distance, distance);
        line.SetVertexCount(numSegments);
        line.useWorldSpace = false;

            float x;
            float y;
            float z = 0f;
             float angle = 20f;

            for (int i = 0; i < (numSegments + 1); i++)
            {
                x = Mathf.Sin(Mathf.Deg2Rad * angle) * 10;
                y = Mathf.Cos(Mathf.Deg2Rad * angle) * 10;

                line.SetPosition(i, new Vector3(x, y, z));

                angle += (360f / numSegments);
            }
        
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        
        Handles.color = isCollistion ? _red : _blue;

            Handles.DrawSolidArc(transform.position, Vector3.up, transform.forward, angleRange / 2, distance);
            Handles.DrawSolidArc(transform.position, Vector3.up, transform.forward, -angleRange / 2, distance);

      
    }
#endif
}
