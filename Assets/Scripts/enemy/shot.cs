using UnityEngine;

public class shot : MonoBehaviour, IPause
{
    private GameObject playerObject;
    private Vector3 PlayerPosition;
    [SerializeField] float speed = 0f;
    [SerializeField] int enemyHP;
    bool m_stop;


    void Start()
    {
        playerObject = GameObject.FindWithTag("Player");
    }
    void Update()
    {
        if(m_stop)
        {
            PlayerPosition = playerObject.transform.position;

            gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, PlayerPosition, speed);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
        if (collision.gameObject.CompareTag("Attack"))
        {
            enemyHP -= 1;

            Destroy(collision.gameObject);

            if (enemyHP == 0)
            {
                Destroy(gameObject);

                // AudioSource.PlayClipAtPoint(destroySound, transform.position);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Attack"))
        {
            enemyHP -= 2;

            Destroy(other.gameObject);

            if (enemyHP <= 0)
            {
                Destroy(gameObject);

                // AudioSource.PlayClipAtPoint(destroySound, transform.position);
            }
        }
    }

    void IPause.Pause()
    {
        m_stop = false;
    }

    void IPause.Resume()
    {
        m_stop = true;
    }
}
