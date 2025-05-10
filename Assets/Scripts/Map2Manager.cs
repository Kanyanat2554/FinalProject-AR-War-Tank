using UnityEngine;

public class Map2Manager : MonoBehaviour
{
    [SerializeField] private int totalEnemiesInMap2 = 5; // ตั้งค่าจำนวนศัตรูใน Inspector

    private void Start()
    {
        // กำหนดจำนวนศัตรูเริ่มต้นใน Map2
        if (Map2EnemyCounter.Instance != null)
        {
            Map2EnemyCounter.Instance.Initialize(totalEnemiesInMap2);
        }
    }
}
