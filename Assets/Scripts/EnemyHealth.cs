using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] public int maxHealth = 100;
    [SerializeField] private int currentHealth;
    public GameObject explosionEffect;
    public EnemyHealthBar healthBar;

    private bool isDead = false;

    private void Start()
    {
        currentHealth = maxHealth;

        // ��Ǩ�ͺ��� healthBar �� Component EnemyHealthBar �������
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
        if (isDead) return;

        isDead = true; // ��駤�� isDead �� true ��͹�����ҧ���

        // �� UIManager ����ѵ�ٶ١�����
        if (UIManager.Instance != null)
        {
            UIManager.Instance.EnemyDefeated();
        }
        else
        {
            Debug.LogError("UIManager.Instance is null!");
        }

        // ���ҧ�Ϳ࿡�����Դ (�����)
        if (explosionEffect != null)
        {
            Instantiate(explosionEffect, transform.position, transform.rotation);
        }

        // ������ѵ��
        Destroy(gameObject);
    }
}
