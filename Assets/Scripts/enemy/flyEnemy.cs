using UnityEngine;
using System.Collections;

public class flyEnemy : EnemyBase
{
    [SerializeField]float m_speed = 0f;
    [SerializeField]float m_time = 0f;

    AudioClip destroySound;
    AudioClip soundbow;

    void Start()
    {
         base.StartSet();
    }

    void Update()
    {
        if (m_sr.isVisible && m_srPlayer.color == m_player.Colors[0] || m_sr.isVisible && m_srPlayer.color == m_player.Colors[2])
        {
            StartCoroutine(Enemy());
        }
    }

    IEnumerator Enemy()
    {
        yield return new WaitForSeconds(m_time);

        m_playerPosition = m_playerOblect.transform.position;

        gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, m_playerPosition, m_speed);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        base.Damage(collision);
    }
}