using System.Collections;
using UnityEngine;

public class longEnemy : EnemyBase
{
    private Vector3 m_enemyPosition;
    [SerializeField]float m_time = 0f;
    [SerializeField] GameObject m_shot = default;

    public float timeOut;
    private float timeElapsed;

    //public AudioClip soundbow;
    //public AudioClip destroySound;
    //public AudioClip soundbeem;
    //AudioSource audioSource;

    void Start()
    {
        base.StartSet();
        timeElapsed = 100;
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
            Vector2 tmp = this.transform.position;

            timeElapsed += Time.deltaTime;

            if (timeElapsed >= timeOut)
            {
                if (m_playerPosition.x < m_enemyPosition.x)
                {
                    Instantiate(m_shot, new Vector2(tmp.x - 3.5f, tmp.y + 4), this.transform.rotation);
                }
                else if (m_playerPosition.x > m_enemyPosition.x)
                {
                    Instantiate(m_shot, new Vector2(tmp.x + 3.5f, tmp.y + 4), this.transform.rotation);
                }

                timeElapsed = 0.0f;
                //audioSource.PlayOneShot(soundbeem);
            }
        }

      
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        base.Damage(collision);
    }
}