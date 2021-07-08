using UnityEngine;

public class bowxScript : MonoBehaviour
{
    [SerializeField] float m_bowspeedx = 3f;
    [SerializeField] float m_bowlifeTimex = 5f;

    void Start()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.velocity = Vector2.left * m_bowspeedx;
        Destroy(this.gameObject, m_bowlifeTimex);
    }
}
