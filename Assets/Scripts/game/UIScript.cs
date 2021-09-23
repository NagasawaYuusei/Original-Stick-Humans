using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIScript : MonoBehaviour
{
    GameObject m_Player;
    Text m_text;
    Player playerstate;
    float n_timeElapsed;
    float n_bk;
    float n_wall;
    float n_heal;
    float n_stelth;
    float n_beam;
    float n_kick;

    float o_bk;
    float o_wall;
    float o_heal;
    float o_stelth;
    float o_beam;
    float o_kick;
    int o_active = default;

    [SerializeField] private GameObject pauseUI = default;
    Button button;

    // Update is called once per frame
    void Start()
    {
        m_text = GameObject.Find("cooltime").GetComponent<Text>();

        m_Player = GameObject.Find("Player");
        playerstate = m_Player.GetComponent<Player>();

        o_active = PlayerStates.ActiveStates;
    }

    // Update is called once per frame
    void Update()
    {
        n_bk = playerstate.BkTime;
        n_wall = playerstate.WlTime;
        n_heal = playerstate.HlTime;
        n_stelth = playerstate.SlTime;
        n_beam = playerstate.BMTime;
        n_kick = playerstate.KCTime;
        n_timeElapsed = playerstate.TimeSkill;

        o_bk = n_bk - n_timeElapsed;
        o_wall = n_wall - n_timeElapsed;
        o_heal = n_heal - n_timeElapsed;
        o_stelth = n_stelth - n_timeElapsed;
        o_beam = n_beam - n_timeElapsed;
        o_kick = n_kick - n_timeElapsed;

        if (o_active == 0)
        {
            if (n_timeElapsed >= n_bk)
            {
                m_text.text = "Blink!";
            }

            if (n_timeElapsed < n_bk)
            {
                m_text.text = "CT " + o_bk.ToString("n2");
            }
        }
        else if (o_active == 1)
        {
            if (n_timeElapsed >= n_wall)
            {
                m_text.text = "Wall!";
            }

            if (n_timeElapsed < n_wall)
            {
                m_text.text = "CT " + o_wall.ToString("n2");
            }
        }
        else if (o_active == 2)
        {
            if (n_timeElapsed >= n_heal)
            {
                m_text.text = "Healing!";
            }

            if (n_timeElapsed < n_heal)
            {
                m_text.text = "CT  " + o_heal.ToString("n2");
            }
        }
        else if (o_active == 3)
        {
            if (n_timeElapsed >= n_stelth)
            {
                m_text.text = "Stealth!";
            }

            if (n_timeElapsed < n_stelth)
            {
                if (Player.IsStelth)
                {
                    m_text.text = "StelthTime　" + (playerstate.MaxToumeiTime - Player.ToumeiTime).ToString("n2");
                }
                else
                {
                    m_text.text = "CT  " + o_stelth.ToString("n2");
                }
            }
        }
        else if (o_active == 4)
        {
            if (n_timeElapsed >= n_beam)
            {
                m_text.text = "Beam!";
            }

            if (n_timeElapsed < n_beam)
            {
                m_text.text = "CT  " + o_beam.ToString("n2");
            }
        }
        else if (o_active == 5)
        {
            if (n_timeElapsed >= n_kick)
            {
                m_text.text = "Kick!";
            }

            if (n_timeElapsed < n_kick)
            {
                m_text.text = "CT  " + o_kick.ToString("n2");
            }
        }

        if (Input.GetButtonDown("Fire3"))
        {
            pauseUI.SetActive(!pauseUI.activeSelf);

            if (pauseUI.activeSelf)
            {
                //Pauser.Pause(); ;
                button = GameObject.Find("Create").GetComponent<Button>();
                button.Select();
            }
            else
            {
                //Pauser.Resume();
            }


        }
    }
}
