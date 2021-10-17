using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

public class Flier : Enemy
{
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _acceleration;
    [SerializeField] private float _damage;

    private PlayerController _targetObject;
    private Transform _targetTransform;

    private Vector2 _targetDir;

    private float _lookAngle;

    protected override void Awake()
    {
        base.Awake();

        _targetObject = FindObjectOfType<PlayerController>();
        _targetTransform = _targetObject.transform;
    }

    private void Update()
    {
        _targetDir = _targetTransform.position - transform.position;
        _targetDir.Normalize();

        _lookAngle = LookDir.GetDir(transform.position, _targetTransform.position);

        transform.localScale = new Vector2(_targetDir.x < 0 ? 1 : -1, transform.localScale.y);

        Rigidbody.velocity = Vector2.Lerp(Rigidbody.velocity, new Vector2(_targetDir.x * _moveSpeed, _targetDir.y * _moveSpeed), _acceleration * Time.fixedDeltaTime);
    }

    public override void Die()
    {
        base.Die();

        Destroy(this.gameObject);
    }

    public override void TakeDamage(float damage)
    {
        CurrentHealth -= damage;

        StartCoroutine(Blink());

        if (CurrentHealth <= 0f) {
            Die();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) {
            other.GetComponent<IDamageable>()?.TakeDamage(_damage);

            Rigidbody.velocity = new Vector2(-_targetDir.x * 20f, -_targetDir.y * 20f);
        } 
        
        if (other.CompareTag("Enemy")) {
            Vector2 dir = transform.position - other.transform.position;

            Rigidbody.velocity = new Vector2(dir.x * 20f, dir.y * 20f);
        }
    }
}
