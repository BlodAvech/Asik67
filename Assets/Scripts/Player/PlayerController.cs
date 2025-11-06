using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    public Rigidbody2D Rigidbody { get; private set; }
    public float Input { get; set;}
    [SerializeField] public Transform sprite;
    
    private bool isFlipped = false;

    private void Awake()
    {
        Rigidbody = GetComponent<Rigidbody2D>();
    }

	private void Update()
	{
        if ((Input > 0 && isFlipped) || ((Input < 0 && !isFlipped))) Flip();
	}

    private void Flip()
    {
        isFlipped = !isFlipped;
        sprite.localScale *= new Vector2(-1, 1);
    }
}
