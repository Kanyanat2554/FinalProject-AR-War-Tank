using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] public int maxHealth = 100;
    [SerializeField] private int currentHealth;

    public GameObject explosionEffect;
    public EnemyHealthBar healthBar;

    [SerializeField] private AudioClip deathSound;
    [SerializeField][Range(0, 1)] private float deathSoundVolume = 0.7f;

    private bool isDead = false;

    private void Start()
    {
        currentHealth = maxHealth;

        if (healthBar == null)
        {
            healthBar = GetComponent<EnemyHealthBar>();
            if (healthBar == null)
            {
                healthBar = gameObject.AddComponent<EnemyHealthBar>();
            }
        }

        healthBar.maxHealth = maxHealth;
        healthBar.currentHealth = currentHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.TakeDamage(damage);

        Debug.Log($"Enemy ({gameObject.name}) took {damage} damage. Current HP: {currentHealth}");

        if (currentHealth <= 0)
        {
            Debug.Log($"Enemy ({gameObject.name}) died!");
            Die();
        }
    }

    private void Die()
    {
        if (isDead) return;
        isDead = true;

        string currentScene = SceneManager.GetActiveScene().name;

        if (deathSound != null)
        {
            AudioSource.PlayClipAtPoint(deathSound, transform.position, deathSoundVolume);
        }

        if (currentScene == "Map3")
        {
            if (Map3EnemyCounter.Instance != null)
            {
                Map3EnemyCounter.Instance.EnemyKilled();
            }
        }
        else if (currentScene == "Map2")
        {
            if (Map2EnemyCounter.Instance != null)
            {
                Map2EnemyCounter.Instance.EnemyKilled();
            }
        }
        else
        {
            // ระบบเดิมสำหรับ Map1
            if (UIManager.Instance != null)
            {
                UIManager.Instance.EnemyDefeated();
            }
        }

        if (explosionEffect != null)
        {
            Instantiate(explosionEffect, transform.position, transform.rotation);
        }
        Destroy(gameObject);
    }
}
