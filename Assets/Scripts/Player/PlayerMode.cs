using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMode : MonoBehaviour
{

    [SerializeField] GameObject ChangeButton;
    int m_attackmode = 0;
    int m_passivemode = 0;
    int m_activemode = 0;

    public int Attackmode
    {
        get
        {
            return m_attackmode;
        }

    }

    public int Passivemode
    {
        get
        {
            return m_passivemode;
        }

    }

    public int Activemode
    {
        get
        {
            return m_activemode;
        }

    }

    public void Attack()
    {
        if (m_attackmode == 0)
        {
            m_attackmode = 1;
            ChangeButton.GetComponentInChildren<Text>().text = "Bow";
        }
        else
        {
            m_attackmode = 0;
            ChangeButton.GetComponentInChildren<Text>().text = "Sword";
        }
    }

    public void Passive()
    {
        if (m_passivemode == 0)
        {
            m_passivemode = 1;
            ChangeButton.GetComponentInChildren<Text>().text = "Flow";
        }
        else if (m_passivemode == 1)
        {
            m_passivemode = 2;
            ChangeButton.GetComponentInChildren<Text>().text = "Health";
        }
        else if (m_passivemode == 2)
        {
            m_passivemode = 3;
            ChangeButton.GetComponentInChildren<Text>().text = "Speed";
        }
        else
        {
            m_passivemode = 0;
            ChangeButton.GetComponentInChildren<Text>().text = "Double";
        }
    }

    public void Active()
    {
        if (m_activemode == 0)
        {
            m_activemode = 1;
            ChangeButton.GetComponentInChildren<Text>().text = "Wall";
        }
        else if (m_activemode == 1)
        {
            m_activemode = 2;
            ChangeButton.GetComponentInChildren<Text>().text = "Healing";
        }
        else if (m_activemode == 2)
        {
            m_activemode = 3;
            ChangeButton.GetComponentInChildren<Text>().text = "Stealth";
        }
        //else if (m_activemode == 3)
        //{
        //    if(m_attackmode == 0)
        //    {
        //        m_activemode = 4;
        //        ChangeButton.GetComponentInChildren<Text>().text = "Beam";
        //    }
        //    else if(m_attackmode == 1)
        //    {
        //        m_activemode = 5;
        //        ChangeButton.GetComponentInChildren<Text>().text = "Kick";
        //    }
        //}
        else if (m_activemode == 3)
        {
            if (m_attackmode == 0)
            {
                m_activemode = 4;
                ChangeButton.GetComponentInChildren<Text>().text = "Beam";
            }
        }
        else if (m_activemode == 3)
        {
            if (m_attackmode == 1)
            {
                m_activemode = 5;
                ChangeButton.GetComponentInChildren<Text>().text = "Kick";
            }
        }

        else if (m_activemode == 4 || m_activemode == 5)
        {
            m_activemode = 0;
            ChangeButton.GetComponentInChildren<Text>().text = "Blink";
        }
    }


}
