using UnityEngine;

public class Game : MonoBehaviour
{
    private static GameObject _enemy_obj;

    private static GameObject GetEnemy()
    {
        return _enemy_obj ?? (_enemy_obj = (GameObject)Resources.Load("Prefabs/Enemy/Enemy"));
    }

}
