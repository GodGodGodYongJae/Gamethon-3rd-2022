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
        else if (Input.GetKeyDown(KeyCode.A))
            PlayerDirection = bitFlags.PlayerMoveDirection.Left;
        else if (Input.GetKeyDown(KeyCode.W))
            PlayerDirection = bitFlags.PlayerMoveDirection.Front;
        else if (Input.GetKeyDown(KeyCode.S))
            PlayerDirection = bitFlags.PlayerMoveDirection.Back;
        transform.LookAt(target.transform);
    }

    public void movement(bitFlags.PlayerMoveDirection pd, Transform target)
    {

        MoveEvents?.Invoke(pd,target);
    }
}
