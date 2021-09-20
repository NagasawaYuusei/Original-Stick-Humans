using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    float m_h;//水平横
    [SerializeField] float m_speed;//スピード
    [SerializeField] int m_Jumpryoku = 15;//ジャンプ力
    [SerializeField] float m_stepPower;
    [SerializeField] float m_settiLength = 0.5f;//ジャンプ判定の長さ
    [SerializeField] float m_bkLength = 5f;
    [SerializeField] LayerMask m_kabe = default;
    [SerializeField] bool m_flipX = false;
    Rigidbody2D m_rb = default;
    SpriteRenderer m_sp = default;
    int m_jc = 0;
    //bool m_swRay = false;
    float m_scaleX;
    float m_timeElapsed;
    float m_timeElapsed2;
    float m_nowStepTime;
    [SerializeField] float m_stepTime;
    [SerializeField] int m_bktime;
    [SerializeField] int m_walltime;
    [SerializeField] int m_healtime;
    [SerializeField] int m_stelthtime;
    [SerializeField] float m_bowtime;
    [SerializeField] float m_swordtime;
    [SerializeField] GameObject m_muzzle = default;

    [SerializeField] int m_playerhp = 1;
    [SerializeField] Slider m_playerhpslider;

    [SerializeField] GameObject m_bow = default;

    [SerializeField] GameObject m_wall = default;
    [SerializeField] Color[] m_colors = default;
    [SerializeField] float m_toumeitime = 0f;

    [SerializeField] int s_attack = default;
    [SerializeField] int s_passive = default;
    [SerializeField] int s_active = default;

    Vector2 tmp;

    Animator m_anim = null;

    [SerializeField] float m_gravityDrag = 0.8f;
    bool m_isGround;
    bool m_isRun;
    bool m_isStep;
    [SerializeField] GameObject m_swordCollider;

    //[SerializeField] bool m_swordRay;
    //[SerializeField] RaycastHit2D m_hit2d;

    public int Active
    {
        get
        {
            return s_active;
        }
    }

    public Color[] Colors
    {
        get
        {
            return m_colors;
        }
    }

    public int BkTime
    {
        get
        {
            return m_bktime;
        }
    }

    public int WlTime
    {
        get
        {
            return m_walltime;
        }
    }
    public int HlTime
    {
        get
        {
            return m_healtime;
        }
    }

    public int SlTime
    {
        get
        {
            return m_stelthtime;
        }
    }

    public float TimeSkill
    {
        get
        {
            return m_timeElapsed;
        }
    }


    void Start()
    {
        m_rb = GetComponent<Rigidbody2D>();

        m_sp = GetComponent<SpriteRenderer>();
        m_sp.color = m_colors[0];

        m_anim = GetComponent<Animator>();

        m_playerhpslider = GameObject.Find("playerHPSlider").GetComponent<Slider>();
        m_playerhpslider.maxValue = m_playerhp;
        m_playerhpslider.value = m_playerhp;

        s_attack = PlayerStates.AttackStates;
        s_passive = PlayerStates.PassiveStates;
        s_active = PlayerStates.ActiveStates;

        if (s_passive == 2)
        {
            m_playerhp = m_playerhp * 2;
        }

        m_timeElapsed = 50;
        m_timeElapsed2 = 50;
        m_nowStepTime = m_stepTime;

        Debug.Log(s_attack + "," + s_passive + "," + s_active);
    }

    void Update()
    {
        PlayerNow();

        Jump();

        Fire1();

        Fire2();

        Step();

        Death();
    }

    void FixedUpdate()
    {
        idou();
    }

    ///<summary>プレイヤーのアップデートステータス</summary>///
    void PlayerNow()
    {
        m_h = Input.GetAxis("Horizontal");//移動入力

        tmp = this.transform.position;//自分の位置

        m_isGround = gd();

        if (m_flipX)//向き
        {
            FlipX(m_h);
        }

        if (m_anim)
        {
            m_anim.SetFloat("SpeedX", Mathf.Abs(m_rb.velocity.x));
            m_anim.SetBool("IsGround", m_isGround);
            m_anim.SetBool("Run", m_isRun);
            m_anim.SetBool("Step", m_isStep);
        }

        if (m_h == 0)
        {
            m_isRun = false;
        }
        else
        {
            m_isRun = true;
        }
    }
    ///<summary>移動処理</summary>///
    void idou()
    {
        if (s_passive == 3)
        {
            m_rb.velocity = new Vector2(m_speed * m_h * 2, m_rb.velocity.y);//移動
        }
        else
        {
            m_rb.velocity = new Vector2(m_speed * m_h, m_rb.velocity.y);//移動
        }
    }
    ///<summary>ジャンプ処理</summary>///
    void Jump()
    {
        Vector2 velocity = m_rb.velocity;
        if (Input.GetButtonDown("Jump"))//ジャンプ
        {
            if (s_passive == 0)
            {
                if (gd() || m_jc <= 0)
                {
                    m_jc++;
                    velocity.y = m_Jumpryoku;
                    m_isGround = false;
                }
            }
            else
            {
                if (gd())
                {
                    velocity.y = m_Jumpryoku;
                }
            }
        }
        else if (!Input.GetButton("Jump") && m_rb.velocity.y > 0)
        {
            // 上昇中にジャンプボタンを離したら上昇を減速する
            velocity.y *= m_gravityDrag;
        }

        if (gd())
        {
            m_jc = 0;
        }

        m_rb.velocity = velocity;

        if (Input.GetButton("Jump") && s_passive == 1)
        {
            m_rb.gravityScale = 1;
        }
    }
    ///<summary>攻撃処理</summary>///
    void Fire1()//攻撃処理
    {
        if (Input.GetButtonDown("Fire1") && s_attack == 0)
        {
            m_anim.SetBool("Sword", true);
        }

        if (Input.GetButtonUp("Fire1") && s_attack == 1)
        {
            if (m_timeElapsed2 >= m_bowtime)
            {
                Instantiate(m_bow, m_muzzle.transform.position, this.transform.rotation);
                m_timeElapsed2 = 0.0f;
            }
        }
    }

    ///<summary>アクティブスキル処理</summary>///
    void Fire2()
    {
        m_timeElapsed += Time.deltaTime;
        if (Input.GetButtonDown("Fire2"))
        {
            if (m_timeElapsed >= m_bktime)
            {
                if (s_active == 0)
                {
                    if (bk() == false && m_scaleX > 0)
                    {
                        m_rb.MovePosition(new Vector2(tmp.x + 20, tmp.y));
                    }

                    if (bk2() == false && m_scaleX < 0)
                    {
                        m_rb.MovePosition(new Vector2(tmp.x + -20, tmp.y));
                    }
                    m_timeElapsed = 0.0f;
                }
            }

            if (m_timeElapsed >= m_walltime)
            {
                if (s_active == 1)
                {
                    if (m_scaleX > 0 && gd())
                    {
                        Instantiate(m_wall, new Vector2(tmp.x + 10, tmp.y - 10), this.transform.rotation);
                    }

                    if (m_scaleX < 0 && gd())
                    {
                        Instantiate(m_wall, new Vector2(tmp.x - 10, tmp.y - 10), this.transform.rotation);
                    }
                    m_timeElapsed = 0.0f;
                }
            }

            if (m_timeElapsed >= m_healtime)
            {
                if (s_active == 2)
                {
                    m_playerhp += 3;
                    m_timeElapsed = 0.0f;
                }
            }

            if (m_timeElapsed >= m_stelthtime)
            {
                if (s_active == 3)
                {
                    m_sp.color = m_colors[1];
                    Invoke("backcolor", m_toumeitime);
                }
                m_timeElapsed = 0.0f;
            }
        }
    }

    void Step()
    {
        m_nowStepTime += Time.deltaTime;
        if (Input.GetButtonDown("Step") && m_nowStepTime > m_stepTime)
        {
            if (m_scaleX > 0)
            {
                m_rb.AddForce(Vector2.left * m_stepPower, ForceMode2D.Force);
                m_isStep = true;
                m_nowStepTime = 0;
            }
            else if (m_scaleX < 0)
            {
                m_rb.AddForce(Vector2.right * m_stepPower, ForceMode2D.Force);
                m_isStep = true;
                m_nowStepTime = 0;
            }
        }
        else
        {
            m_isStep = false;
        }
    }
    ///<summary>接触判定処理</summary>///
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("enemy"))
        {
            m_playerhp -= 1;
            damage();
            m_playerhpslider.value = m_playerhp;
            //AudioSource.PlayClipAtPoint(sounddamage, Camera.main.transform.position);
            FixedUpdate();

            if (m_playerhp <= 0)
            {
                //AudioSource.PlayClipAtPoint(sounddeth, Camera.main.transform.position);

                Invoke("Gameover", 1.5f);
                this.gameObject.SetActive(false);
            }
        }
    }

    ///<summary>死亡処理</summary>///
    void Death()
    {
        if (this.transform.position.y < -30f)
        {
            SceneManager.LoadScene("GameOver");
        }
    }

    ///<summary>ゲームオーバー処理</summary>///
    void Gameover()
    {
        SceneManager.LoadScene("GameOver");
    }

    ///<summary>ノックバック</summary>///
    void damage()
    {
        m_rb.AddForce(-transform.forward * 10000f * m_h, ForceMode2D.Impulse);
    }

    ///<summary>向きの処理</summary>///
    void FlipX(float horizontal)
    {
        m_scaleX = this.transform.localScale.x;

        if (horizontal > 0)
        {
            this.transform.localScale = new Vector2(Mathf.Abs(m_scaleX), this.transform.localScale.y);
        }
        else if (horizontal < 0)
        {
            this.transform.localScale = new Vector2(-1 * Mathf.Abs(m_scaleX), this.transform.localScale.y);
        }
    }

    ///<summary>接地判定</summary>///
    bool gd()
    {
        bool jumpray = Physics2D.Raycast(this.transform.position, Vector2.down, m_settiLength);
        Debug.DrawRay(this.transform.position, Vector2.down * m_settiLength);
        return jumpray;
    }

    ///<summary>ブリンク判定</summary>///
    bool bk()
    {
        bool bklay = Physics2D.Raycast(this.transform.position, Vector2.right, m_bkLength, m_kabe);
        return bklay;
    }

    ///<summary>ブリンク後ろ判定</summary>///
    bool bk2()
    {
        bool bklay = Physics2D.Raycast(this.transform.position, Vector2.left, m_bkLength, m_kabe);
        return bklay;
    }
}
