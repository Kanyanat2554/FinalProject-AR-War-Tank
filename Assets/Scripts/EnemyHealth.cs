using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    [Header("Health Settings")]
    public int maxHealth = 100;
    private int currentHealth;
    private bool isDead = false;

    [Header("UI (Health Bar)")]
    public Slider healthSliderPrefab;
    public Vector3 healthBarOffset = new Vector3(0, 2f, 0);
    private Slider healthSlider;
    private Camera mainCamera;

    [Header("Effects")]
    public GameObject explosionEffect;

    void Start()
    {
        currentHealth = maxHealth;
        mainCamera = Camera.main;

        CreateHealthBar();
    }

    void CreateHealthBar()
    {
        Canvas canvas = FindOrCreateWorldCanvas();

        if (healthSliderPrefab != null)
        {
            healthSlider = Instantiate(healthSliderPrefab, canvas.transform);
            healthSlider.maxValue = maxHealth;
            healthSlider.value = currentHealth;
        }
        else
        {
            Debug.LogWarning("⚠️ Health Slider Prefab not assigned!");
        }
    }

    Canvas FindOrCreateWorldCanvas()
    {
        Canvas canvas = Object.FindFirstObjectByType<Canvas>(); 
        if (canvas == null || canvas.renderMode != RenderMode.WorldSpace)
        {
            GameObject canvasGO = new GameObject("WorldSpaceCanvas");
            canvas = canvasGO.AddComponent<Canvas>();
            canvas.renderMode = RenderMode.WorldSpace;
            canvasGO.AddComponent<CanvasScaler>();
            canvasGO.AddComponent<GraphicRaycaster>();
        }
        return canvas;
    }

    public void TakeDamage(int damage)
    {
        if (isDead) return;

        currentHealth -= damage;

        if (healthSlider != null)
            healthSlider.value = currentHealth;

        if (currentHealth <= 0)
            Die();
    }

    void LateUpdate()
    {
        if (healthSlider != null && mainCamera != null)
        {
            healthSlider.transform.position = transform.position + healthBarOffset;
            healthSlider.transform.rotation = mainCamera.transform.rotation;
        }
    }

    void Die()
    {
        isDead = true;

        if (UIManager.Instance != null)
            UIManager.Instance.EnemyDefeated();

        if (explosionEffect != null)
            Instantiate(explosionEffect, transform.position, Quaternion.identity);

        if (healthSlider != null)
            Destroy(healthSlider.gameObject);

        Destroy(gameObject);
    }

    void OnDestroy()
    {
        if (healthSlider != null)
            Destroy(healthSlider.gameObject);
    }
}
