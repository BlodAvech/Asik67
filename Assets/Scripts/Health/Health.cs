using System;
using UnityEngine;

public abstract class Health : MonoBehaviour
{
	[SerializeField] private float maxHealth = 100f;
	public float MaxHealth { get => maxHealth; private set => maxHealth = value; }
	public float CurrentHealth { get; private set; }

	public delegate void HealthChangedEventHandler(HealthChangedEventArgs e);
	public event HealthChangedEventHandler OnHealthChanged;

	void Start()
	{
		SetHealth(MaxHealth);
	}

	private void SetHealth(float value)
	{
		OnHealthChanged?.Invoke(new HealthChangedEventArgs(CurrentHealth , value , MaxHealth));
		CurrentHealth = value;
	}
	public void FixedDamage(float damage) => SetHealth(Math.Max(CurrentHealth - damage , 0f));
	public void FixedHeal(float heal) => SetHealth(Math.Min(CurrentHealth + heal, MaxHealth));

	public virtual void Die(){}
}