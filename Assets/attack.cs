using UnityEngine;
using System.Collections;

public class attack : StateMachineBehaviour
{

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        animator.SetBool("Sword", false);
    }
}
