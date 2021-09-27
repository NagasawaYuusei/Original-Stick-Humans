using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class Attack : MonoBehaviour
{
    [SerializeField] PlayableDirector m_director;
    [SerializeField] PlayableAsset m_swordScript;
    [SerializeField] PlayableAsset m_bowScript;

    public void SelectAttack()
    {
        if(PlayerStates.AttackStates == 0)
        {
            m_director.Play(m_swordScript);
        }
        else
        {
            m_director.Play(m_bowScript);
        }
    }
}
