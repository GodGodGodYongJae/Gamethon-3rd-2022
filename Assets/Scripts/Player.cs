using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    [SerializeField]
    UnityEvent<bitFlags.PlayerMoveDirection,Transform> MoveEvents;
    [SerializeField]
    GameObject target;
    float speed = 30f;
    Rigidbody rb;


    bitFlags.PlayerMoveDirection PlayerDirection;
    
    // Start is called before the first frame update
    void Start()
    {
        PlayerDirection = bitFlags.PlayerMoveDirection.None;
        rb = GetComponent<Rigidbody>();
    }




    // Update is called once per frame
    private void FixedUpdate()
    {
        //if(PlayerDirection.HasFlag(bitFlags.PlayerMoveDirection.Left))
        //  {
        //      PlayerDirection &= ~bitFlags.PlayerMoveDirection.Left;
        //      Debug.Log("Left!");
        //  }
        //switch (PlayerDirection)
        //{
        //    case bitFlags.PlayerMoveDirection.Left:
        //        movement(Vector3.up, target.transform);
        //        PlayerDirection &= ~bitFlags.PlayerMoveDirection.Left;
        //        break;
        //    case bitFlags.PlayerMoveDirection.Right:
        //        movement(Vector3.down, target.transform);
        //        PlayerDirection &= ~bitFlags.PlayerMoveDirection.Right;
        //        break;
        //    case bitFlags.PlayerMoveDirection.Front:
        //        PlayerDirection &= ~bitFlags.PlayerMoveDirection.Front;
        //        break;
        //    case bitFlags.PlayerMoveDirection.Back:
        //        PlayerDirection &= ~bitFlags.PlayerMoveDirection.Back;
        //        break;
        //    default:
        //        break;
        //}
        if (PlayerDirection != bitFlags.PlayerMoveDirection.None)
        {
            movement(PlayerDirection, target.transform);
            PlayerDirection = bitFlags.PlayerMoveDirection.None;
        }
            
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.D))
            PlayerDirection = bitFlags.PlayerMoveDirection.Right;
        //movement(Vector3.down, target.transform);
        else if (Input.GetKeyDown(KeyCode.A))
            PlayerDirection = bitFlags.PlayerMoveDirection.Left;
        //movement(Vector3.up, target.transform);
        else if (Input.GetKeyDown(KeyCode.W))
            PlayerDirection = bitFlags.PlayerMoveDirection.Front;
            //    movement(Vector3.right, target.transform,true);
        else if (Input.GetKeyDown(KeyCode.S))
            PlayerDirection = bitFlags.PlayerMoveDirection.Back;
        transform.LookAt(target.transform);
    }

    public void movement(bitFlags.PlayerMoveDirection pd, Transform target)
    {

        MoveEvents?.Invoke(pd,target);
    }
}
