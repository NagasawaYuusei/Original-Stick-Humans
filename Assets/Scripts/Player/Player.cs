using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    float m_h;//水平横
    float m_v;
    [SerializeField] float m_speed;//スピード
    [SerializeField] int m_Jumpryoku = 15;//ジャンプ力
    [SerializeField] float m_settiLength = 0.5f;//ジャンプ判定の長さ
    [SerializeField] float m_bkLength = 5f;
    [SerializeField] LayerMask m_zimen = default;//地面判定
    [SerializeField] LayerMask m_kabe = default;
    [SerializeField] bool m_flipX = false;
    Rigidbody2D m_rb = default;
    SpriteRenderer m_sp = default;
    int m_jc = 0;
    float m_scaleX;
    float m_timeElapsed;
    float m_timeElapsed2;
    [SerializeField] int m_bktime;
    [SerializeField] int m_walltime;
    [SerializeField] int m_healtime;
    [SerializeField] int m_stelthtime;
    [SerializeField] float m_bowtime;
    [SerializeField] float m_swordtime;

    [SerializeField] int m_playerhp = 1;
    [SerializeField] Slider m_playerhpslider;

    [SerializeField] GameObject m_sword = default;
    [SerializeField] GameObject m_swordx = default;
    [SerializeField] GameObject m_bow = default;
    [SerializeField] GameObject m_bowx = default;

    [SerializeField] GameObject m_wall = default;
    [SerializeField] Color[] m_colors = default;
    [SerializeField] float m_toumeitime = 0f;

    int s_attack = default;
    int s_passive = default;
    int s_active = default;

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

    Animator anim = null;

    public int Active
    {
        get
        {
            return s_active;
        }

        private set
        {
            if (value > 0)
            {
                s_active = PlayerStates.AttackStates;
            }
            else
            {
                Debug.LogError("不正なスキルです");
            }
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

        private set
        {
            if (value > 0)
            {
                m_timeElapsed = value;
            }
            else
            {
                Debug.LogError("不正な時間です");
            }
        }

    }


    void Start()
    {
        m_rb = GetComponent<Rigidbody2D>();
        m_sp = GetComponent<SpriteRenderer>();

        m_sp.color = m_colors[0];

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

        //audioSource = GetComponent<AudioSource>();

        anim = GetComponent<Animator>();

        Debug.Log(s_attack + "," + s_passive + "," + s_active);

    }

    void Update()
    {
        Debug.Log(m_timeElapsed);
        m_h = Input.GetAxis("Horizontal");//移動入力
        m_v = Input.GetAxis("Vertical");

        Vector2 tmp = this.transform.position;//自分の位置

        if (m_flipX)//向き
        {
            FlipX(m_h);
        }

        m_timeElapsed += Time.deltaTime;//時間
        m_timeElapsed2 += Time.deltaTime;

        if (Input.GetButtonDown("Jump"))//ジャンプ
        {
            Debug.Log("a");
            if (s_passive == 0)
            {
                if (gd() || m_jc <= 1)
                {
                    Debug.Log("jump");
                    m_jc++;
                    m_rb.velocity = new Vector2(m_rb.velocity.x,m_Jumpryoku);
                    //m_rb.AddForce(Vector2.up * m_Jumpryoku, ForceMode2D.Impulse);

                }
            }

        }


        if (Input.GetButton("Jump") && s_passive == 1)
        {
            m_rb.gravityScale = 1;
        }


        if (this.transform.position.y < -30f)
        {
            SceneManager.LoadScene("GameOver");
        }


        if (Input.GetButtonDown("Fire1") && s_attack == 0)
        {

            if (m_timeElapsed2 >= m_swordtime)
            {
                if (m_scaleX > 0)
                {
                    Instantiate(m_sword, new Vector2(tmp.x + 2, tmp.y), this.transform.rotation);
                }
                else if (m_scaleX < 0)
                {
                    Instantiate(m_swordx, new Vector2(tmp.x - 2, tmp.y), this.transform.rotation);
                }
                m_timeElapsed = 0.0f;
                //audioSource.PlayOneShot(soundsword);
                //anim.SetBool("sword", true);
            }
            
        }
        //else
        //{
        //    anim.SetBool("sword", false);
        //}

        if (Input.GetButtonUp("Fire1") && s_attack == 1)
        {

            if (m_timeElapsed2 >= m_bowtime)
            {
                if (m_scaleX > 0)
                {
                    Instantiate(m_bow, new Vector2(tmp.x + 2, tmp.y + 0.5f), this.transform.rotation);
                }
                else if (m_scaleX < 0)
                {
                    Instantiate(m_bowx, new Vector2(tmp.x - 2, tmp.y + 0.5f), this.transform.rotation);
                }
                m_timeElapsed2 = 0.0f;
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
                    //audioSource.PlayOneShot(soundbk);
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
                    //audioSource.PlayOneShot(soundwall);
                    //anim.SetBool("wall", true);
                }

            }

            if (m_timeElapsed >= m_healtime)
            {
                if (s_active == 2)
                {

                    m_playerhp += 3;
                    m_timeElapsed = 0.0f;
                    //audioSource.PlayOneShot(soundheal);
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
        if (s_passive == 3)
        {
            m_rb.velocity = new Vector2(m_speed * m_h * 2, m_rb.velocity.y);//移動
            
        }

        else
        {
            m_rb.velocity = new Vector2(m_speed * m_h, m_rb.velocity.y);//移動
        }
        if(0 < m_h || m_h < 0)
        {
            anim.Play("Player_run");
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
            anim.Play("Player_run");
        }
    }

    public void backcolor()
    {
        m_sp.color = m_colors[0];
    }

    bool gd()//接地判定
    {
        bool jumpray = Physics2D.Raycast(this.transform.position, Vector2.down, m_settiLength);
        Debug.DrawRay(this.transform.position, Vector2.down * m_settiLength);
        if (jumpray) m_jc = 0;
        return jumpray;
    }

    bool bk()
    {
        bool bklay = Physics2D.Raycast(this.transform.position, Vector2.right, m_bkLength, m_kabe);
        return bklay;
    }

    bool bk2()
    {
        bool bklay = Physics2D.Raycast(this.transform.position, Vector2.left, m_bkLength, m_kabe);
        return bklay;
    }

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

    void Gameover()
    {
        SceneManager.LoadScene("GameOver");
        
    }

    void damage()
    {
        m_rb.AddForce(-transform.forward * 10000f * m_h, ForceMode2D.Impulse);
    }

}


public class SkillSettings : MonoBehaviour
{
    private Skill setedSkill;

    public SkillSettings(Skill setedSkill)
    {
        this.SetedSkill = setedSkill;
    }

    public Skill SetedSkill { get => setedSkill;private  set => setedSkill = value; }
}
public class player : MonoBehaviour
{
    private Skill m_Skill;
    public int HP { get;private set; }

    private void Start()
    {
        var skillSetting = GameObject.Find("").GetComponent<SkillSettings>();
        m_Skill = skillSetting.SetedSkill;
    }

    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            m_Skill.Activate(this);
        }
    }

    public void SetHealth(int health)
    {
        HP = health;
    }
}

public class HpUpSkill : Skill
{
    private float increaseRate = 1.5f;
    private int increasedHealth;
    public override void Activate(player player)
    {
        var currentHealth = player.HP;
        var newHealth = Mathf.RoundToInt(currentHealth * increaseRate);
        increasedHealth = newHealth - currentHealth;
        player.SetHealth(newHealth);
    }

    public override void Deactivate(player player)
    {
        // 増やしたHP分戻す処理
    }
}

public class DoubleJumpSkill : Skill
{

    public override void Activate(player player)
    {
        throw new System.NotImplementedException();
    }

    public override void Deactivate(player player)
    {
        throw new System.NotImplementedException();
    }
}

public abstract class Skill
{
    public abstract void Activate(player player);
    public abstract void Deactivate(player player);
}
