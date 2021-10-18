using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer), typeof(Animator))]
public class PlayerGraphics : MonoBehaviour
{
    [SerializeField] private Animator _scaleAnimator;
    [SerializeField] private ParticleSystem _jumpParticle;
    [SerializeField] private Material _hitMaterial;

    public SpriteRenderer SpriteRenderer;
    public Animator Animator;

    [HideInInspector] public int Side;

    private Material _defaultMaterial;

    
    private PlayerController _playerController;

    private void Start()
    {
        _defaultMaterial = SpriteRenderer.material;

        SpriteRenderer = GetComponent<SpriteRenderer>();
        Animator = GetComponent<Animator>();
        _playerController = FindObjectOfType<PlayerController>();
    }

    private void Update()
    {
        if (PauseMenu.isGamePaused)
            return;

        Animator.SetFloat("Horizontal", Mathf.Abs(_playerController.Input.MovementVec.x));
        _scaleAnimator.SetFloat("yVel", _playerController.Rigidbody.velocity.y);
    }

    public void FlipObject()
    {
        SpriteRenderer.flipX = Side != 1;
    }

    public IEnumerator Blink()
    {
        SpriteRenderer.material = _hitMaterial;

        yield return new WaitForSeconds(.1f);

        SpriteRenderer.material = _defaultMaterial;
    }

    public void AnimDeath() => _playerController.Die();
    public void SetTrigger(string triggerName) => Animator.SetTrigger(triggerName);
    public void SetHeightTrigger(string triggerName) => _scaleAnimator.SetTrigger(triggerName);
    public void PlayerJumpParticle() => _jumpParticle.Play();
}
