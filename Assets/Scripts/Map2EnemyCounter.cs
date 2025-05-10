using UnityEngine;
using TMPro;

public class Map2EnemyCounter : MonoBehaviour
{
    public static Map2EnemyCounter Instance; // Singleton สำหรับ Map2 โดยเฉพาะ

    [Header("UI Elements")]
    public TextMeshProUGUI enemyCounterText;

    [SerializeField] private int totalEnemies;
    [SerializeField] private int remainingEnemies;

    private void Awake()
    {
        // Singleton Pattern เฉพาะสำหรับ Map2
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        // หา UI อัตโนมัติถ้าไม่ได้ใส่ Manual
        if (enemyCounterText == null)
        {
            enemyCounterText = GameObject.Find("Map2EnemyCounterText")?.GetComponent<TextMeshProUGUI>();
            if (enemyCounterText == null)
            {
                Debug.LogError("Map2 Enemy Counter Text not found!");
            }
        }
    }

    // เรียกจาก Enemy ใน Map2 เมื่อถูกทำลาย
    public void EnemyKilled()
    {
        remainingEnemies--;
        UpdateUI();

        if (remainingEnemies <= 0)
        {
            Debug.Log("All enemies in Map2 defeated!");
            // เพิ่ม Logic เมื่อเคลียร์ Map2 ตรงนี้
        }
    }

    // ตั้งค่าจำนวนศัตรูเริ่มต้น
    public void Initialize(int totalEnemies)
    {
        this.totalEnemies = totalEnemies;
        this.remainingEnemies = totalEnemies;
        UpdateUI();
    }

    private void UpdateUI()
    {
        if (enemyCounterText != null)
        {
            enemyCounterText.text = $"Map2 Enemies: {remainingEnemies}/{totalEnemies}";
        }
    }
}
