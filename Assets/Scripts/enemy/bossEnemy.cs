using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class bossEnemy : MonoBehaviour
{
    private SpriteRenderer m_sr = null;
    private GameObject playerObject;
    private Vector3 PlayerPosition;
    private Vector3 EnemyPosition;
    public float speed = 0f;
    public float time = 0f;

    public float timeOut;
    private float timeElapsed;
    [SerializeField] GameObject m_shot = default;
    [SerializeField] Slider m_slider;

    SpriteRenderer playercoler;
    Player m_player;

    public AudioClip destroySound;
    public int enemyHP;
    Slider slider;
    public int enemynum = 0;

    public AudioClip soundbow;
    public AudioClip soundbeem;
    public AudioClip soundboss;
    AudioSource audioSource;

    bool isCalledOnce = false;

    void Start()
    {
        m_sr = GetComponent<SpriteRenderer>();
        playerObject = GameObject.FindWithTag("Player");

        playercoler = playerObject.GetComponent<SpriteRenderer>();
        m_player = playerObject.GetComponent<Player>();

        PlayerPosition = playerObject.transform.position;
        EnemyPosition = transform.position;

        m_slider = GetComponent<Slider>();
        m_slider.maxValue = enemyHP;
        m_slider.value = enemyHP;

        audioSource = GetComponent<AudioSource>();


    }

    void Update()
    {

        if (m_sr.isVisible && playercoler.color == m_player.Colors[0] || m_sr.isVisible && playercoler.color == m_player.Colors[2])
        {
            Invoke("enemy", time);
            if (!isCalledOnce)
            {
                isCalledOnce = true;
                audioSource.PlayOneShot(soundboss);
            }
           
        }

    }

    void enemy()
    {
        PlayerPosition = playerObject.transform.position;
        EnemyPosition = transform.position;

        if (PlayerPosition.x > EnemyPosition.x)
        {
            EnemyPosition.x = EnemyPosition.x + speed;
        }
        else if (PlayerPosition.x < EnemyPosition.x)
        {
            EnemyPosition.x = EnemyPosition.x - speed;
        }

        transform.position = EnemyPosition;

        Vector2 tmp = this.transform.position;

        timeElapsed += Time.deltaTime;

        if (timeElapsed >= timeOut)
        {
            if (PlayerPosition.x < EnemyPosition.x)
            {
                Instantiate(m_shot, new Vector2(tmp.x - 10f, tmp.y + 5.5f), this.transform.rotation);
                Instantiate(m_shot, new Vector2(tmp.x - 11f, tmp.y + 1.8f), this.transform.rotation);
                Instantiate(m_shot, new Vector2(tmp.x - 10f, tmp.y - 2.71f), this.transform.rotation);
            }
            else if (PlayerPosition.x > EnemyPosition.x)
            {
                Instantiate(m_shot, new Vector2(tmp.x + 10f, tmp.y + 5.5f), this.transform.rotation);
                Instantiate(m_shot, new Vector2(tmp.x + 11f, tmp.y + 1.8f), this.transform.rotation);
                Instantiate(m_shot, new Vector2(tmp.x + 10f, tmp.y - 2.71f), this.transform.rotation);
            }
            timeElapsed = 0.0f;
            audioSource.PlayOneShot(soundbeem);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bow"))
        {
            enemyHP -= 2;
            m_slider.value = enemyHP;
            audioSource.PlayOneShot(soundbow);
            Destroy(collision.gameObject);

            if (enemyHP <= 0)
            {

                // AudioSource.PlayClipAtPoint(destroySound, transform.position);
                Invoke("Gameclear", 1.5f);
                this.gameObject.SetActive(false);
            }
        }

        if (collision.gameObject.CompareTag("Sword"))
        {
            enemyHP -= 1;
            m_slider.value = enemyHP;

            Destroy(collision.gameObject);

            if (enemyHP <= 0)
            {
                AudioSource.PlayClipAtPoint(destroySound, transform.position);
                Invoke("Gameclear", 1.5f);
                this.gameObject.SetActive(false);
            }
        }
    }

    void Gameclear()
    {
        SceneManager.LoadScene("Gameclear");
    }
}