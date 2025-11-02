using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerController))]
public class PlayerMovement : MonoBehaviour
{
	private PlayerController playerController;

	private float input;
    [SerializeField] private float defaultMoveSpeed = 7f;
    

    private void Awake()
    {
        InputManager.Controls.Player.Move.performed += Move;
		InputManager.Controls.Player.Move.canceled += Move;

		playerController = GetComponent<PlayerController>();
    }

	private void FixedUpdate()
	{
		playerController.Rigidbody.linearVelocityX = input * defaultMoveSpeed * Time.fixedDeltaTime;
	}
	
	private void Move(InputAction.CallbackContext ctx)
	{
		input = ctx.ReadValue<float>();
		playerController.Input = input;
	}
	
	void OnDestroy()
	{
		InputManager.Controls.Player.Move.performed -= Move;
        InputManager.Controls.Player.Move.canceled -= Move;
	}
}
