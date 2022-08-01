using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackEffect : StateMachineBehaviour
{
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //Vector3 pos = EnemyFactoryMethod.Instance.player.transform.position + EnemyFactoryMethod.Instance.player.transform.forward * 1.0f;
        //Debug.Log(pos);
        //pos += new Vector3(0.051f, 0.314f, 0.381f);
        //Vector3 rot = new Vector3(220.250f, 55.7f, -59);

        EnemyFactoryMethod.Instance.player.effectParent.transform.localPosition = new Vector3(0.39f, 1.36f,  1.33f);
        EnemyFactoryMethod.Instance.player.effectParent.transform.localRotation = Quaternion.Euler(new Vector3(-53f, -88f, 81f));
        GameObject TextGameObj = ObjectPoolManager.Instance?.Get("test");
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
