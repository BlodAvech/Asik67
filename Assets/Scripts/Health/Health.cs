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
		OnHealthChanged?.Invoke(new HealthChangedEventArgs(CurrentHealth, value, MaxHealth));
		CurrentHealth = value;
		if (value <= 0) Die();
	}
	
	public void FixedDamage(float damage) =>
		SetHealth(Math.Max(CurrentHealth - damage, 0f));

	public void FixedHeal(float heal) =>
		SetHealth(Math.Min(CurrentHealth + heal, MaxHealth));

	public void DamagePercentFromMax(float percent) =>
		SetHealth(Math.Max(CurrentHealth - MaxHealth * (percent / 100f), 0f));

	public void HealPercentFromMax(float percent) =>
		SetHealth(Math.Min(CurrentHealth + MaxHealth * (percent / 100f), MaxHealth));

	public void DamagePercentFromCurrent(float percent) =>
		SetHealth(Math.Max(CurrentHealth - CurrentHealth * (percent / 100f), 0f));

	public void HealPercentFromCurrent(float percent) =>
		SetHealth(Math.Min(CurrentHealth + CurrentHealth * (percent / 100f), MaxHealth));

	public void DamagePercentFromMissing(float percent)
	{
		float missing = MaxHealth - CurrentHealth;
		SetHealth(Math.Max(CurrentHealth - missing * (percent / 100f), 0f));
	}

	public void HealPercentFromMissing(float percent)
	{
		float missing = MaxHealth - CurrentHealth;
		SetHealth(Math.Min(CurrentHealth + missing * (percent / 100f), MaxHealth));
	}

	public virtual void Die()
	{
		GameObject.Destroy(gameObject);
	}
}