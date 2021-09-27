using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

[RequireComponent(typeof(Collider2D))]

public abstract class EnemyBase : MonoBehaviour, IPause
{
    [SerializeField] GameObject m_enemy;
    [SerializeField] int m_enemyMaxHp = 0;
    int m_enemyHp = 0;
    protected SpriteRenderer m_sr;
    [SerializeField] GameObject m_objectSlider;
    Slider m_slider;
    AudioSource audioSource;

    protected GameObject m_playerOblect;
    protected Player m_player;
    protected SpriteRenderer m_srPlayer;
    protected Vector3 m_playerPosition;

    [SerializeField] AudioClip m_sound = default;

    Animator m_anim = null;
    Rigidbody2D m_rb;
    protected bool m_stop = true;

    public virtual void StartSet()
    {
        m_sr = GetComponent<SpriteRenderer>();

        m_playerOblect = GameObject.FindWithTag("Player");
        m_player = m_playerOblect.GetComponent<Player>();
        m_srPlayer = m_playerOblect.GetComponent<SpriteRenderer>();
        m_playerPosition = m_playerOblect.transform.position;

        m_slider = m_objectSlider.GetComponent<Slider>();
        m_slider.maxValue = m_enemyMaxHp;
        m_enemyHp = m_enemyMaxHp;
        m_slider.value = m_enemyHp;

        audioSource = GetComponent<AudioSource>();
        m_anim = GetComponent<Animator>();
        m_rb = GetComponent<Rigidbody2D>();
    }

    public virtual void Damage(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Sword"))
        {
            Attack(3);
        }

        if (collision.gameObject.CompareTag("Sword2"))
        {
            Attack(5);
        }

        if (collision.gameObject.CompareTag("Sword3"))
        {
            Attack(7);
        }

        if (collision.gameObject.CompareTag("Bow"))
        {
            Attack(5);
        }

        if (collision.gameObject.CompareTag("Bow2"))
        {
            Attack(7);
        }

        if (collision.gameObject.CompareTag("Bow3"))
        {
            Attack(10);
        }

        if (collision.gameObject.CompareTag("Abi"))
        {
            Attack(5);
        }

        Debug.Log(m_enemyHp);
    }

    public virtual void BossDamage(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Sword"))
        {
            BossAttack(3);
        }

        if (collision.gameObject.CompareTag("Sword2"))
        {
            BossAttack(5);
        }

        if (collision.gameObject.CompareTag("Sword3"))
        {
            BossAttack(7);
        }

        if (collision.gameObject.CompareTag("Bow"))
        {
            BossAttack(5);
        }

        if (collision.gameObject.CompareTag("Bow2"))
        {
            BossAttack(7);
        }

        if (collision.gameObject.CompareTag("Bow3"))
        {
            BossAttack(10);
        }

        if (collision.gameObject.CompareTag("Abi"))
        {
            BossAttack(5);
        }

        Debug.Log(m_enemyHp);
    }

    void Attack(int damage)
    {
        m_enemyHp -= damage;
        m_slider.value = m_enemyHp;

        if (m_enemyHp <= 0)
        {
            Destroy(gameObject);
        }
    }
    void BossAttack(int damage)
    {
        m_enemyHp -= damage;
        m_slider.value = m_enemyHp;

        if (m_enemyHp <= 0)
        {
            this.gameObject.SetActive(false);
            StartCoroutine(Gameclear());
        }
    }

    IEnumerator Gameclear()
    {
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene("Gameclear");
    }

    void IPause.Pause()
    {
        m_anim.speed = 0;
        m_stop = false;
        m_rb.Sleep();
    }

    void IPause.Resume()
    {
        m_anim.speed = 1;
        m_stop = true;
        m_rb.WakeUp();

    }
}
