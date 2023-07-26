using System;
using UnityEngine;

public class GameInput : MonoBehaviour
{
    public static GameInput Instance { get; private set; }

    public event EventHandler OnJumpAction;
    public event EventHandler OnSwitchAction;
    public event EventHandler OnPlayerFireAction;
    public event EventHandler OnGhostFireAction;
    public event EventHandler OnGamePaused;

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
        _playerInputActions.Player.Fire.performed += Player_Fire_performed;
        _playerInputActions.Player.Pause.performed += Pause_performed;

        _playerInputActions.Ghost.SwitchControl.performed += SwitchControl_performed;
        _playerInputActions.Ghost.Fire.performed += Ghost_Fire_performed;
    }

    private void Pause_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnGamePaused?.Invoke(this, EventArgs.Empty);
    }

    private void Ghost_Fire_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnGhostFireAction?.Invoke(this, EventArgs.Empty);
    }

    private void Player_Fire_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnPlayerFireAction?.Invoke(this, EventArgs.Empty);
    }

    private void SwitchControl_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnSwitchAction?.Invoke(this, EventArgs.Empty);
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

    public Vector2 GetPlayerMovementNormalized()
    {
        Vector2 inputVector = _playerInputActions.Player.Move.ReadValue<Vector2>();

        inputVector.Normalize();

        return inputVector;
    }

    public Vector2 GetGhostMovementNormalized()
    {
        Vector2 inputVector = _playerInputActions.Ghost.Move.ReadValue<Vector2>();

        inputVector.Normalize();

        return inputVector;
    }
}
