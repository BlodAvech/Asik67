using UnityEngine;

public class ErrorProjectileBuilder
{
    private GameObject prefab;
    private Vector2 position;
    private float damage;
    private float speed;
    private float rotation;
    private float lifetime = 5f;

    public ErrorProjectileBuilder WithPrefab(GameObject prefab)
    {
        this.prefab = prefab;
        return this;
    }

    public ErrorProjectileBuilder WithPosition(Vector2 position)
    {
        this.position = position;
        return this;
    }

    public ErrorProjectileBuilder WithDamage(float damage)
    {
        this.damage = damage;
        return this;
    }

    public ErrorProjectileBuilder WithSpeed(float speed)
    {
        this.speed = speed;
        return this;
    }

    public ErrorProjectileBuilder WithRotation(float rotation)
    {
        this.rotation = rotation;
        return this;
    }

    public ErrorProjectileBuilder WithLifetime(float lifetime)
    {
        this.lifetime = lifetime;
        return this;
    }


    public ErrorProjectile Build()
    {
        if (prefab == null)
        {
            Debug.LogError("Prefab is not set in ErrorProjectileBuilder");
            return null;
        }

        GameObject projectileObj = Object.Instantiate(prefab, position, Quaternion.Euler(0, 0, rotation));
        ErrorProjectile projectile = projectileObj.GetComponent<ErrorProjectile>();

        projectile.Initialize(damage, speed, lifetime);
        return projectile;
    }
}