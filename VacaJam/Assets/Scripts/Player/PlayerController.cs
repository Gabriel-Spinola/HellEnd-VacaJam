using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

[RequireComponent(typeof(Rigidbody2D), typeof(CollisionDetection))]
public class PlayerController : MonoBehaviour, IDamageable
{
    [Header("References")]
    [SerializeField] private InputManager _input;

    [Header("Stats")]
    [SerializeField] private float _health;

    [Header("Movement")]
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _friction;

    [Header("Debug")]
    [SerializeField] private bool _shouldDie;

    private Rigidbody2D _rigidbody;
    private CollisionDetection _collision;

    private void Awake()
    {
        _collision = GetComponent<CollisionDetection>();
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        
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
