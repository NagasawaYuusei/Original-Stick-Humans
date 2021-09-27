using UnityEngine;
using UnityEngine.UI;

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
    static int m_active = 4;
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

        if (m_attack == 0)
        {
            m_attackstates.GetComponentInChildren<Text>().text = "Sword";
        }
        else
        {
            m_attackstates.GetComponentInChildren<Text>().text = "Bow";
        }

        if (m_passive == 0)
        {
            m_passivestates.GetComponentInChildren<Text>().text = "Double";
        }
        else if (m_passive == 1)
        {
            m_passivestates.GetComponentInChildren<Text>().text = "Flow";
        }
        else if (m_passive == 2)
        {
            m_passivestates.GetComponentInChildren<Text>().text = "Health";
        }
        else
        {
            m_passivestates.GetComponentInChildren<Text>().text = "Speed";
        }

        if (m_active == 0)
        {
            m_activestates.GetComponentInChildren<Text>().text = "Blink";
        }
        else if (m_active == 1)
        {
            m_activestates.GetComponentInChildren<Text>().text = "Wall";
        }
        else if (m_active == 2)
        {
            m_activestates.GetComponentInChildren<Text>().text = "Healing";
        }
        else if (m_active == 3)
        {
            m_activestates.GetComponentInChildren<Text>().text = "Stealth";
        }
        else if (m_active == 4)
        {
            m_activestates.GetComponentInChildren<Text>().text = "Beam";
        }
        else
        {
            m_activestates.GetComponentInChildren<Text>().text = "Kick";
        }
    }


    void Update()
    {
        m_attack = m_attackscript.Attackmode;
        m_passive = m_passivescript.Passivemode;
        m_active = m_activescript.Activemode;

        int active = m_attackscript.Activemode;
        if(m_activescript.Activemode == 4 || m_activescript.Activemode == 5)
        {
            m_active = active;
        }
        
        Debug.Log("Attackmode"+ m_attack+ ",passivemode"+m_passive+ ",activemode"+m_active) ;
    }

}
