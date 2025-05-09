using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] public int maxHealth = 100;
    [SerializeField] private int currentHealth;
    public GameObject explosionEffect;
    public EnemyHealthBar healthBar;

    private void Start()
    {
        currentHealth = maxHealth;

        // ตรวจสอบว่า healthBar มี Component EnemyHealthBar หรือไม่
        healthBar = GetComponent<EnemyHealthBar>();
        if (healthBar == null)
        {
            healthBar = gameObject.AddComponent<EnemyHealthBar>();
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
        if (explosionEffect != null)
        {
            Instantiate(explosionEffect, transform.position, transform.rotation);
        }

        UIManager.Instance?.EnemyDefeated();
        Destroy(gameObject);
    }
}
