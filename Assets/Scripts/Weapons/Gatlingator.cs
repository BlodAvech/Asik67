using System.Collections;
using UnityEngine;

public class Gatlingator : Weapon
{
    private bool isShooting;
    private static readonly int IsShooting = Animator.StringToHash("IsShooting");

    private Coroutine shootingCoroutine = null;

    [SerializeField] private float minTimeBtwShoots = 0f;
    [SerializeField] private float maxTimeBtwShoots = .25f;
    [SerializeField] private float dispersion = 5f;

    [SerializeField] private GameObject bulletPrefab;
    
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
            var bulletObj = Instantiate(bulletPrefab , transform.position , Quaternion.Euler(0,0, transform.eulerAngles.z - 90f + Random.Range(-dispersion , dispersion)));
            yield return new WaitForSeconds(Random.Range(minTimeBtwShoots, maxTimeBtwShoots));
		}
	}
}
