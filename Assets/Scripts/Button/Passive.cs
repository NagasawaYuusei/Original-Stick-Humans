using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class Passive : MonoBehaviour
{
    [SerializeField] PlayableDirector m_director;
    [SerializeField] PlayableAsset m_doubleScript;
    [SerializeField] PlayableAsset m_flowScript;
    [SerializeField] PlayableAsset m_healthScript;
    [SerializeField] PlayableAsset m_speedScript;
    public void SelectAttack()
    {
        if (PlayerStates.PassiveStates == 0)
        {
            m_director.Play(m_doubleScript);
        }
        else if(PlayerStates.PassiveStates == 1)
        {
            m_director.Play(m_flowScript);

        }
        else if(PlayerStates.PassiveStates == 2)
        {
            m_director.Play(m_healthScript);
        }
        else
        {
            m_director.Play(m_speedScript);
        }
    }
}
