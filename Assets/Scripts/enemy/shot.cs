using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shot : MonoBehaviour
{
    private GameObject playerObject;
    private Vector3 PlayerPosition;
    public float speed = 0f;
    public int enemyHP;

    void Start()
    {
        playerObject = GameObject.FindWithTag("Player");
    }
    void Update()
    {
        PlayerPosition = playerObject.transform.position;

        gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, PlayerPosition, speed);
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
                Destroy(transform.root.gameObject);

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
                Destroy(transform.root.gameObject);

                // AudioSource.PlayClipAtPoint(destroySound, transform.position);
            }
        }
    }
}
