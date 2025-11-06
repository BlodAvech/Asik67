using TMPro;
using Unity.Mathematics;
using UnityEngine;

public class GradeProjectile : MonoBehaviour
{
    [SerializeField] private TMP_Text text;
    
    private float damage;
    private Transform target;
    private Rigidbody2D rb;
    private float speed;
	private Vector2 direction;
	[SerializeField] private Gradient color;

	private void Initialize(float damage, float speed , Transform target = null)
    {
        this.damage = damage;
        this.speed = speed;
		this.target = target;

		text.text = (100 - damage).ToString();
		text.color = color.Evaluate((100 - damage) / 100);
		
        rb = GetComponent<Rigidbody2D>();

		if (target != null)
		{
			direction = ((Vector2)target.position - rb.position).normalized;
			float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
			rb.rotation = angle;

			if (angle <= -90)
				rb.transform.localScale *= -1;
		}
		else
		{
			direction = Vector2.down;
		}
		GameObject.Destroy(gameObject, 5f);
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + direction * speed * Time.fixedDeltaTime);
    }
	public static GradeProjectile Create(GameObject prefab, Vector2 position, float damage, float speed, Transform target = null)
	{
		GameObject projectileObj = Instantiate(prefab, position, Quaternion.identity);
		GradeProjectile projectile = projectileObj.GetComponent<GradeProjectile>();
		projectile.Initialize(damage, speed , target);
		return projectile;
	}

	void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.TryGetComponent<Health>(out Health targetHealth))
		{
			targetHealth.FixedDamage(damage);
		}

		GameObject.Destroy(gameObject);
	}
}