using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float _move_speed = 10f;
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
        Vector2 angles = transform.eulerAngles;
        float x = direction.y * 0.01f * _look_sensitive_y * -1f;
        float y = direction.x * 0.01f * _look_sensitive_x;
        if (x > 75f) x = 75f;
        if (x < -75f) x = -75f;
        if (y > 75f) y = 75f;
        if (y < -75f) y = -75f;
        transform.rotation = Quaternion.Euler(angles.x + x, angles.y + y, 0);
    }

    private void Move()
    {
        Vector2 move_direction = _input.GetMoveDirection();
        Vector3 posi = transform.position;
        if (move_direction != Vector2.zero)
        {
            posi += transform.right * move_direction.x * _move_speed * Time.deltaTime;
            posi += transform.forward * move_direction.y * _move_speed * Time.deltaTime;
        }
        Vector3 delta = posi - transform.position;
        delta.y = 0;
        _chara.Move(delta);
    }

    private void Update()
    {
        Aim();
        Move();
    }
}
