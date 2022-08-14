using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttacAnimSetBool : StateMachineBehaviour
{

    public float AttackStartTime;
    bool isAttack;
    public bool isDoubleAttack;
    // OnStateEnter is called before OnStateEnter is called on any state inside this state machine
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        animator.SetFloat("multiplier", animator.transform.GetComponent<Player>().atkSpeed); 
        animator.SetBool("isAttack", true);
        isAttack = false;
    }

    // OnStateUpdate is called before OnStateUpdate is called on any state inside this state machine
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= AttackStartTime && isAttack.Equals(false))
        {
            //Debug.Log(animator.GetCurrentAnimatorStateInfo(0).normalizedTime+"," + AttackStartTime);
            PlayerAttack pa = animator.transform.GetComponent<PlayerAttack>();
            pa.AttackAnimationSync();
            isAttack = true;
        }
    }



    // OnStateExit is called before OnStateExit is called on any state inside this state machine
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    animator.speed = speed;
    //}

    // OnStateMove is called before OnStateMove is called on any state inside this state machine
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateIK is called before OnStateIK is called on any state inside this state machine
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateMachineEnter is called when entering a state machine via its Entry Node
    //override public void OnStateMachineEnter(Animator animator, int stateMachinePathHash)
    //{
    //    
    //}

    // OnStateMachineExit is called when exiting a state machine via its Exit Node
    //override public void OnStateMachineExit(Animator animator, int stateMachinePathHash)
    //{
    //    animator.speed = speed;
    //}
}
