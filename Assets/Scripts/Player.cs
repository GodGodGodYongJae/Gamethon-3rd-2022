using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    [SerializeField]
    UnityEvent<Vector3,Transform> events;
    [SerializeField]
    GameObject target;
    float speed = 15.0f;

    // Start is called before the first frame update
    void Start()
    {
       
    }




    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.D))
            movement(Vector3.down, target.transform);
        else if (Input.GetKeyDown(KeyCode.A))
            movement(Vector3.up, target.transform);
        //else if (Input.GetKeyDown(KeyCode.W))
        //    movement(Vector3.right, target.transform,true);

        transform.LookAt(target.transform);
    }

    private void playerDir()
    {

        Vector3 TargetPos = new Vector3(
   transform.position.x + 0,
    transform.position.y + 3.14f,
   transform.position.z + -7
    );

 

        Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, TargetPos, Time.deltaTime * speed);
        Camera.main.transform.rotation = transform.rotation;

    }
    public void movement(Vector3 dir,Transform target)
    {
   
            events?.Invoke(dir,target);
    }    
}
