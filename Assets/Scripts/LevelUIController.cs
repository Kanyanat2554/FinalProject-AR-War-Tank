using UnityEngine;
using TMPro;

public class LevelUIController : MonoBehaviour
{
    [Header("UI Elements")]
    public TextMeshProUGUI levelText;
    public TextMeshProUGUI enemyCounterText;

    private void Awake()
    {
        if (UIManager.Instance != null)
        {
            UIManager.Instance.levelUI = this;
            Debug.Log("✅ LevelUIController registered");
        }
    }

    public void SetLevel(int level)
    {
        if (levelText != null)
            levelText.text = $"Level: {level}";
    }

    public void UpdateEnemyCounter(int remaining, int total)
    {
        if (enemyCounterText != null)
            enemyCounterText.text = $"Enemies: {remaining}/{total}";
    }
}