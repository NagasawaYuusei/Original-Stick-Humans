using UnityEngine;

public class bowScript : MonoBehaviour
{
    [SerializeField] float m_bowspeed = 3f;
    [SerializeField] float m_bowlifeTime = 5f;

    void Start()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        GameObject player = GameObject.Find("Player");
        float m_h = player.transform.localScale.x;
        if (m_h > 0)
        {
            rb.velocity = Vector2.right * m_bowspeed;
        }
        else
        {
            rb.velocity = Vector2.left * m_bowspeed;
            this.transform.localScale = new Vector2(-1 * Mathf.Abs(this.transform.localScale.x), this.transform.localScale.y);
        }
            
        Destroy(this.gameObject, m_bowlifeTime);
    }
}


