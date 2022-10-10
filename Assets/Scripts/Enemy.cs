using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    private GameObject _player;
    private NavMeshAgent _enemy;
    private float health = 50f;

    private GameObject GetPlayer()
    {
        return _player ?? (_player = GameObject.FindWithTag("Player"));
    }

    public void TakeDamage(float amount)
    {
        health -= amount;
        if (health <= 0) Die();
    }

    private void Die()
    {
        Destroy(gameObject);
    }

    void Awake()
    {
        _enemy = gameObject.GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        Vector3 player_position = GetPlayer().transform.position;
        _enemy.destination = player_position;
    }
}
