public interface IDamageable
{
    void TakeDamage(float damage);
    void Die();
}

public interface IShooteable
{
    void ShootFeedback(float force, float angle);
}
