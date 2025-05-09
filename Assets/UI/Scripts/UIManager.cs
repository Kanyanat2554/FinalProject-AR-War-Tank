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
    private int totalEnemies = 3; // จำนวนศัตรูทั้งหมดในด่าน 1
    private int enemiesRemaining;

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
        // Initialize enemy count for level 1
        InitializeLevel(3); // ตั้งค่าศัตรู 3 ตัวสำหรับด่าน 1

        if (optionPanel != null)
            optionPanel.SetActive(false);
    }

    // ========== LEVEL MANAGEMENT ==========
    public void InitializeLevel(int enemyCount)
    {
        totalEnemies = enemyCount;
        enemiesRemaining = enemyCount;
        SetEnemyCount(enemiesRemaining);
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
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex + 1);

        if (SceneManager.GetActiveScene().name == "Map2")
        {
            InitializeLevel(5); 
        }
    }

    public LevelData[] levelDatas;
    private int currentLevelIndex = 0;

    public void LoadLevel(int levelIndex)
    {
        currentLevelIndex = levelIndex;
        SceneManager.LoadScene(levelDatas[levelIndex].sceneName);
        InitializeLevel(levelDatas[levelIndex].enemyCount);
        SetLevel(levelDatas[levelIndex].levelName);
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

    public void SetLevel(string levelName)
    {
        if (levelText != null)
            levelText.text = "Level: " + levelName;
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
        SceneManager.LoadScene("Map1");
    }
}