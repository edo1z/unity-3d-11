using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float _move_speed = 10f;
    [SerializeField] private float _run_speed_multiplier = 2f;
    [SerializeField] private float _look_sensitive_x = 9f;
    [SerializeField] private float _look_sensitive_y = 3f;

    private PlayerInputHandler _input;
    private CharacterController _chara;
    private GameObject _cam;

    private void Awake()
    {
        TryGetComponent(out _chara);
        TryGetComponent(out _input);
        _cam = GameObject.Find("Main Camera");
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
        Vector2 move_direction = _input.GetMoveDirection();
        Vector3 posi = transform.position;
        if (move_direction != Vector2.zero)
        {
            float speed = _move_speed * (_input.GetIsRunning() ? _run_speed_multiplier : 1f);
            posi += transform.right * move_direction.x * speed * Time.deltaTime;
            posi += transform.forward * move_direction.y * speed * Time.deltaTime;
        }
        _chara.Move(posi - transform.position);
    }

    private void Update()
    {
        Aim();
        Move();
    }
}
