using UnityEngine;
using TMPro;

public class Map2EnemyCounter : MonoBehaviour
{
    public static Map2EnemyCounter Instance; // Singleton ����Ѻ Map2 ��੾��

    [Header("UI Elements")]
    public TextMeshProUGUI enemyCounterText;

    [SerializeField] private int totalEnemies;
    [SerializeField] private int remainingEnemies;

    private void Awake()
    {
        // Singleton Pattern ੾������Ѻ Map2
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        // �� UI �ѵ��ѵԶ���������� Manual
        if (enemyCounterText == null)
        {
            enemyCounterText = GameObject.Find("Map2EnemyCounterText")?.GetComponent<TextMeshProUGUI>();
            if (enemyCounterText == null)
            {
                Debug.LogError("Map2 Enemy Counter Text not found!");
            }
        }
    }

    // ���¡�ҡ Enemy � Map2 ����Ͷ١�����
    public void EnemyKilled()
    {
        remainingEnemies--;
        UpdateUI();

        if (remainingEnemies <= 0)
        {
            Debug.Log("All enemies in Map2 defeated!");
            // ���� Logic ����������� Map2 �ç���
        }
    }

    // ��駤�Ҩӹǹ�ѵ���������
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
