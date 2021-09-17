using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

[RequireComponent(typeof(Collider2D))]

public abstract class EnemyBase : MonoBehaviour
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
    }

    public virtual void Damage(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("Sword"))
        {
            m_enemyHp -= 1;
            m_slider.value = m_enemyHp;

            if (m_enemyHp <= 0)
            {
                Destroy(transform.root.gameObject);
            }
        }
            

        if (collision.gameObject.CompareTag("Player"))
        {
            m_enemyHp -= 2;
            m_slider.value = m_enemyHp;

            if (m_enemyHp <= 0)
            {
                Destroy(transform.root.gameObject);
            }
        }
    }

    public virtual void BossDamage(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Bow"))
        {
            m_enemyHp -= 2;
            m_slider.value = m_enemyHp;
            Destroy(collision.gameObject);

            if (m_enemyHp <= 0)
            {
                this.gameObject.SetActive(false);
                StartCoroutine(Gameclear());
            }
        }

        if (collision.gameObject.CompareTag("Player"))
        {
            m_enemyHp -= 1;
            m_slider.value = m_enemyHp;

            if (m_enemyHp <= 0)
            {
                this.gameObject.SetActive(false);
                StartCoroutine(Gameclear());
            }
        }
    }

    IEnumerator Gameclear()
    {
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene("Gameclear");
    }


}
