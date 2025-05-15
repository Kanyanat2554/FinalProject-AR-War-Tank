using UnityEngine;
using UnityEngine.UI;
/*
public class EnemyHealthBar : MonoBehaviour
{
    public Slider healthSliderPrefab;       // ลาก prefab Slider แบบ World Space
    public Vector3 healthBarOffset = new Vector3(0, 2f, 0);

    private Slider healthSlider;
    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;

        // หา Canvas แบบ World Space หรือสร้างใหม่ถ้าไม่มี
        Canvas canvas = FindOrCreateCanvas();

        if (healthSliderPrefab != null)
        {
            healthSlider = Instantiate(healthSliderPrefab, canvas.transform);
        }
        else
        {
            Debug.LogError("Health Slider Prefab is not assigned!");
        }
    }

    Canvas FindOrCreateCanvas()
    {
        Canvas canvas = Object.FindObjectOfType<Canvas>();
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

    public void SetMaxHealth(int max)
    {
        if (healthSlider != null)
        {
            healthSlider.maxValue = max;
            healthSlider.value = max;
        }
    }

    public void SetHealth(int current)
    {
        if (healthSlider != null)
        {
            healthSlider.value = current;
        }
    }

    void LateUpdate()
    {
        if (healthSlider != null && mainCamera != null)
        {
            // ทำให้แถบเลือดลอยเหนือศัตรู และหันเข้ากล้องเสมอ
            healthSlider.transform.position = transform.position + healthBarOffset;
            healthSlider.transform.rotation = mainCamera.transform.rotation;
        }
    }

    public void DestroyBar()
    {
        if (healthSlider != null)
        {
            Destroy(healthSlider.gameObject);
        }
    }

    private void OnDestroy()
    {
        DestroyBar();
    }
}
*/