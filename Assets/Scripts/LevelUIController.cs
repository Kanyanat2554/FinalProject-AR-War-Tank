using UnityEngine;
using TMPro;

public class LevelUIController : MonoBehaviour
{
    [Header("UI Elements")]
    public TextMeshProUGUI levelText;
    public TextMeshProUGUI enemyCounterText;

    [SerializeField] private int currentLevel = 1;
    [SerializeField] private int totalEnemies = 3;
    [SerializeField] private int remainingEnemies = 3;

    private void Awake()
    {
        // ลงทะเบียนกับ UIManager ทันทีที่สร้าง
        if (UIManager.Instance != null)
        {
            UIManager.Instance.levelUI = this;
            Debug.Log("LevelUIController registered with UIManager");
        }

        // ย้ายการอัพเดต UI มาไว้ที่นี่แทน Start
        UpdateLevelDisplay();
        UpdateEnemyDisplay();

    }

    public void SetLevel(int level)
    {
        Debug.Log($"Setting level to: {level}");
        currentLevel = level;
        UpdateLevelDisplay();
    }

    public void SetEnemyCount(int total)
    {
        totalEnemies = total;
        remainingEnemies = total;
        UpdateEnemyDisplay();
    }

    public void EnemyDefeated()
    {
        remainingEnemies = Mathf.Max(0, remainingEnemies - 1);
        UpdateEnemyDisplay();
    }

    private void UpdateLevelDisplay()
    {
        if (levelText != null)
            levelText.text = $"Level: {currentLevel}";
    }

    private void UpdateEnemyDisplay()
    {
        if (enemyCounterText != null)
            enemyCounterText.text = $"Enemies: {remainingEnemies}/{totalEnemies}";
    }

    public void UpdateRemainingEnemies(int remaining)
    {
        remainingEnemies = Mathf.Clamp(remaining, 0, totalEnemies);
        UpdateEnemyDisplay();
    }

    public void ResetUI(int level, int enemies)
    {
        currentLevel = level;
        totalEnemies = enemies;
        remainingEnemies = enemies;
        UpdateLevelDisplay();
        UpdateEnemyDisplay();
    }
}

