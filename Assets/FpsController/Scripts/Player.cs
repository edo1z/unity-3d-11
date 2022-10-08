using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float _player_height = 1.8f;
    [SerializeField] private float _player_crouching_height = 0.9f;
    [SerializeField] private float _camera_height_ratio = 0.9f;
    [SerializeField] private float _move_speed = 6f;
    [SerializeField] private float _run_speed_multiplier = 2f;
    [SerializeField] private float _crouch_speed_multiplier = 0.3f;
    [SerializeField] private float _jump_force = 10f;
    [SerializeField] private float _jumping_add_speed = 10f;
    [SerializeField] private float _gravity_force = 20f;
    [SerializeField] private float _look_sensitive_x = 12f;
    [SerializeField] private float _look_sensitive_y = 8f;
    private float _start_check_grounded_time = 0.2f;
    private float _grounded_check_distance = 0.05f;

    private PlayerInputHandler _input;
    private CharacterController _chara;
    private GameObject _cam;
    private float _player_now_height;
    private bool _is_grounded = false;
    private float _last_jump_time;

    private Vector3 _character_velocity = new Vector3(0, 0, 0);

    private void Awake()
    {
        TryGetComponent(out _chara);
        TryGetComponent(out _input);
        _cam = GameObject.Find("Main Camera");
    }

    private void Start()
    {
        _player_now_height = _player_height;
        UpdateHeight();
    }

    private void Aim()
    {
        Vector2 direction = _input.GetLookDirection();
        if (direction == Vector2.zero) return;
        float x = direction.y * 0.01f * _look_sensitive_y * -1f;
        float y = direction.x * 0.01f * _look_sensitive_x;
        transform.Rotate(new Vector3(0, y, 0), Space.Self);
        float vertical_angle = Mathf.Clamp(x, -89f, 89f);
        _cam.transform.localEulerAngles += new Vector3(vertical_angle, 0, 0);
    }

    private void Move()
    {
        Vector2 local_move_direction = _input.GetMoveDirection();
        Vector3 move_direction = transform.TransformVector(new Vector3(local_move_direction.x, 0, local_move_direction.y));
        if (_is_grounded)
        {
            float speed = _move_speed * (_input.GetIsRunning() ? _run_speed_multiplier : 1f);
            if (_input.GetIsCrouching())
            {
                speed *= _crouch_speed_multiplier;
                _player_now_height = _player_crouching_height;
            }
            else
            {
                _player_now_height = _player_height;
            }
            float sharpness = 15f;
            _character_velocity = Vector3.Lerp(_character_velocity, move_direction * speed, sharpness * Time.deltaTime);

            if (_input.GetIsJumping())
            {
                if (_input.GetIsCrouching())
                {
                    _input.SetCrouching(false);
                }
                else
                {
                    _character_velocity.y = _jump_force;
                    _is_grounded = false;
                    _last_jump_time = Time.time;
                }
            }
        }
        else
        {
            _character_velocity += move_direction * _jumping_add_speed * Time.deltaTime;
            Vector3 horizontal_velocity = Vector3.ProjectOnPlane(_character_velocity, Vector3.up);
            horizontal_velocity = Vector3.ClampMagnitude(horizontal_velocity, _move_speed);
            _character_velocity = horizontal_velocity + Vector3.up * _character_velocity.y;
            _character_velocity += Vector3.down * _gravity_force * Time.deltaTime;
        }
        _chara.Move(_character_velocity * Time.deltaTime);
    }

    private void UpdateHeight()
    {
        float sharpness = 15f;
        _chara.height = Mathf.Lerp(_chara.height, _player_now_height, sharpness * Time.deltaTime);
        _chara.center = Vector3.up * _chara.height * 0.5f;
        _cam.transform.localPosition = Vector3.up * _chara.height * _camera_height_ratio;
    }

    private void CheckGrounded()
    {
        _is_grounded = false;
        if (Time.time >= _last_jump_time + _start_check_grounded_time)
        {
            Vector3 bottom_posi = transform.position + (transform.up * _chara.radius);
            Vector3 top_posi = transform.position + transform.up * (_chara.height - _chara.radius);
            float distance = _grounded_check_distance + _chara.skinWidth;
            Debug.Log("bottom: " + bottom_posi + " top: " + top_posi + " distance: " + distance);
            if (Physics.CapsuleCast(bottom_posi, top_posi, _chara.radius, Vector3.down, out RaycastHit hit, distance, -1, QueryTriggerInteraction.Ignore))
            {
                _is_grounded = true;
            }
        }
    }

    private void Update()
    {
        CheckGrounded();
        Aim();
        Move();
        UpdateHeight();
    }
}
