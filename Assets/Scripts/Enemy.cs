using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    private GameObject _player;
    //private static GameObject _enemy_obj;
    private NavMeshAgent _enemy;

    //private static GameObject GetEnemy()
    //{
    //    return _enemy_obj ?? (_enemy_obj = (GameObject)Resources.Load("Prefabs/Enemy/Enemy"));
    //}

    private GameObject GetPlayer()
    {
        return _player ?? (_player = GameObject.FindWithTag("Player"));
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
