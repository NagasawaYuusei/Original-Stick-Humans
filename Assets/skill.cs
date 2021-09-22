using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class skill : StateMachineBehaviour
{
    [SerializeField] string m_skillName;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        animator.SetBool(m_skillName, false);
    }
}
