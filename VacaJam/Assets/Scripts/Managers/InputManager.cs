using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InputManager : MonoBehaviour
{
    public PInputAction InputAction { get; private set; }

    public Vector2 MovementVec;

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

        MovementVec = InputAction.Player.Movement.ReadValue<Vector2>();
    }
}
