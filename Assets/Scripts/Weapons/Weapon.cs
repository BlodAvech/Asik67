using System;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public abstract class Weapon : MonoBehaviour
{
	protected Animator animator;
	protected GameObject container;

	public bool IsAttack { get; private set; }

	void Awake()
	{
		animator = GetComponent<Animator>();
	}
	
	public virtual void AttackPerformed()
	{
		IsAttack = true;
	}
	public virtual void AttackCanceled()
	{
		IsAttack = false;
	}

	public void SetContainer(GameObject container)
	{
		this.container = container;
	}
}