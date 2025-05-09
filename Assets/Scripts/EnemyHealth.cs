using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EnemyHealth : MonoBehaviour
{
    [Header("Health Settings")]
    [SerializeField] int maxHealth = 100;
    [SerializeField] private Slider healthSlider;
    [SerializeField] private Vector3 healthBarOffset = new Vector3(0, 2f, 0);
    [SerializeField] GameObject explosionEffect;

    private int currentHealth;

    private void Start()
    {
        currentHealth = maxHealth;

        if (healthSlider != null)
        {
            healthSlider.transform.SetParent(null);
            healthSlider.gameObject.SetActive(true);
            UpdateHealthUI();
        }
    }

    private void Update()
    {
        if (healthSlider != null && healthSlider.gameObject != null &&
            Camera.main != null && gameObject != null)
        {
            healthSlider.transform.position = transform.position + healthBarOffset;
            healthSlider.transform.rotation = Camera.main.transform.rotation;
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        ShowHealthBar();
        UpdateHealthUI();

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void UpdateHealthUI()
    {
        if (healthSlider != null)
        {
            healthSlider.value = (float)currentHealth / maxHealth;
        }
    }

    private void Die()
    {
        if (explosionEffect != null)
        {
            Instantiate(explosionEffect, transform.position, Quaternion.identity);
        }

        if (GameManager.Instance != null)
        {
            GameManager.Instance.EnemyDefeated();
        }

        if (healthSlider != null && healthSlider.gameObject != null)
        {
            Destroy(healthSlider.gameObject);
        }

        Destroy(gameObject);
    }

    private IEnumerator HideHealthBar()
    {
        yield return new WaitForSeconds(1f);

        if (healthSlider != null)
        {
            healthSlider.gameObject.SetActive(false);
        }
    }

    public void ShowHealthBar()
    {
        if (healthSlider != null)
        {
            healthSlider.gameObject.SetActive(true);
            StopAllCoroutines();
            StartCoroutine(HideHealthBar());
        }
    }
}
