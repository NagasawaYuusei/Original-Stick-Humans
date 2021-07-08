using UnityEngine;

public class swordxScript : MonoBehaviour
{
    [SerializeField] float m_swordxlifeTime = 5f;

    void Start()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.velocity = Vector2.right;
        Destroy(this.gameObject, m_swordxlifeTime);
    }
}