using UnityEngine;

public class bowScript : MonoBehaviour, IPause
{
    [SerializeField] float m_bowspeed = 3f;
    [SerializeField] float m_bowlifeTime = 5f;
    Rigidbody2D m_rb;
    Vector2 m_velocity;

    void Start()
    {
        m_rb = GetComponent<Rigidbody2D>();
        GameObject player = GameObject.Find("Player");
        float m_h = player.transform.localScale.x;
        if (m_h > 0)
        {
            m_rb.velocity = Vector2.right * m_bowspeed;
        }
        else
        {
            m_rb.velocity = Vector2.left * m_bowspeed;
            this.transform.localScale = new Vector2(-1 * Mathf.Abs(this.transform.localScale.x), this.transform.localScale.y);
        }
            
        Destroy(this.gameObject, m_bowlifeTime);
    }

    void IPause.Pause()
    {
        m_velocity = m_rb.velocity;
        m_rb.Sleep();
        m_rb.simulated = false;
    }

    void IPause.Resume()
    {
        m_rb.simulated = true;
        m_rb.WakeUp();
        m_rb.velocity = m_velocity;
    }
}


