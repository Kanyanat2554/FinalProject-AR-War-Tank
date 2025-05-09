using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;


public class UIManager : MonoBehaviour
{
    // Singleton pattern
    public static UIManager Instance;

    // HP Bar
    public Slider playerHPBar;

    // Text Elements
    public TextMeshProUGUI ammoText;
    //public TextMeshProUGUI levelText;
    //public TextMeshProUGUI enemyCounter;

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

        // ตรวจสอบการเชื่อมโยง UI Elements
        //if (levelText == null) Debug.LogError("levelText is not assigned!");
        //if (enemyCounter == null) Debug.LogError("enemyCounter is not assigned!");

        // ตรวจสอบ Level Data
        if (levelDatas == null || levelDatas.Length == 0)
            Debug.LogError("levelDatas is not set up properly!");
    }

    // ========== LEVEL MANAGEMENT ==========

    public LevelUIController levelUI;

    public void InitializeLevel(int enemyCount, int levelNum)
    {
        totalEnemies = enemyCount;
        enemiesRemaining = enemyCount;

        if (levelUI != null)
        {
            levelUI.SetLevel(levelNum);
            levelUI.SetEnemyCount(totalEnemies);
        }

        Debug.Log($"Level {levelNum} initialized. Enemies: {enemiesRemaining}/{totalEnemies}");
    }

    public void EnemyDefeated()
    {
        enemiesRemaining--;
        if (levelUI != null)
            levelUI.EnemyDefeated();

        if (enemiesRemaining <= 0)
        {
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

        // ใช้ SceneManager.LoadScene แบบ asynchronous และตั้งค่าหลังจากโหลดเสร็จ
        StartCoroutine(LoadLevelAsync(levelData));
    }

    private IEnumerator LoadLevelAsync(LevelData levelData)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(levelData.sceneName);

        // รอจนกระทั่งโหลดซีนเสร็จสิ้น
        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        // ตั้งค่าด่านหลังจากโหลดซีนเสร็จแล้ว
        InitializeLevel(levelData.enemyCount, levelData.levelNumber);
    }

    // ========== UI FUNCTIONS ==========
    public void SetPlayerHP(float current, float max)
    {
        if (playerHPBar != null)
            playerHPBar.value = current / max;
    }


    public void SetAmmo(int value)
    {
        if (ammoText != null)
            ammoText.text = "Ammo: " + value;
    }

    public void SetLevel(int levelNumber)
    {
        if (levelUI != null)
            levelUI.SetLevel(levelNumber);
    }

    public void SetEnemyCount(int count)
    {
        if (levelUI != null)
        {
            // ตั้งค่าครั้งแรก
            levelUI.SetEnemyCount(totalEnemies);
        }
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