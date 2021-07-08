using UnityEngine;

public class bowScript : MonoBehaviour
{
    [SerializeField] float m_bowspeed = 3f;
    [SerializeField] float m_bowlifeTime = 5f;

    void Start()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.velocity = Vector2.right * m_bowspeed;
        Destroy(this.gameObject, m_bowlifeTime);
    }
}


