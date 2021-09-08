using UnityEngine;
using System.Collections;
public class bossEnemy : EnemyBase
{
    [SerializeField] float m_speed = 0f;
    [SerializeField] float m_time = 0f;

    Vector3 m_enemyPosition;

    [SerializeField]float timeOut;
    private float timeElapsed;
    [SerializeField] GameObject m_shot = default;

    //public AudioClip destroySound;

    //public AudioClip soundbow;
    //public AudioClip soundbeem;
    //public AudioClip soundboss;
    //AudioSource audioSource;

    bool isCalledOnce = false;

    void Start()
    {
        base.StartSet();
        m_enemyPosition = transform.position;
    }

    void Update()
    {
        if (m_sr.isVisible && m_srPlayer.color == m_player.Colors[0] || m_sr.isVisible && m_srPlayer.color == m_player.Colors[2])
        {
            StartCoroutine(Enemy());
            if (!isCalledOnce)
            {
                isCalledOnce = true;
                //audioSource.PlayOneShot(soundboss);
            }
        }
    }

    IEnumerator Enemy()
    {
        yield return new WaitForSeconds(m_time);
        m_playerPosition = m_playerOblect.transform.position;
        m_enemyPosition = transform.position;

        if (m_playerPosition.x > m_enemyPosition.x)
        {
            m_enemyPosition.x = m_enemyPosition.x + m_speed;
        }
        else if (m_playerPosition.x < m_enemyPosition.x)
        {
            m_enemyPosition.x = m_enemyPosition.x - m_speed;
        }

        transform.position = m_enemyPosition;

        Vector2 tmp = this.transform.position;

        timeElapsed += Time.deltaTime;

        if (timeElapsed >= timeOut)
        {
            if (m_playerPosition.x < m_enemyPosition.x)
            {
                Instantiate(m_shot, new Vector2(tmp.x - 10f, tmp.y + 5.5f), this.transform.rotation);
                Instantiate(m_shot, new Vector2(tmp.x - 11f, tmp.y + 1.8f), this.transform.rotation);
                Instantiate(m_shot, new Vector2(tmp.x - 10f, tmp.y - 2.71f), this.transform.rotation);
            }
            else if (m_playerPosition.x > m_enemyPosition.x)
            {
                Instantiate(m_shot, new Vector2(tmp.x + 10f, tmp.y + 5.5f), this.transform.rotation);
                Instantiate(m_shot, new Vector2(tmp.x + 11f, tmp.y + 1.8f), this.transform.rotation);
                Instantiate(m_shot, new Vector2(tmp.x + 10f, tmp.y - 2.71f), this.transform.rotation);
            }
            timeElapsed = 0.0f;
            //audioSource.PlayOneShot(soundbeem);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        base.BossDamage(collision);
    }

    
}