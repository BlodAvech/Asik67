using UnityEngine;

public class MoodleErrorShoot : StateMachineBehaviour
{
    [SerializeField] private float minTimer = 1f;
    [SerializeField] private float maxTimer = 2f;
    private Animator animator;
    private Timer timer;
	private MoodleStateManager stateManager;

	[SerializeField] private GameObject projectilePrefab;
	[SerializeField] private float minTimeBtwShoots = 0f;
	[SerializeField] private float maxTimeBtwShoots = 2f;
	[SerializeField] private float minSpeed = 5f;
	[SerializeField] private float maxSpeed = 30f;
	[SerializeField] private float damage = 40.4f;
	private float timeBtwShoot;
	private bool isShooting = false;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		stateManager = animator.GetComponent<MoodleStateManager>();
		stateManager.StartStateSelection(minTimer, maxTimer, 0);

		this.animator = animator;

		timeBtwShoot = Random.Range(minTimeBtwShoots, maxTimeBtwShoots);
	}

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		timeBtwShoot -= Time.deltaTime;
		if (timeBtwShoot <= 0 && !isShooting) Shoot();
	}

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.GetComponent<Rigidbody2D>().transform.rotation = Quaternion.Euler(0,0,0);
        Debug.Log(animator.GetComponent<Rigidbody2D>().transform.rotation);
    }

	public void Shoot()
	{
		isShooting = true;
		timeBtwShoot = Random.Range(minTimeBtwShoots, maxTimeBtwShoots);

        ErrorProjectile projectile = new ErrorProjectileBuilder()
            .WithPrefab(projectilePrefab)
            .WithPosition(animator.transform.position)
            .WithDamage(damage)
            .WithSpeed(Random.Range(minSpeed, maxSpeed))
            .WithRotation(animator.transform.eulerAngles.z)
            .WithLifetime(3f)
            .Build();
		isShooting = false;
	}
}
