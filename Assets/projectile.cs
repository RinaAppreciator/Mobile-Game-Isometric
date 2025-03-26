using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class projectile : MonoBehaviour
{

    Rigidbody2D rigidbody2d;
    // Start is called before the first frame update
    void Awake()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
    }

    void Update()
    {

        if (transform.position.magnitude > 100.0f)
        {
            Destroy(gameObject);
        }


    }

    // Update is called once per frame
    public void Launch(Vector2 direction, float force)
    {
        rigidbody2d.AddForce(direction * force);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        EnemyController enemy = other.GetComponent<EnemyController>();
        if (enemy != null)
        {
            enemy.Fix();
        }

    }

    void OnCollisionEnter2D(Collision2D other)
    {
        Destroy(gameObject);
  }
}
