using UnityEngine;
using UnityEngine.SceneManagement;
public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    
    private bool isMuted = false;

    [Header("UI")]
    public LevelUIController levelUI;

    [Header("Level Data")]
    public LevelData[] levelDatas;
    private int currentLevelIndex = 0;
    private int enemiesRemaining;
    private int totalEnemies;

    [Header("Map Control")]
    public ARMapAutoPlacer_Static mapPlacer;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        if (transform.parent == null)
        {
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Debug.LogWarning("⚠️ UIManager is not on a root GameObject. Not using DontDestroyOnLoad.");
        }
    }

    void Start()
    {
        if (levelDatas == null || levelDatas.Length == 0)
        {
            Debug.LogError("❌ levelDatas is empty! Please assign in Inspector.");
            return;
        }

        LoadLevel(0);
    }

    public void LoadLevel(int index)
    {
        if (index < 0 || index >= levelDatas.Length)
        {
            Debug.LogError($"Invalid level index: {index}");
            return;
        }

        currentLevelIndex = index;
        var data = levelDatas[index];
        totalEnemies = data.enemyCount;
        enemiesRemaining = data.enemyCount;

        if (mapPlacer != null)
        {
            mapPlacer.LoadLevelAtIndex(index);
        }

        if (levelUI != null)
        {
            levelUI.SetLevel(data.levelNumber);
            levelUI.UpdateEnemyCounter(enemiesRemaining, totalEnemies);
        }
    }

    public void EnemyDefeated()
    {
        enemiesRemaining--;
        if (levelUI != null)
            levelUI.UpdateEnemyCounter(enemiesRemaining, totalEnemies);

        if (enemiesRemaining <= 0)
        {
            if (currentLevelIndex >= levelDatas.Length - 1)
                mapPlacer.ShowWin();
            else
                LoadLevel(currentLevelIndex + 1);
        }
    }

    public void TriggerGameOver()
    {
        mapPlacer.ShowLose();
    }
    
    public void ResetGame()
    {
        currentLevelIndex = 0;
        LoadLevel(0);  // รีเซตด่านแรกใหม่
    }
    // --- ฟังก์ชันเพิ่ม ---

    // รีเซตเกม (ใช้กับ RestartButton)
    public void RestartGame()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }

    // Mute / Unmute เสียง
    public void ToggleMute()
    {
        isMuted = !isMuted;
        AudioListener.volume = isMuted ? 0f : 1f;
        Debug.Log(isMuted ? "Audio muted" : "Audio unmuted");
    }
}

[System.Serializable]
public class LevelData
{
    public int levelNumber;
    public int enemyCount;
}