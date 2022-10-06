using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    private PlayerInput _input;
    private Vector2 _move_direction;
    private Vector2 _look_direction;

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
        Debug.Log("Player OnEnable");
        _input.actions["Move"].performed += OnMove;
        _input.actions["Move"].canceled += OnMove;
        _input.actions["Look"].performed += OnLook;
        _input.actions["Look"].canceled += OnLook;
    }

    private void OnDisable()
    {
        Debug.Log("Player OnDisable");
        _input.actions["Move"].performed -= OnMove;
        _input.actions["Move"].canceled -= OnMove;
        _input.actions["Look"].performed -= OnLook;
        _input.actions["Look"].canceled -= OnLook;
    }

    private void OnMove(InputAction.CallbackContext obj)
    {
        Debug.Log("OnMove");
        _move_direction = obj.ReadValue<Vector2>();
    }

    private void OnLook(InputAction.CallbackContext obj)
    {
        Debug.Log("OnLook");
        _look_direction = obj.ReadValue<Vector2>();
        Debug.Log("OnLook: " + _look_direction);
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

}
