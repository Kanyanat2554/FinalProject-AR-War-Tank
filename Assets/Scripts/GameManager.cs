using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] string winSceneName = "Win";
    [SerializeField] int totalLevels = 3;

    private int currentLevel = 1;
    private int enemiesInLevel;

    private void Awake()
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

    public void RegisterEnemy()
    {
        enemiesInLevel++;
    }

    public void EnemyDefeated()
    {
        enemiesInLevel--;

        if (enemiesInLevel <= 0)
        {
            if (currentLevel >= totalLevels)
            {
                SceneManager.LoadScene(winSceneName);
            }
            else
            {
                currentLevel++;
                Invoke("LoadNextLevel", 2f);
            }
        }
    }

    private void LoadNextLevel()
    {
        SceneManager.LoadScene($"Level{currentLevel}");
    }
}
