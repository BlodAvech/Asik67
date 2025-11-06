using System.Collections;
using UnityEngine;

public class MeteorStaff : Weapon
{
    private bool isShooting;
    private static readonly int IsShooting = Animator.StringToHash("IsShooting");

    private Coroutine shootingCoroutine = null;

    [SerializeField] private float timeBtwShoots = 1f;
    [SerializeField] private float spawnOffset = 5f;

    [SerializeField] private GameObject projectilePrefab;
    
    public override void AttackPerformed()
	{
		base.AttackPerformed();
        isShooting = true;
        animator.SetBool(IsShooting, isShooting);
        shootingCoroutine = StartCoroutine(Shoot());
    }

    public override void AttackCanceled()
	{
		base.AttackCanceled();
        isShooting = false;
        animator.SetBool(IsShooting, isShooting);
        if (shootingCoroutine != null) StopCoroutine(shootingCoroutine);
    }
    
    private IEnumerator Shoot()
    {
        while(true)
		{
			Vector3 worldPos = Camera.main.ScreenToWorldPoint(
				new Vector3(Input.mousePosition.x, Screen.height, Camera.main.nearClipPlane)
			);

			float xOffset = Random.Range(-spawnOffset, spawnOffset);
			Vector2 spawnPos = new Vector2(worldPos.x + xOffset, worldPos.y);

			Vector2 targetPos = new Vector2(worldPos.x, worldPos.y - 1f);
			Vector2 direction = (targetPos - spawnPos).normalized;

			float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

			var projectileObj = Instantiate(
				projectilePrefab,
				spawnPos,
				Quaternion.Euler(0, 0, angle)
			);

			yield return new WaitForSeconds(timeBtwShoots);
		}
	}
}