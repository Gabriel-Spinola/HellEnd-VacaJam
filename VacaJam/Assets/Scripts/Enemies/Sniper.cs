using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

public class Sniper : PathFinderEnemy, IShooteable
{
    [SerializeField] private Weapon _weapon;

    [SerializeField] private LayerMask _whatIsBlock;

    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _acceleration;
    [SerializeField] private float _deacceleration;
    [SerializeField] private float _maxDistanceFromPlayer;

    [Range(.1f, 10f)]
    [SerializeField] private float _attackRadius;

    private Vector3 _targetDir;
    private PlayerController _targetObject;

    private float _lookAngle;

    private bool _canMove = true;

    protected sealed override void Awake()
    {
        base.Awake();

        _targetObject = FindObjectOfType<PlayerController>();
        TargetTransform = _targetObject.transform;
    }

    private void Update()
    {
        _lookAngle = LookDir.GetDir(transform.position, TargetTransform.position);

        transform.localScale = new Vector2(_targetDir.x < 0 ? 1 : -1, transform.localScale.y);

        Attack();
    }

    private void FixedUpdate()
    {
        if (!Physics2D.OverlapCircle(transform.position, _attackRadius, WhatIsTarget) && _canMove && _targetObject.IsEnabled) {
            Chase();
            Debug.Log("Not moving");
        }
        else if (_canMove) {
            if (Vector2.Distance(transform.position, TargetTransform.position) >= _maxDistanceFromPlayer) {
                Rigidbody.velocity = Vector2.Lerp(Rigidbody.velocity, Vector2.zero, _deacceleration * Time.fixedDeltaTime);
            }
            else {
                _targetDir = TargetTransform.position - transform.position;
                _targetDir.Normalize();

                Rigidbody.velocity = Vector2.Lerp(Rigidbody.velocity, -_targetDir * _moveSpeed, _deacceleration * Time.fixedDeltaTime);
            }
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

        StartCoroutine(Blink());

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