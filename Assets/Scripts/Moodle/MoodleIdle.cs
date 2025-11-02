using UnityEngine;

public class MoodleIdle : StateMachineBehaviour
{
    [SerializeField] private float minTimer = 1f;
    [SerializeField] private float maxTimer = 2f;
    private Animator animator;
    private Timer timer;
	private MoodleStateManager stateManager;

	public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
		stateManager = animator.GetComponent<MoodleStateManager>();
        stateManager.StartStateSelection(minTimer, maxTimer, 1 ,2);
        
        this.animator = animator;
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
    }

	public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
	}

	public void SelectNextState()
	{
        int stateIndex = Random.Range(0, 2);
        
        switch(stateIndex)
		{
            case 0:
                animator.SetTrigger("Move");
                break;

            case 1:
                animator.SetTrigger("GradeShoot");
                break;
		}
	}
}