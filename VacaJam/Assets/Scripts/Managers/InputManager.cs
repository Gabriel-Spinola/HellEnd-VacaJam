using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InputManager : MonoBehaviour
{
    public PInputAction InputAction { get; private set; }

    [SerializeField] private Vector2 _movementVec;

    [SerializeField] private bool _keyJump;
    [SerializeField] private bool _keyJumpHold;

    public Vector2 MovementVec => _movementVec;

    public bool KeyJump => _keyJump;
    public bool KeyJumpHold => _keyJumpHold;

    private void Awake()
    {
        InputAction = new PInputAction();

        InputAction.Player.Enable();
    }

    private void Update()
    {
#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.RightAlt)) {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
#endif

        _movementVec = InputAction.Player.Movement.ReadValue<Vector2>();
        _keyJumpHold = InputAction.Player.JumpHold.ReadValue<float>() > 0f;
        _keyJump = InputAction.Player.Jump.triggered;

        Debug.Log
            (KeyJump);
    }
}
