using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class longEnemy : MonoBehaviour
{
    private SpriteRenderer m_sr = null;
    private GameObject playerObject;
    private Vector3 PlayerPosition;
    private Vector3 EnemyPosition;
    public float time = 0f;

    public float timeOut;
    private float timeElapsed;
    [SerializeField] GameObject m_shot = default;

    SpriteRenderer playercoler;
    Player m_player;

    public int enemyHP;
    Slider slider;
    public int enemynum = 0;

    public AudioClip soundbow;
    public AudioClip destroySound;
    public AudioClip soundbeem;
    AudioSource audioSource;


    void Start()
    {
        m_sr = GetComponent<SpriteRenderer>();
        playerObject = GameObject.FindWithTag("Player");

        playercoler = playerObject.GetComponent<SpriteRenderer>();
        m_player = playerObject.GetComponent<Player>();

        PlayerPosition = playerObject.transform.position;
        EnemyPosition = transform.position;

        slider = GameObject.Find("EnemyHPSlider" + enemynum).GetComponent<Slider>();
        slider.maxValue = enemyHP;
        slider.value = enemyHP;

        audioSource = GetComponent<AudioSource>();
        timeElapsed = 100;
    }

    void Update()
    {
       

        if (m_sr.isVisible && playercoler.color == m_player.m_colors[0] || m_sr.isVisible && playercoler.color == m_player.m_colors[2])
        {
            Invoke("enemy", time);
        }

    }

    void enemy()
    {
        Vector2 tmp = this.transform.position;

        timeElapsed += Time.deltaTime;

        if (timeElapsed >= timeOut)
        {
            if (PlayerPosition.x < EnemyPosition.x)
            {
                Instantiate(m_shot, new Vector2(tmp.x - 3.5f, tmp.y + 4), this.transform.rotation);
            }
            else if (PlayerPosition.x > EnemyPosition.x)
            {
                Instantiate(m_shot, new Vector2(tmp.x + 3.5f, tmp.y + 4), this.transform.rotation);
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
            slider.value = enemyHP;
            audioSource.PlayOneShot(soundbow);
            Destroy(collision.gameObject);

            if (enemyHP <= 0)
            {
                Destroy(transform.root.gameObject);

                AudioSource.PlayClipAtPoint(destroySound, transform.position);
            }
        }

        if (collision.gameObject.CompareTag("Sword"))
        {
            enemyHP -= 1;
            slider.value = enemyHP;
            Destroy(collision.gameObject);

            if (enemyHP <= 0)
            {
                Destroy(transform.root.gameObject);

                AudioSource.PlayClipAtPoint(destroySound, transform.position);
            }
        }
    }
}