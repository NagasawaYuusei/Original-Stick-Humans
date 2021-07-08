using UnityEngine;

public class swordScript : MonoBehaviour
{
    [SerializeField] float m_swordlifeTime = 5f;

    void Start()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.velocity = Vector2.right;
        Destroy(this.gameObject, m_swordlifeTime);
    }
}

