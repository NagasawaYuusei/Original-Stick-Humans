using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStates : MonoBehaviour
{

    GameObject m_attackstates;
    GameObject m_passivestates;
    GameObject m_activestates;
    PlayerMode m_attackscript;
    PlayerMode m_passivescript;
    PlayerMode m_activescript;
    static int m_attack = default;
    static int m_passive = default;
    static int m_active = default;

    public static int AttackStates
    {
        get
        {
            return m_attack;
        }

    }

    public static int PassiveStates
    {
        get
        {
            return m_passive;
        }

    }

    public static int ActiveStates
    {
        get
        {
            return m_active;
        }
    }
    void Start()
    {
        m_attackstates = GameObject.Find("Atack Mode");
        m_passivestates = GameObject.Find("Passive Mode");
        m_activestates = GameObject.Find("Active Mode");

        m_attackscript = m_attackstates.GetComponent<PlayerMode>();
        m_passivescript = m_passivestates.GetComponent<PlayerMode>();
        m_activescript = m_activestates.GetComponent<PlayerMode>();
    }


    void Update()
    {
        m_attack = m_attackscript.Attackmode;
        m_passive = m_passivescript.Passivemode;
        m_active = m_activescript.Activemode;
        if (m_activescript.Activemode == 4 || m_activescript.Activemode == 5)
        {
            m_active = m_attackscript.Activemode;
        }

        Debug.Log("Attackmode"+ m_attack+ ",passivemode"+m_passive+ ",activemode"+m_active) ;
    }

}
