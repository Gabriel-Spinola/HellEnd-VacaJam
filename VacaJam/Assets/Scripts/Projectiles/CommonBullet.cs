using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommonBullet : Projectile
{
    private Rigidbody2D _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        StartCoroutine(DestroyProjectileOnTime());
    }

    private void Update()
    {
        _rigidbody.velocity = transform.right * Speed;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name.Contains(gameObject.name) || other.CompareTag("CameraCollider"))
            return;

        // $ Optimize it later
        if (Owner.Enabled) {
            if (other.gameObject == null || Owner.Value == null)
                return;

            if (other.gameObject.name.Contains(Owner.Value.name) && Owner.Value.layer == other.gameObject.layer) {
                return;
            }
        }

        other.gameObject.GetComponent<IDamageable>()?.TakeDamage(Damage);

        Destroy(gameObject);
    }
}