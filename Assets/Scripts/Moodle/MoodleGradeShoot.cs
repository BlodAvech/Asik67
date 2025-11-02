using UnityEngine;

public class MoodleGradeShoot : StateMachineBehaviour
{
	[SerializeField] private float minTimer = 1f;
    [SerializeField] private float maxTimer = 2f;
    private Animator animator;
	private MoodleStateManager stateManager;

	[SerializeField] private GameObject projectile;
	[SerializeField] private float minTimeBtwShoots = 0f;
	[SerializeField] private float maxTimeBtwShoots = 2f;
	private float timeBtwShoot;

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
		if (timeBtwShoot <= 0) Shoot();
	}

	override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{

	}

	public void Shoot()
	{
		timeBtwShoot = Random.Range(minTimeBtwShoots, maxTimeBtwShoots); 


	}
}
