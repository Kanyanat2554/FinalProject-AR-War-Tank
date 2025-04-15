using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    // HP Bar
    public Slider playerHPBar;
    public Slider enemyHPBar;

    // Text Elements
    public TextMeshProUGUI ammoText;
    public TextMeshProUGUI levelText;
    public TextMeshProUGUI enemyCounter;

    // Option Menu
    public GameObject optionPanel;

    // ========== INITIALIZATION ==========
    void Start()
    {
        //test
        SetAmmo(100);
        SetLevel("1");
        SetEnemyCount(3);
        SetPlayerHP(100, 100);
        SetEnemyHP(75, 100);
        
        if (optionPanel != null)
            optionPanel.SetActive(false);
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
            enemyCounter.text = "Enemies: " + count;
    }

    public void ToggleOptionPanel()
    {
        if (optionPanel != null)
            optionPanel.SetActive(!optionPanel.activeSelf);
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
        SceneManager.LoadScene("Map1");
    }
}