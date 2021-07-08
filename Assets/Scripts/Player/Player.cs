using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public float Speed;//スピード
    float m_h;//水平横
    [SerializeField] float m_Jumpryoku = 15f;//ジャンプ力
    [SerializeField] float settiLength = 0.5f;//ジャンプ判定の長さ
    [SerializeField] float bkLength = 5f;
    [SerializeField] LayerMask zimen = default;//地面判定
    [SerializeField] LayerMask kabe = default;
    Rigidbody2D m_rb = default;
    SpriteRenderer m_sp = default;
    int m_jc = 0;
    [SerializeField] bool m_flipX = false;
    float m_scaleX;
    public float timeElapsed;
    public float timeElapsed2;
    public int bktime;
    public int walltime;
    public int healtime;
    public int stelthtime;
    public float bowtime;
    public float swordtime;

    public int playerHP = 1;
    private Slider playerHPslider;

    [SerializeField] GameObject m_sword = default;
    [SerializeField] GameObject m_swordx = default;
    [SerializeField] GameObject m_bow = default;
    [SerializeField] GameObject m_bowx = default;

    [SerializeField] GameObject m_wall = default;
    public Color[] m_colors = default;
    public float toumeitime = 0f;


    GameObject m_mode;
    PlayerStates playerstate;
    int n_attack = default;
    int n_passive = default;
    public int n_active = default;

    //public AudioClip soundbow;
    //public AudioClip soundbk;
    //public AudioClip soundsword;
    //public AudioClip sounddamage;
    //public AudioClip sounddeth;
    //public AudioClip soundjump;
    //public AudioClip soundwall;
    //public AudioClip soundheal;
    //public AudioClip soundstelth;

    //AudioSource audioSource;

    //private Animator anim = null;

    void Start()
    {
        m_rb = GetComponent<Rigidbody2D>();
        m_sp = GetComponent<SpriteRenderer>();

        m_mode = GameObject.Find("Player States");
        playerstate = m_mode.GetComponent<PlayerStates>();

        m_sp.color = m_colors[0];

        playerHPslider = GameObject.Find("playerHPSlider").GetComponent<Slider>();
        playerHPslider.maxValue = playerHP;
        playerHPslider.value = playerHP;

        n_attack = playerstate.m_attack;
        n_passive = playerstate.m_passive;
        n_active = playerstate.m_active;

        if (n_passive == 2)
        {
            playerHP = playerHP * 2;
        }

        timeElapsed = 50;
        timeElapsed2 = 50;

        //audioSource = GetComponent<AudioSource>();

        //anim = GetComponent<Animator>();

    }

    void Update()
    {
        m_h = Input.GetAxis("Horizontal");//移動入力

        //if (m_h > 0)
        //{
        //    anim.SetBool("run", true);
        //}
        //else if (m_h < 0)
        //{
        //    anim.SetBool("run", true);
        //}
        //else
        //{
        //    anim.SetBool("run", false);
        //}

        Vector2 tmp = this.transform.position;//自分の位置

        if (m_flipX)//向き
        {
            FlipX(m_h);
        }

        timeElapsed += Time.deltaTime;//時間
        timeElapsed2 += Time.deltaTime;

        if (Input.GetButtonDown("Jump"))//ジャンプ
        {
            if (n_passive == 0)
            {
                if (gd() || m_jc <= 1)
                {
                    m_jc++;
                    m_rb.AddForce(Vector2.up.normalized * m_Jumpryoku, ForceMode2D.Impulse);
                    //audioSource.PlayOneShot(soundjump);
                    //anim.SetBool("jump", true);
                }
            }

            else
            {
                if (gd() == true)
                {
                    m_rb.AddForce(Vector2.up.normalized * m_Jumpryoku, ForceMode2D.Impulse);
                    //audioSource.PlayOneShot(soundjump);
                    //anim.SetBool("jump", true);
                }
            }

        }


        if (Input.GetButton("Jump") && n_passive == 1)
        {
            m_rb.gravityScale = 1;
        }


        if (this.transform.position.y < -30f)
        {
            SceneManager.LoadScene("GameOver");
        }


        if (Input.GetButtonDown("Fire1") && n_attack == 0)
        {

            if (timeElapsed2 >= swordtime)
            {
                if (m_scaleX > 0)
                {
                    Instantiate(m_sword, new Vector2(tmp.x + 2, tmp.y), this.transform.rotation);
                }
                else if (m_scaleX < 0)
                {
                    Instantiate(m_swordx, new Vector2(tmp.x - 2, tmp.y), this.transform.rotation);
                }
                timeElapsed = 0.0f;
                //audioSource.PlayOneShot(soundsword);
                //anim.SetBool("sword", true);
            }
            
        }
        //else
        //{
        //    anim.SetBool("sword", false);
        //}

        if (Input.GetButtonUp("Fire1") && n_attack == 1)
        {

            if (timeElapsed2 >= bowtime)
            {
                if (m_scaleX > 0)
                {
                    Instantiate(m_bow, new Vector2(tmp.x + 2, tmp.y + 0.5f), this.transform.rotation);
                }
                else if (m_scaleX < 0)
                {
                    Instantiate(m_bowx, new Vector2(tmp.x - 2, tmp.y + 0.5f), this.transform.rotation);
                }
                timeElapsed2 = 0.0f;
                //audioSource.PlayOneShot(soundbow);
                //anim.SetBool("bow", true);
            }

        }
        //else
        //{
        //    anim.SetBool("bow", false);
        //}

        if (Input.GetButtonDown("Fire2"))
        {
            if (timeElapsed >= bktime)
            {
                if (n_active == 0)
                {

                    if (bk() == false && m_scaleX > 0)
                    {

                        m_rb.MovePosition(new Vector2(tmp.x + 20, tmp.y));
                    }

                    if (bk2() == false && m_scaleX < 0)
                    {
                        m_rb.MovePosition(new Vector2(tmp.x + -20, tmp.y));
                    }
                    timeElapsed = 0.0f;
                    //audioSource.PlayOneShot(soundbk);
                }

            }

            if (timeElapsed >= walltime)
            {
                if (n_active == 1)
                {

                    if (m_scaleX > 0 && gd())
                    {
                        Instantiate(m_wall, new Vector2(tmp.x + 10, tmp.y - 10), this.transform.rotation);
                    }

                    if (m_scaleX < 0 && gd())
                    {
                        Instantiate(m_wall, new Vector2(tmp.x - 10, tmp.y - 10), this.transform.rotation);
                    }
                    timeElapsed = 0.0f;
                    //audioSource.PlayOneShot(soundwall);
                    //anim.SetBool("wall", true);
                }

            }

            if (timeElapsed >= healtime)
            {
                if (n_active == 2)
                {

                    playerHP += 3;
                    timeElapsed = 0.0f;
                    //audioSource.PlayOneShot(soundheal);
                }

            }

            if (timeElapsed >= stelthtime)
            {
                if (n_active == 3)
                {
                    m_sp.color = m_colors[1];
                    Invoke("backcolor", toumeitime);
                }
                timeElapsed = 0.0f;
                //audioSource.PlayOneShot(soundheal);

            }
        }
        //else
        //{
        //    anim.SetBool("wall", false);
        //}

        if (Input.GetButtonDown("Fire2"))
        { 

        }

    }

    void FixedUpdate()
    {
        if (n_passive == 3)
        {
            m_rb.AddForce(Vector2.right * m_h * Speed * 2, ForceMode2D.Impulse);//移動
        }

        else
        {
            m_rb.AddForce(Vector2.right * m_h * Speed, ForceMode2D.Impulse);//移動
        }



    }

    void FlipX(float horizontal)
    {
        m_scaleX = this.transform.localScale.x;

        if (horizontal > 0)
        {
            this.transform.localScale = new Vector2(Mathf.Abs(this.transform.localScale.x), this.transform.localScale.y);
        }
        else if (horizontal < 0)
        {
            this.transform.localScale = new Vector2(-1 * Mathf.Abs(this.transform.localScale.x), this.transform.localScale.y);
        }
    }

    public void backcolor()
    {
        m_sp.color = m_colors[0];
    }

    bool gd()//接地判定
    {
        bool jumpray = Physics2D.Raycast(this.transform.position, Vector2.down, settiLength, zimen);
        Debug.DrawRay(this.transform.position, Vector2.down * settiLength);
        if (jumpray) m_jc = 0;
        return jumpray;
    }

    bool bk()
    {
        bool bklay = Physics2D.Raycast(this.transform.position, Vector2.right, bkLength, kabe);
        return bklay;
    }

    bool bk2()
    {
        bool bklay = Physics2D.Raycast(this.transform.position, Vector2.left, bkLength, kabe);
        return bklay;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("enemy"))
        {
            playerHP -= 1;
            damage();
            playerHPslider.value = playerHP;
            //AudioSource.PlayClipAtPoint(sounddamage, Camera.main.transform.position);
            FixedUpdate();

            if (playerHP <= 0)
            {
                //AudioSource.PlayClipAtPoint(sounddeth, Camera.main.transform.position);

                Invoke("Gameover", 1.5f);
                this.gameObject.SetActive(false);
            }
        }
    }

    void Gameover()
    {
        SceneManager.LoadScene("GameOver");
        
    }

    void damage()
    {
        m_rb.AddForce(-transform.forward * 10000f * m_h, ForceMode2D.Impulse);
    }

}

