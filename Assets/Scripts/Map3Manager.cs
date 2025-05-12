using UnityEngine;

public class Map3Manager : MonoBehaviour
{
    [SerializeField] private int totalEnemiesInMap3 = 6; // ��駤��� Inspector

    private void Start()
    {
        if (Map3EnemyCounter.Instance != null)
        {
            Map3EnemyCounter.Instance.Initialize(totalEnemiesInMap3);
        }
    }
}
