using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Terrablade : Weapon
{
	[SerializeField] private GameObject terraBeamPrefab;
	[SerializeField] private float damage = 20f;

	[Header("Collider")]
	[SerializeField] private Transform center;
	[SerializeField] private Vector3 offset;
	[SerializeField] private float radius;
	[SerializeField] private LayerMask targetMask;
	[SerializeField] private float knockback = 20f;

	private bool canAttack = true;
	public override void AttackPerformed()
	{
		if (!canAttack) return;
		animator.SetTrigger("attack");
		canAttack = false;
	}

	public void OnAttack()
	{
		var beam = Instantiate(terraBeamPrefab, transform.position, container.transform.rotation);
		foreach(var targetHealth in GetHealthTargets())
		{
			targetHealth.FixedDamage(damage);
			if(targetHealth.TryGetComponent<Rigidbody2D>(out Rigidbody2D targetRb))
			{
				Vector2 dir = transform.right; 
				targetRb.AddForce(dir * knockback, ForceMode2D.Impulse);
			}
		}
	}

	public void ResetAttack() => canAttack = true;

	public List<Health> GetHealthTargets()
	{
		return Physics2D.OverlapCircleAll(center.position + offset, radius, targetMask)
		.Select(collider => collider.GetComponent<Health>())
		.Where(health => health != null)
		.ToList<Health>();
	}

	void OnDrawGizmos()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(center.position + offset, radius);
	}
}