using UnityEngine;

public class Map2Manager : MonoBehaviour
{
    [SerializeField] private int totalEnemiesInMap2 = 5; // ��駤�Ҩӹǹ�ѵ��� Inspector

    private void Start()
    {
        // ��˹��ӹǹ�ѵ���������� Map2
        if (Map2EnemyCounter.Instance != null)
        {
            Map2EnemyCounter.Instance.Initialize(totalEnemiesInMap2);
        }
    }
}
