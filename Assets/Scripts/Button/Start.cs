using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class Start : MonoBehaviour
{
    [SerializeField] PlayableDirector m_director;
    [SerializeField] PlayableAsset m_startScript;

    public void SelectStart()
    {
        m_director.Play(m_startScript);
    }
    
}
