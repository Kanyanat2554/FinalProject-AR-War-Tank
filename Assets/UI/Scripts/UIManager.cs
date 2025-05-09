using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;


public class UIManager : MonoBehaviour
{
    // Singleton pattern
    public static UIManager Instance;

    // HP Bar
    public Slider playerHPBar;
    public Slider enemyHPBar;

    // Text Elements
    public TextMeshProUGUI ammoText;
    public TextMeshProUGUI levelText;
    public TextMeshProUGUI enemyCounter;

    // Option Menu
    public GameObject optionPanel;

    // Level Settings
    private int totalEnemies;
    private int enemiesRemaining;

    // Level Data
    public LevelData[] levelDatas;
    private int currentLevelIndex = 0;

    // ========== INITIALIZATION ==========
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        if (optionPanel != null)
            optionPanel.SetActive(false);
    }

    // ========== LEVEL MANAGEMENT ==========
    public void InitializeLevel(int enemyCount, int levelNum)
    {
        totalEnemies = enemyCount;
        enemiesRemaining = enemyCount;
        SetEnemyCount(enemiesRemaining);
        SetLevel(levelNum);
    }

    public void EnemyDefeated()
    {
        enemiesRemaining--;
        SetEnemyCount(enemiesRemaining);

        if (enemiesRemaining <= 0)
        {
            // เปลี่ยนด่านหลังจาก 2 วินาที
            Invoke("LoadNextLevel", 2f);
        }
    }

    private void LoadNextLevel()
    {
        currentLevelIndex++;

        if (currentLevelIndex < levelDatas.Length)
        {
            LoadLevel(currentLevelIndex);
        }
        else
        {
            // โหลดซีนจบเกมเมื่อผ่านทุกด่าน
            SceneManager.LoadScene("Win");
        }
    }

    public void LoadLevel(int levelIndex)
    {
        if (levelIndex < 0 || levelIndex >= levelDatas.Length)
        {
            Debug.LogError("Invalid level index!");
            return;
        }

        currentLevelIndex = levelIndex;
        var levelData = levelDatas[levelIndex];
        SceneManager.LoadScene(levelData.sceneName);
        InitializeLevel(levelData.enemyCount, levelData.levelNumber);
    }

    // ========== UI FUNCTIONS ==========
    public void SetPlayerHP(float current, float max)
    {
        if (playerHPBar != null)
            playerHPBar.value = current / max;
    }

    public void SetEnemyHP(float current, float max)
    {
        if (enemyHPBar != null)
            enemyHPBar.value = current / max;
    }

    public void SetAmmo(int value)
    {
        if (ammoText != null)
            ammoText.text = "Ammo: " + value;
    }

    public void SetLevel(int levelNumber)
    {
        if (levelText != null)
            levelText.text = "Level: " + levelNumber;
    }

    public void SetEnemyCount(int count)
    {
        if (enemyCounter != null)
            enemyCounter.text = "Enemies: " + count + "/" + totalEnemies;
    }

    // ========== OPTION MENU FUNCTIONS ==========
    public void ToggleOptionPanel()
    {
        if (optionPanel != null)
        {
            optionPanel.SetActive(!optionPanel.activeSelf);
            Time.timeScale = optionPanel.activeSelf ? 0f : 1f;
        }
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
        if (optionPanel != null)
            optionPanel.SetActive(false);
    }

    public void MuteGame()
    {
        AudioListener.volume = (AudioListener.volume > 0f) ? 0f : 1f;
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quit Game");
    }

    public void StartGame()
    {
        Time.timeScale = 1f;
        LoadLevel(0);
    }
}