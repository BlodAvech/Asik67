using System;
using UnityEngine;

public class Terrabeam : MonoBehaviour
{
	[SerializeField] private float speed = 100f;
	[SerializeField] private float decceleration = 200f;
	[SerializeField] private float lifeTime = .5f;
	[SerializeField] private float damage = 20f;

	void Start()
	{
		GameObject.Destroy(gameObject, lifeTime);
	}
	private void FixedUpdate()
	{
		transform.position = transform.position + transform.right * speed * Time.fixedDeltaTime;
		speed = Math.Max(speed - decceleration * Time.fixedDeltaTime, 2f);
	}

	void OnTriggerEnter2D(Collider2D collision)
	{
		if(collision.TryGetComponent<Health>(out Health targetHealth))
		{
			targetHealth.FixedDamage(damage);
		}
	}
}