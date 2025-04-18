using UnityEngine;
using UnityEngine.EventSystems;

public class UIButtonScaler : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private Vector3 originalScale;
    public float scaleFactor = 1.2f;

    private void Awake()
    {
        originalScale = transform.localScale;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        transform.localScale = originalScale * scaleFactor;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        transform.localScale = originalScale;
    }
}