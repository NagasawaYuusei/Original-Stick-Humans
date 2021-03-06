using System.Collections;
using UnityEngine;

public class zimenEnemy : EnemyBase
{
    Vector3 m_enemyPosition;

    [SerializeField]float m_speed = 0f;
    [SerializeField]float m_time = 0f;

    //public AudioClip soundbow;
    //public AudioClip destroySound;

    void Start()
    {
        base.StartSet();
        m_enemyPosition = transform.position;
    }

    void Update()
    {
        if (m_sr.isVisible && !Player.IsStelth)
        {
            StartCoroutine(Enemy());
        }
    }

    IEnumerator Enemy()
    {
        yield return new WaitForSeconds(m_time);
        if(m_stop)
        {
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
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        base.Damage(collision);
    }
}