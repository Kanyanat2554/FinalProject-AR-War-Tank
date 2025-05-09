using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] public int maxHealth = 100;
    [SerializeField] Slider healthSlider;
    [SerializeField] string loseSceneName = "Lose";

    public int currentHealth;

    private void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthUI();
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        UpdateHealthUI();

        Debug.Log($"Player took {damage} damage. Current HP: {currentHealth}/{maxHealth}");

        if (currentHealth <= 0)
        {
            Debug.Log("Player died!");
            Die();
        }
    }

    public void UpdateHealthUI()
    {
        healthSlider.value = (float)currentHealth / maxHealth;
    }

    private void Die()
    {
        SceneManager.LoadScene(loseSceneName);
    }
}