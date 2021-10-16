using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

public class Carlin : PathFinderEnemy, IShooteable
{
    [SerializeField] private Weapon _weapon;

    [SerializeField] private LayerMask _whatIsBlock;

    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _acceleration;
    [SerializeField] private float _deacceleration;

    [Range(.1f, 10f)]
    [SerializeField] private float _attackRadius;

    private Vector3 _targetDir;

    private float _lookAngle;

    private bool _canMove = true;

    protected sealed override void Awake()
    {
        base.Awake();

        TargetTransform = FindObjectOfType<PlayerController>().transform;
    }

    private void Update()
    {
        _lookAngle = LookDir.GetDir(transform.position, TargetTransform.position);

        if (!Physics2D.Linecast(transform.position, TargetTransform.position, _whatIsBlock)) {
            Attack();
        }
    }

    private void FixedUpdate()
    {
        if (!Physics2D.OverlapCircle(transform.position, _attackRadius, WhatIsTarget.value) && _canMove) {
            Chase();
        }
        else if (_canMove) {
            /*
            _targetDir = TargetTransform.position - transform.position;
            _targetDir.Normalize();

            Rigidbody.velocity = Vector2.Lerp(Rigidbody.velocity, -_targerDir * _moveSpeed, _deacceleration * Time.fixedDeltaTime);
            */

            Rigidbody.velocity = Vector2.Lerp(Rigidbody.velocity, new Vector2(0f, Rigidbody.velocity.y), _deacceleration * Time.fixedDeltaTime);
        }
    }

    private void Chase()
    {
        if (Path == null)
            return;

        if (CurrentWaypoint >= Path.vectorPath.Count) {
            Debug.Log($"End Of Path Reached: { gameObject.name }");

            return;
        }

        Vector2 dir = (Path.vectorPath[CurrentWaypoint] - transform.position);
        dir.Normalize();

        _targetDir = TargetTransform.position - transform.position;
        _targetDir.Normalize();

        Rigidbody.velocity = Vector2.Lerp(Rigidbody.velocity, new Vector2(dir.x * _moveSpeed, dir.y * _moveSpeed), _acceleration * Time.fixedDeltaTime);

        float distance = Vector2.Distance(transform.position, Path.vectorPath[CurrentWaypoint]);

        if (distance < NextWaypointDistance) {
            CurrentWaypoint++;
        }
    }

    private void Attack()
    {
        _weapon.UseWeapon(new OptionalNonSerializable<GameObject>(this.gameObject));
        _weapon.LookAngle = _lookAngle;
        _weapon.transform.position = transform.position + LookDir.AngleAxisToVector3(_lookAngle, 1f);
    }

    public override void Die()
    {
        base.Die();

        Destroy(this.gameObject);
    }

    public override void TakeDamage(float damage)
    {
        CurrentHealth -= damage;

        if (CurrentHealth <= 0f) {
            Die();
        }
    }

    public void ShootFeedback(float force, float angle)
    {
        Rigidbody.velocity = new Vector2(Mathf.Lerp(Rigidbody.velocity.x, 0f, 16f * Time.deltaTime), Rigidbody.velocity.y);

        Rigidbody.velocity -= (Vector2) LookDir.GetDir(angle) * force;
        StartCoroutine(DisableMovement(.1f));
    }

    private IEnumerator DisableMovement(float time)
    {
        _canMove = false;

        yield return new WaitForSeconds(time);

        _canMove = true;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, _attackRadius);
    }
}
