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
	private float knockback;

	private void Initialize(float damage, float speed , float knockback , Transform target = null)
    {
        this.damage = damage;
        this.speed = speed;
		this.target = target;
		this.knockback = knockback;

		text.text = (100 - damage).ToString();
		
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
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + direction * speed * Time.fixedDeltaTime);
    }
	public static GradeProjectile Create(GameObject prefab, Vector2 position, float damage, float speed,float knockback , Transform target = null)
	{
		GameObject projectileObj = Instantiate(prefab, position, Quaternion.identity);
		GradeProjectile projectile = projectileObj.GetComponent<GradeProjectile>();
		projectile.Initialize(damage, speed, knockback , target);
		return projectile;
	}

	void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.TryGetComponent<Health>(out Health targetHealth))
		{
			targetHealth.FixedDamage(damage);

			if (collision.gameObject.TryGetComponent<Rigidbody2D>(out Rigidbody2D targetRb))
			{
				Vector2 dir = transform.right;
				targetRb.AddForce(dir * knockback, ForceMode2D.Impulse);
			}
		}

		GameObject.Destroy(gameObject);
	}
}