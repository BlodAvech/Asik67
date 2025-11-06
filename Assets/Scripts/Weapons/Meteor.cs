using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Meteor : MonoBehaviour
{
	private Rigidbody2D rb;
	[SerializeField] private float speed = 50f;
	[SerializeField] private float lifeTime = 3;
	[SerializeField] private float damage = 60f;
	void Start()
	{
		rb = GetComponent<Rigidbody2D>();
		rb.linearVelocity = transform.right * speed;

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