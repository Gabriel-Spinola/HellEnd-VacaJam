﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

[RequireComponent(typeof(Rigidbody2D), typeof(CollisionDetection))]
public class PlayerController : MonoBehaviour, IDamageable
{
    [Header("References")]
    [SerializeField] private InputManager _input;
    [SerializeField] private Weapon _weapon;

    [Header("Stats")]
    [SerializeField] private float _health;

    [Header("Config")]
    [Range(0.1f, 10f)]
    [SerializeField] private float _maxWeaponRotationDistance = 1f;

    [Header("Movement")]
    [SerializeField] private float _jumpForce;
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _friction;

    [Header("Jump")]
    [SerializeField] private float _fallMultiplier;
    [SerializeField] private float _lowJumpMultiplier;

    [Header("Debug")]
    [SerializeField] private bool _shouldDie;

    private Rigidbody2D _rigidbody;
    private CollisionDetection _collision;

    private bool _isJumpDisabled;

    private void Awake()
    {
        _collision = GetComponent<CollisionDetection>();
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        BetterJump();

        if (_input.KeyJump && _collision.IsGrounded) {
            Jump();
        }

        float lookAngle = LookDir.GetDir(transform.position);

        _weapon.UseWeapon(_input.KeyShoot, new OptionalNonSerializable<GameObject>(this.gameObject));
        _weapon.LookAngle = lookAngle;
        _weapon.transform.position = transform.position + LookDir.AngleAxisToVector3(lookAngle, _maxWeaponRotationDistance);
    }

    private void FixedUpdate()
    {
        Movement();
    }

    private void Movement()
    {
        if (_input.MovementVec.x != 0) {
            _rigidbody.velocity = new Vector2(_input.MovementVec.x * _moveSpeed, _rigidbody.velocity.y);
        }
        else {
            _rigidbody.velocity = new Vector2(Mathf.Lerp(_rigidbody.velocity.x, 0f, _friction * Time.fixedDeltaTime), _rigidbody.velocity.y);
        }
    }

    private void Jump()
    {
        if (_isJumpDisabled)
            return;

        _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, _jumpForce);

        StartCoroutine(DisableJump());
    }

    private IEnumerator DisableJump()
    {
        _isJumpDisabled = true;

        yield return new WaitForSeconds(.25f);

        _isJumpDisabled = false;
    }

    /// <summary>
    /// if falling, add fallMultiplier
    /// if jumping and not holding spacebar or walljumping, increase gravity to peform a small jump
    /// if jumping and holding spacebar, perform a full jump
    /// </summary>
    private void BetterJump()
    {
        if (_rigidbody.velocity.y < 0) {
            _rigidbody.velocity += (_fallMultiplier - 1) * Physics.gravity.y * Time.deltaTime * Vector2.up;
        }
        else if (_rigidbody.velocity.y > 0 && !_input.KeyJumpHold) {
            _rigidbody.velocity += (_lowJumpMultiplier - 1) * Physics.gravity.y * Time.deltaTime * Vector2.up;
        }
    }

    public void TakeDamage(float damage)
    {
        _health -= damage;

        if (_health <= 0)
            Die();
    }

    public void Die()
    {
#if UNITY_EDITOR
        if (!_shouldDie) {
            Debug.Log("Died");

            return;
        }
#endif

        // Die
    }
}