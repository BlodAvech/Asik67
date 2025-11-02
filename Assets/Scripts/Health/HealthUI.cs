using System;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
	[SerializeField] private Health health;
	[SerializeField] private Image healthBar;

	public void HealthChanged(HealthChangedEventArgs e)
	{
		healthBar.fillAmount = e.NewValue / e.MaxValue;
	}

	void Awake()
	{
		health.OnHealthChanged += HealthChanged;
	}

	void OnDestroy()
	{
		health.OnHealthChanged -= HealthChanged;
	}
}