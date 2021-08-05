using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIScript : MonoBehaviour
{
    GameObject m_Player;
    Text m_text;
    GameObject panel;
    Player playerstate;
    float n_timeElapsed;
    float n_bk;
    float n_wall;
    float n_heal;
    float n_stelth;

    float o_bk;
    float o_wall;
    float o_heal;
    float o_stelth;
    public int o_active = default;

    [SerializeField] private GameObject pauseUI  = default;
    Button button;

    // Update is called once per frame
    void Start()
    {
        m_text = GameObject.Find("cooltime").GetComponent<Text>();
        panel = GameObject.Find("Panel");

        m_Player = GameObject.Find("Player");
        playerstate = m_Player.GetComponent<Player>();

        o_active = playerstate.Active;

       

    }

    // Update is called once per frame
    void Update()
    {
        n_bk = playerstate.BkTime;
        n_wall = playerstate.WlTime;
        n_heal = playerstate.HlTime;
        n_stelth = playerstate.SlTime;
        n_timeElapsed = playerstate.TimeSkill;

        o_bk = n_bk - n_timeElapsed;
        o_wall = n_wall - n_timeElapsed;
        o_heal = n_heal - n_timeElapsed;
        o_stelth = n_stelth - n_timeElapsed;

        if (o_active == 0)
        {
            if(n_timeElapsed >= n_bk)
            {
                m_text.text = "Blink!";
            }

            if (n_timeElapsed < n_bk)
            {
                m_text.text = "CT "+o_bk;
            }
        }

        if(o_active == 1)
        {
            if (n_timeElapsed >= n_wall)
            {
                m_text.text = "Wall!";
            }

            if (n_timeElapsed < n_wall)
            {
                m_text.text = "CT " + o_wall;
            }
        }

        if(o_active == 2)
        {
            if (n_timeElapsed >= n_heal)
            {
                m_text.text = "Healing!";
            }

            if (n_timeElapsed < n_heal)
            {
                m_text.text = "CT  " + o_heal;
            }
        }

        if(o_active == 3)
        {
            if (n_timeElapsed >= n_stelth)
            {
                m_text.text = "Stealth!";
            }

            if (n_timeElapsed < n_stelth)
            {
                m_text.text = "CT  " + o_stelth;
            }
        }

        if (Input.GetButtonDown("Fire3"))
        {
            pauseUI.SetActive(!pauseUI.activeSelf);

            if (pauseUI.activeSelf)
            {
                Time.timeScale = 0f;
                button = GameObject.Find("Create").GetComponent<Button>();
                button.Select();
            }
            else
            {
                Time.timeScale = 1f;
            }


        }
    }
}
