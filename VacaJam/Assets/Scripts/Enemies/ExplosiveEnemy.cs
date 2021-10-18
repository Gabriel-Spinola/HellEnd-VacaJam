using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

public class ExplosiveEnemy : Enemy
{
    [SerializeField] private LineRenderer _line;

    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _acceleration;
    [SerializeField] private float _damage;

    [SerializeField] private float _explosionDelay;

    [Range(0.1f, 10f)]
    [SerializeField] private float _attackRadius;
    [Range(0.1f, 10f)]
    [SerializeField] private float _explosiveRadius;
    [Range(0, 50)]
    [SerializeField] private int _segmentCount = 50;

    private PlayerController _targetObject;
    private Transform _targetTransform;
    private Animator _animator;

    private Vector2 _targetDir;

    private float _lookAngle;

    private bool _isAttacking = false;

    protected override void Awake()
    {
        base.Awake();

        _targetObject = FindObjectOfType<PlayerController>();
        _targetTransform = _targetObject.transform;
    }

    protected override void Start()
    {
        base.Start();

        /*_line = gameObject.GetComponent<LineRenderer>();

        _line.positionCount = _segmentCount + 1;
        _line.useWorldSpace = false;*/

        CreateCircle();
    }

    private void Update()
    {
        if (_isAttacking)
            return;

        _targetDir = _targetTransform.position - transform.position;
        _targetDir.Normalize();

        _lookAngle = LookDir.GetDir(transform.position, _targetTransform.position);

        transform.localScale = new Vector2(_targetDir.x < 0 ? 1 : -1, transform.localScale.y);

        Rigidbody.velocity = Vector2.Lerp(Rigidbody.velocity, new Vector2(_targetDir.x * _moveSpeed, _targetDir.y * _moveSpeed), _acceleration * Time.fixedDeltaTime);
    }

    private void FixedUpdate()
    {
        if (Physics2D.OverlapCircle(transform.position, _attackRadius, WhatIsTarget)) {
            StartCoroutine(Attack());
        }
    }

    private IEnumerator Attack()
    {
        _isAttacking = true;
        _animator.SetTrigger("Explode");

        yield return new WaitForSeconds(_explosionDelay);

        AudioManager._I.PlaySound2D("Explosion");

        //Explosion explosion_ = Instantiate(explosionEffect, transform.position, Quaternion.identity).GetComponent<Explosion>();
        //explosion_.radius = explosionRadius;

        Collider2D hitted = Physics2D.OverlapCircle(transform.position, _explosiveRadius, WhatIsTarget);

        if (hitted) {
            hitted.GetComponent<IDamageable>().TakeDamage(_damage);
        }

        Die();
    }

    private void CreateCircle()
    {
        float x;
        float y;

        float angle = 20f;

        for (int i = 0; i < (_segmentCount + 1); i++) {
            x = Mathf.Sin(Mathf.Deg2Rad * angle) * 3.55f;
            y = Mathf.Cos(Mathf.Deg2Rad * angle) * 3.55f;

            _line.SetPosition(i, new Vector3(x, y, 0));

            angle += (360f / _segmentCount);
        }
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
        if (other.CompareTag("Enemy")) {
            Vector2 dir = transform.position - other.transform.position;

            Rigidbody.velocity = new Vector2(dir.x * 20f, dir.y * 20f);
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Enemy")) {
            Vector2 dir = transform.position - other.transform.position;

            Rigidbody.velocity = new Vector2(dir.x * 20f, dir.y * 20f);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _explosiveRadius);
    }
}
