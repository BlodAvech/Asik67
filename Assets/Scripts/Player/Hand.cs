using UnityEngine;
using UnityEngine.InputSystem;

public class Hand : MonoBehaviour
{
    private bool isFlipped = false;
    public float AngleToMouse
    {
        get
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 direction = mousePos - transform.position;
            return Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        }
    }

    private void Awake()
    {
        InputManager.Controls.Player.Mouse.performed += RotateToMouse;
    }

    private void RotateToMouse(InputAction.CallbackContext ctx)
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(ctx.ReadValue<Vector2>());
        Vector3 direction = mousePos - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, angle);

        if ((((180f >= angle && angle >= 90f) ||
        (-180f <= angle && angle <= -90f)) && !isFlipped) ||
            ((90 > angle && angle > -90f) && isFlipped)) Flip();
    }

    private void Flip()
	{
        isFlipped = !isFlipped;
        transform.localScale *= new Vector2(1, -1);
	}

    void OnDisable()
	{
		InputManager.Controls.Player.Mouse.performed -= RotateToMouse;
	}
}
