using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour
{
    [SerializeField] private bool _isActive = false;
    public static bool GoToNextLevel = false;

    private Collider2D _collider;
    private Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _collider = GetComponent<Collider2D>();

        if (!_isActive)
            _collider.enabled = false;
    }

    public void StartPortal()
    {
        _animator.SetTrigger("StartPortal");
        _collider.enabled = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) {
            var obj = other.GetComponent<PlayerController>();
            StartCoroutine(obj.DisablePlayer(.4f));
            obj.Rigidbody.velocity = Vector2.zero;
            GoToNextLevel = true;
        }
    }
}
