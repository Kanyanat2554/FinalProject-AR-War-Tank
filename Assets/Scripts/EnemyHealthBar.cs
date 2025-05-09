using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour
{
    [Header("Health Settings")]
    public int maxHealth = 100;
    public int currentHealth;

    [Header("UI Settings")]
    public Slider healthSliderPrefab; // �ҡ Prefab Slider �ҡ Editor ���������
    public Vector3 healthBarOffset = new Vector3(0, 2f, 0);

    private Slider healthSlider;
    private Camera mainCamera;

    private void Start()
    {
        currentHealth = maxHealth;
        mainCamera = Camera.main;

        // ��Ǩ�ͺ��� Canvas �������������
        var canvas = FindCanvas();

        // ���ҧ Health Bar �ҡ Prefab
        if (healthSliderPrefab != null)
        {
            healthSlider = Instantiate(healthSliderPrefab, canvas.transform);
            healthSlider.minValue = 0;
            healthSlider.maxValue = maxHealth;
            healthSlider.value = currentHealth;
        }
        else
        {
            Debug.LogError("Health Slider Prefab is not assigned!");
        }
    }

    private Canvas FindCanvas()
    {
        // �� World Space Canvas ���������
        var canvas = Object.FindFirstObjectByType<Canvas>();
        if (canvas == null || canvas.renderMode != RenderMode.WorldSpace)
        {
            // ���ҧ Canvas �����������
            var canvasGO = new GameObject("WorldSpaceCanvas");
            canvas = canvasGO.AddComponent<Canvas>();
            canvas.renderMode = RenderMode.WorldSpace;
            canvasGO.AddComponent<CanvasScaler>();
            canvasGO.AddComponent<GraphicRaycaster>();
        }
        return canvas;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        UpdateHealth(currentHealth);

        if (currentHealth <= 0)
        {
            DestroyHealthBar();
            Destroy(gameObject);
        }
    }

    public void UpdateHealth(int health)
    {
        if (healthSlider != null)
        {
            healthSlider.value = health;
        }
    }

    private void LateUpdate()
    {
        if (healthSlider != null && mainCamera != null)
        {
            // �Ѿവ���˹� Health Bar
            healthSlider.transform.position = transform.position + healthBarOffset;
            healthSlider.transform.rotation = mainCamera.transform.rotation;
        }
    }

    private void DestroyHealthBar()
    {
        if (healthSlider != null)
        {
            Destroy(healthSlider.gameObject);
        }
    }

    private void OnDestroy()
    {
        DestroyHealthBar();
    }
}
