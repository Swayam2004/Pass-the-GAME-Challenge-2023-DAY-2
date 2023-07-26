using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInput : MonoBehaviour
{
    public static GameInput Instance { get; private set; }

    public event EventHandler OnJumpAction;

    private PlayerInputActions _playerInputActions;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("There are more than one GameInput");
            Destroy(gameObject);
            return;
        }
        Instance = this;

        _playerInputActions = new PlayerInputActions();
        _playerInputActions.Enable();

        _playerInputActions.Player.Jump.performed += Jump_performed;
    }

    private void OnDestroy()
    {
        _playerInputActions.Player.Jump.performed -= Jump_performed;

        _playerInputActions.Dispose();
    }

    private void Jump_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnJumpAction?.Invoke(this, EventArgs.Empty);
    }

    public Vector2 GetMovementNormalized()
    {
        Vector2 inputVector = _playerInputActions.Player.Move.ReadValue<Vector2>();

        inputVector.Normalize();

        return inputVector;
    }
}
