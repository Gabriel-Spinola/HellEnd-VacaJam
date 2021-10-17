using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer), typeof(Animator))]
public class PlayerGraphics : MonoBehaviour
{
    [SerializeField] private Animator _scaleAnimator;
    [SerializeField] private ParticleSystem _jumpParticle;

    [HideInInspector] public SpriteRenderer SpriteRenderer;
    [HideInInspector] public int Side;

    private Animator _animator;
    private PlayerController _playerController;

    private void Start()
    {
        SpriteRenderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
        _playerController = FindObjectOfType<PlayerController>();
    }

    private void Update()
    {
        _animator.SetFloat("Horizontal", Mathf.Abs(_playerController.Input.MovementVec.x));
        _scaleAnimator.SetFloat("yVel", _playerController.Rigidbody.velocity.y);
    }

    public void FlipObject()
    {
        SpriteRenderer.flipX = Side != 1;
    }

    public void SetHeightTrigger(string triggerName) => _scaleAnimator.SetTrigger(triggerName);
    public void PlayerJumpParticle() => _jumpParticle.Play();
}
