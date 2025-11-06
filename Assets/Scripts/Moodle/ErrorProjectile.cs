using TMPro;
using Unity.Mathematics;
using UnityEngine;

public class ErrorProjectile : MonoBehaviour
{    
    private float damage;
    private Rigidbody2D rb;
    private float speed;
    private float lifetime;

    public void Initialize(float damage, float speed, float lifetime = 5f)
    {
        this.damage = damage;
        this.speed = speed;
        this.lifetime = lifetime;

        GameObject.Destroy(gameObject, lifetime);
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + (Vector2)rb.transform.right * speed * Time.fixedDeltaTime);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<Health>(out Health targetHealth))
        {
            targetHealth.FixedDamage(damage);
        }

        GameObject.Destroy(gameObject);
    }
}