using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using Utils;

[RequireComponent(typeof(Rigidbody2D), typeof(CollisionDetection))]
public class PlayerController : MonoBehaviour, IDamageable, IShooteable
{
    [Header("References")]
    [SerializeField] private InputManager _input;
    [SerializeField] private Weapon _weapon;
    [SerializeField] private PlayerGraphics _playerGraphics;

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
    [SerializeField] private float _jumpBufferLength = .1f;

    [SerializeField] private int _hangTime = 8;

    [Header("Debug")]
    [SerializeField] private bool _shouldDie;

    public Rigidbody2D Rigidbody => _rigidbody;
    public InputManager Input => _input;

    private Rigidbody2D _rigidbody;
    private CollisionDetection _collision;

    private float _jumpBufferCount;
    private float _lookAngle;

    private int _canJump = 0;

    private bool _isJumpDisabled = false;
    private bool _canMove = true;
    private bool _isGroundedPrev = false;

    private void Awake()
    {
        _collision = GetComponent<CollisionDetection>();
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        BetterJump();

        _lookAngle = LookDir.GetDir(transform.position);

        _canJump--;

        _playerGraphics.Side = _lookAngle < 90 && _lookAngle > -90 ? 1 : -1;

        _playerGraphics.FlipObject();

        if (_collision.IsGrounded && !_isGroundedPrev) {
            _playerGraphics.SetHeightTrigger("Squash");
            _playerGraphics.PlayerJumpParticle();
        }

        if (_collision.IsGrounded) {
            _canJump = _hangTime;
        }

        if (_input.KeyJump) {
            _jumpBufferCount = _jumpBufferLength;
        }
        else {
            _jumpBufferCount -= Time.deltaTime;
        }

        if (_canJump > 0 && _jumpBufferCount >= 0) {
            Jump();
        }

        _weapon.UseWeapon(_input.KeyShoot, new OptionalNonSerializable<GameObject>(this.gameObject));
        _weapon.LookAngle = _lookAngle;
        _weapon.transform.position = transform.position + LookDir.AngleAxisToVector3(_lookAngle, _maxWeaponRotationDistance);

        if (transform.position.y < -14)
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        _isGroundedPrev = _collision.IsGrounded;
    }

    private void FixedUpdate()
    {
        if (_canMove)
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

        _playerGraphics.SetHeightTrigger("Stretch");
        _playerGraphics.PlayerJumpParticle();

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

    public IEnumerator DisableMovement(float time)
    {
        _canMove = false;

        yield return new WaitForSeconds(time);

        _canMove = true;
    }

    public void ShootFeedback(float force, float angle)
    {
        _rigidbody.velocity = new Vector2(Mathf.Lerp(Rigidbody.velocity.x, 0f, 16f * Time.deltaTime), Rigidbody.velocity.y);

        _rigidbody.velocity -= (Vector2) LookDir.GetDir(angle) * force;
        StartCoroutine(DisableMovement(.1f));
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

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
