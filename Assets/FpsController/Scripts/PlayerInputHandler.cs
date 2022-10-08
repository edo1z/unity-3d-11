using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    private PlayerInput _input;
    private Vector2 _move_direction;
    private Vector2 _look_direction;
    private bool _is_running = false;
    private bool _is_crouching = false;
    private bool _is_fire = false;
    private bool _is_jumping = false;

    private void Awake()
    {
        TryGetComponent(out _input);
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public bool CanProcessInput()
    {
        return Cursor.lockState == CursorLockMode.Locked;
    }

    private void OnEnable()
    {
        _input.actions["Move"].performed += OnMove;
        _input.actions["Move"].canceled += OnMove;
        _input.actions["Look"].performed += OnLook;
        _input.actions["Look"].canceled += OnLook;
        _input.actions["Run"].started += OnRun;
        _input.actions["Run"].canceled += OnRun;
        _input.actions["Crouch"].started += OnCrouch;
        _input.actions["Crouch"].canceled += OnCrouch;
        _input.actions["Fire"].started += OnFire;
        _input.actions["Fire"].canceled += OnFire;
        _input.actions["Jump"].started += OnJump;
        _input.actions["Jump"].canceled += OnJump;
    }

    private void OnDisable()
    {
        _input.actions["Move"].performed -= OnMove;
        _input.actions["Move"].canceled -= OnMove;
        _input.actions["Look"].performed -= OnLook;
        _input.actions["Look"].canceled -= OnLook;
        _input.actions["Run"].started += OnRun;
        _input.actions["Run"].canceled += OnRun;
        _input.actions["Crouch"].started += OnCrouch;
        _input.actions["Crouch"].canceled += OnCrouch;
        _input.actions["Fire"].started += OnFire;
        _input.actions["Fire"].canceled += OnFire;
        _input.actions["Jump"].started += OnJump;
        _input.actions["Jump"].canceled += OnJump;
    }

    private void OnMove(InputAction.CallbackContext obj)
    {
        _move_direction = obj.ReadValue<Vector2>();
    }

    private void OnLook(InputAction.CallbackContext obj)
    {
        _look_direction = obj.ReadValue<Vector2>();
    }

    private void OnRun(InputAction.CallbackContext obj)
    {
        switch (obj.phase)
        {
            case InputActionPhase.Started:
                _is_running = true;
                break;
            case InputActionPhase.Canceled:
                _is_running = false;
                break;
        }
    }

    // ボタンを押すとしゃがむが切り替わる
    private void OnCrouch(InputAction.CallbackContext obj)
    {
        switch (obj.phase)
        {
            case InputActionPhase.Started:
                _is_crouching = !_is_crouching;
                break;
            case InputActionPhase.Canceled:
                break;
        }
    }

    // ボタンを押している間のみしゃがむ
    private void OnCrouch2(InputAction.CallbackContext obj)
    {
        switch (obj.phase)
        {
            case InputActionPhase.Started:
                _is_crouching = true;
                break;
            case InputActionPhase.Canceled:
                _is_crouching = false;
                break;
        }
    }

    private void OnFire(InputAction.CallbackContext obj)
    {
        switch (obj.phase)
        {
            case InputActionPhase.Started:
                _is_fire = true;
                break;
            case InputActionPhase.Canceled:
                _is_fire = false;
                break;
        }
    }

    private void OnJump(InputAction.CallbackContext obj)
    {
        switch (obj.phase)
        {
            case InputActionPhase.Started:
                _is_jumping = true;
                break;
            case InputActionPhase.Canceled:
                _is_jumping = false;
                break;
        }
    }

    public Vector2 GetMoveDirection()
    {
        if (CanProcessInput())
        {
            return _move_direction.normalized;
        }
        else
        {
            return Vector2.zero;
        }
    }

    public Vector2 GetLookDirection()
    {
        if (CanProcessInput())
        {
            return _look_direction;
        }
        else
        {
            return Vector2.zero;
        }
    }

    public bool GetIsRunning()
    {
        return _is_running;
    }

    public bool GetIsCrouching()
    {
        return _is_crouching;
    }

    public void SetCrouching(bool is_crouching)
    {
        _is_crouching = is_crouching;
    }

    public bool GetIsJumping()
    {
        return _is_jumping;
    }

    public bool GetIsFire()
    {
        return _is_fire;
    }

}
