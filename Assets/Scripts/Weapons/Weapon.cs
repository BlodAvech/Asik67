using System;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public abstract class Weapon : MonoBehaviour
{
	protected Animator animator;
	[SerializeField] protected GameObject container;

	void Start()
	{
		animator = GetComponent<Animator>();
		container = GetComponentInParent<Hand>().gameObject;
	}
	
	public abstract void AttackPerformed();
	public virtual void AttackCanceled() { }
}