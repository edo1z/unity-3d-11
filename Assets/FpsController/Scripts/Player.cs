using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float _player_height = 1.8f;
    [SerializeField] private float _player_crouching_height = 0.9f;
    [SerializeField] private float _camera_height_ratio = 0.9f;
    [SerializeField] private float _move_speed = 10f;
    [SerializeField] private float _run_speed_multiplier = 2f;
    [SerializeField] private float _crouch_speed_multiplier = 0.5f;
    [SerializeField] private float _look_sensitive_x = 9f;
    [SerializeField] private float _look_sensitive_y = 3f;

    private PlayerInputHandler _input;
    private CharacterController _chara;
    private GameObject _cam;
    private float _player_now_height;

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
        _character_velocity = move_direction * speed;
        _chara.Move(_character_velocity * Time.deltaTime);
    }

    private void UpdateHeight()
    {
        _chara.height = _player_now_height;
        _chara.center = Vector3.up * _chara.height * 0.5f;
        _cam.transform.localPosition = Vector3.up * _chara.height * _camera_height_ratio;
    }

    private void CheckGrounded()
    {

    }

    private void Update()
    {
        Aim();
        Move();
        UpdateHeight();
    }
}
