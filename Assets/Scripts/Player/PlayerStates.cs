using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStates : MonoBehaviour
{

    GameObject m_Attackmode;
    GameObject m_Passivemode;
    GameObject m_Activemode;
    PlayerMode Attackscript;
    PlayerMode Passivescript;
    PlayerMode Activescript;
    public int m_attack = default;
    public int m_passive = default;
    public int m_active = default;
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        m_Attackmode = GameObject.Find("Atack Mode");
        m_Passivemode = GameObject.Find("Passive Mode");
        m_Activemode = GameObject.Find("Active Mode");

        Attackscript = m_Attackmode.GetComponent<PlayerMode>();
        Passivescript = m_Passivemode.GetComponent<PlayerMode>();
        Activescript = m_Activemode.GetComponent<PlayerMode>();
    }

    void Update()
    {
        m_attack = Attackscript.attackmode;
        m_passive = Passivescript.passivemode;
        m_active = Activescript.activemode;
        //Debug.Log("Attackmode"+ m_attack+ ",passivemode"+m_passive+ ",activemode"+m_active) ;
    }

}
