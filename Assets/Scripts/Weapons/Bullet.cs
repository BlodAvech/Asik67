using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Bullet : MonoBehaviour
{
	private Rigidbody2D rb;
	[SerializeField] private float speed = 50f;
	[SerializeField] private float lifeTime = .5f;
	[SerializeField] private float damage = 20f;
	void Start()
	{
		rb = GetComponent<Rigidbody2D>();
		rb.linearVelocity = transform.up * speed;

		GameObject.Destroy(gameObject, lifeTime);
	}


	void OnTriggerEnter2D(Collider2D collision)
	{
		if(collision.TryGetComponent<Health>(out Health targetHealth))
		{
			targetHealth.FixedDamage(damage);
		}
	}
}