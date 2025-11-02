using System;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{    
    private PlayerController playerController;

    [Header("Jump")]
    [SerializeField] private float jumpForce = 20f;
    [SerializeField] private Transform feetPos = null;
    [SerializeField] private LayerMask groundMask;

    
    public bool IsGrounded
    {
        get
        {
            return Physics2D.OverlapCircle(feetPos.position, .2f, groundMask);
        }
        private set { }
    }

    private void Awake()
    {
        playerController = GetComponent<PlayerController>();
                
        InputManager.Controls.Player.Jump.performed += ctx => Jump();
    }

    private void Jump()
    {
        if (!IsGrounded) return;
        playerController.Rigidbody.AddForce(jumpForce * Vector2.up, ForceMode2D.Impulse);
	}
    
    void OnDrawGizmos()
	{
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(feetPos.position, .2f);
	}

	void OnDestroy()
	{
        InputManager.Controls.Player.Jump.performed -= ctx => Jump();
	}
}