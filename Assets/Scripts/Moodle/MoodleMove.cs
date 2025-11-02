using Unity.VisualScripting;
using UnityEngine;
[ExecuteAlways]
public class MoodleMove : StateMachineBehaviour
{

    [SerializeField] private float speed = 2f;
    [SerializeField] private float amplitude = 1f;
    [SerializeField] private float frequency = 2f;
    [SerializeField] private float XLimit = 15f;
    private bool toRight = true;

    [SerializeField] private float minTimer = 5f;
    [SerializeField] private float maxTimer = 10f;
    private Animator animator;
    private Timer timer;

    private Rigidbody2D rigidbody2D;
    private Vector3 startPos;
    private float stateTimer;
	private MoodleStateManager stateManager;

	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        startPos = animator.transform.position;
        rigidbody2D = animator.GetComponent<Rigidbody2D>();
        stateTimer = 0f;

		stateManager = animator.GetComponent<MoodleStateManager>();
        stateManager.StartStateSelection(minTimer, maxTimer, 0);
        this.animator = animator;
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        stateTimer += Time.fixedDeltaTime;

        float direction = toRight ? 1f : -1f;

        float deltaX = speed * direction * Time.fixedDeltaTime;
        float deltaY = Mathf.Sin(stateTimer * frequency) * amplitude - Mathf.Sin((stateTimer - Time.fixedDeltaTime) * frequency) * amplitude;

        rigidbody2D.MovePosition(rigidbody2D.position + new Vector2(deltaX, deltaY));

        if ((rigidbody2D.position.x > XLimit && toRight) || (rigidbody2D.position.x < -XLimit && !toRight))
        {
            toRight = !toRight;
            var scale = animator.transform.localScale;
            scale.x *= -1;
            rigidbody2D.transform.localScale = scale;
        }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        rigidbody2D.position = new Vector2(rigidbody2D.position.x, startPos.y);
    }
    
    public void SelectNextState()
	{
        animator.SetTrigger("Idle");
	}
}
