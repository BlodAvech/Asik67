using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : Health
{
	public override void Die()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
		base.Die();
	}
}