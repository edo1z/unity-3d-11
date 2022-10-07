using UnityEngine;

public class Hoge : MonoBehaviour
{
    private float _time;

    void Start()
    {

    }

    void Update()
    {
        _time += Time.deltaTime;
        if (_time > 1f)
        {
            Vector3 angles = new Vector3(0f, 30f, 0f);
            transform.Rotate(angles, Space.World);
            _time = 0;
        }
    }
}
