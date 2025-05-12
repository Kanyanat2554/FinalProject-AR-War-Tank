using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Map3EnemyCounter : MonoBehaviour
{
    public static Map3EnemyCounter Instance;

    [Header("UI Settings")]
    public TextMeshProUGUI enemyCounterText;
    public string winSceneName = "Win";

    [SerializeField] private int totalEnemies;
    [SerializeField] private int remainingEnemies;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        if (enemyCounterText == null)
        {
            enemyCounterText = GameObject.Find("Map3EnemyCounterText")?.GetComponent<TextMeshProUGUI>();
        }
    }

    public void Initialize(int totalEnemies)
    {
        this.totalEnemies = totalEnemies;
        this.remainingEnemies = totalEnemies;
        UpdateUI();
    }

    public void EnemyKilled()
    {
        remainingEnemies--;
        UpdateUI();

        if (remainingEnemies <= 0)
        {
            Debug.Log("All enemies in Map3 defeated! Loading win scene...");
            Invoke("LoadWinScene", 2f); // โหลดซีน Win หลังจาก 2 วินาที
        }
    }

    private void UpdateUI()
    {
        if (enemyCounterText != null)
        {
            enemyCounterText.text = $"Map3 Enemies: {remainingEnemies}/{totalEnemies}";
        }
    }

    private void LoadWinScene()
    {
        SceneManager.LoadScene(winSceneName);
    }
}
