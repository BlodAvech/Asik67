using UnityEngine;

public class DummyHealth : Health
{
	void Awake()
	{
        OnHealthChanged += HealthChanged;
	}
    public void HealthChanged(HealthChangedEventArgs e)
    {
        if (e.NewValue <= 0) Die();
    }

    public override void Die()
    {
        GameObject.Destroy(gameObject);
    }

	void OnDestroy()
	{
		OnHealthChanged -= HealthChanged;
	}
}
