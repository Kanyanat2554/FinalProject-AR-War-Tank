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

    // Option Menu
    public GameObject optionPanel;

    // Level Settings
    public int totalEnemies;
    public int enemiesRemaining;

    // Level Data
    [SerializeField]
    private LevelData[] _levelDatas;
    public static LevelData[] levelDatas;
   
    public int currentLevelIndex = 0;

    // ========== INITIALIZATION ==========
    void Awake()
    {
        Debug.Log("UIManager Awake called");
        if (Instance == null)
        {
            Instance = this;
            levelDatas = _levelDatas; // �Ѵ�͡������仵���� static
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Debug.Log("Duplicate UIManager destroyed");
            Destroy(gameObject);
        }
        if (Instance != null && Instance != this)
        {
            Debug.Log($"Destroying duplicate UIManager in {gameObject.scene.name}");
            Destroy(gameObject);
            return; // �͡�ҡ��÷ӧҹ�ѹ��
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
        Debug.Log($"UIManager initialized in {gameObject.scene.name}");
    }

    void Start()
    {
        Debug.Log($"Initial Level Data: {levelDatas?.Length ?? 0} levels loaded");

        if (levelDatas != null && levelDatas.Length > 0)
        {
            Debug.Log($"First level enemies: {levelDatas[0].enemyCount}");
        }

        // ����������´�ҹ�á
        if (currentLevelIndex == 0 && levelDatas != null && levelDatas.Length > 0)
        {
            InitializeLevel(levelDatas[0].enemyCount, levelDatas[0].levelNumber);
        }
    }

    // ========== LEVEL MANAGEMENT ==========

    public LevelUIController levelUI;

    public void InitializeLevel(int enemyCount, int levelNum)
    {
        if (enemyCount <= 0)
        {
            Debug.LogError($"Invalid enemy count: {enemyCount} for level {levelNum}");
            return;
        }

        totalEnemies = enemyCount;
        enemiesRemaining = enemyCount;

        Debug.Log($"Level {levelNum} initialized with {totalEnemies} enemies");

        if (levelUI != null)
        {
            levelUI.SetLevel(levelNum);
            levelUI.SetEnemyCount(totalEnemies);
        }
        else
        {
            Debug.LogError("levelUI is not assigned!");
        }
    }

    public void EnemyDefeated()
    {
        // ��Ǩ�ͺ����ѧ���ѵ������������͹Ŵ���
        if (enemiesRemaining <= 0)
        {
            Debug.LogWarning("Attempted to decrement enemies when count is already 0");
            return;
        }

        enemiesRemaining--;

        // ��Ǩ�ͺ��� levelUI ����� null ��͹���¡��
        if (levelUI != null)
        {
            levelUI.UpdateRemainingEnemies(enemiesRemaining);
        }
        else
        {
            Debug.LogError("levelUI is null in UIManager!");
        }

        // ��Ǩ�ͺ���͹��������ҹ
        if (enemiesRemaining <= 0)
        {
            Debug.Log("All enemies defeated! Loading next level...");
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
            // ��Ŵ�չ��������ͼ�ҹ�ء��ҹ
            SceneManager.LoadScene("Win");
        }
    }

    public void LoadLevel(int levelIndex)
    {
        if (levelIndex < 0 || levelIndex >= levelDatas.Length)
        {
            Debug.LogError($"Invalid level index: {levelIndex}");
            return;
        }

        currentLevelIndex = levelIndex;
        var levelData = levelDatas[levelIndex];

        Debug.Log($"Loading level {levelData.levelNumber} with {levelData.enemyCount} enemies");

        StartCoroutine(LoadLevelAsync(levelData));
    }

    private IEnumerator LoadLevelAsync(LevelData levelData)
    {
        /*AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(levelData.sceneName);

        // �ͨ���з����Ŵ�չ�������
        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        // ��駤�Ҵ�ҹ��ѧ�ҡ��Ŵ�չ��������
        InitializeLevel(levelData.enemyCount, levelData.levelNumber);*/
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(levelData.sceneName);

        // �ͨ���з����Ŵ�չ�������
        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        // �����չ�����駤�ҷ���������
        yield return new WaitForEndOfFrame();

        // ��駤�Ҵ�ҹ��ѧ�ҡ��Ŵ�չ��������
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
            levelUI.UpdateRemainingEnemies(count); // ��ͧ���¡�ѧ��ѹ������س��ͧ����
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