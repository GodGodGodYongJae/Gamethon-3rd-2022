using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }
    public void Movement(bitFlags.PlayerMoveDirection pd, Transform target)
    {
        switch (pd)
        {
            case bitFlags.PlayerMoveDirection.Left:
                anim.SetInteger("Movement", 1);
                break;
            case bitFlags.PlayerMoveDirection.Right:
                anim.SetInteger("Movement", 2);
                break;
            case bitFlags.PlayerMoveDirection.Front:

                anim.SetInteger("Movement", 3);
                break;
            case bitFlags.PlayerMoveDirection.Back:
                //anim.Play("RollBackward", -1, 0f);
                anim.SetInteger("Movement", 4);
                break;
            default:
                break;
        }
    }
}