using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.UI;

public class ImmediateQuit : MonoBehaviour
{
    [Header("AR Session Settings")]
    [SerializeField] private ARSession arSession; // ลาก ARSession จาก Hierarchy มาใส่ที่นี่
    [SerializeField] private float quitDelay = 0.3f; // ความล่าช้าก่อนปิดเกม

    private void Awake()
    {
        // หาปุ่มอัตโนมัติถ้าไม่กำหนดใน Inspector
        Button quitButton = GetComponent<Button>();
        if (quitButton != null)
        {
            quitButton.onClick.AddListener(QuitImmediately);
        }
        else
        {
            Debug.LogError("This script must be attached to a UI Button!");
        }

        // หา ARSession อัตโนมัติถ้าไม่ได้กำหนด
        if (arSession == null)
        {
            arSession = Object.FindFirstObjectByType<ARSession>();
        }
    }

    public void QuitImmediately()
    {
        Debug.Log("Quitting application...");

        // ปิด ARSession ก่อน (ถ้ามี)
        if (arSession != null)
        {
            arSession.enabled = false;
            Debug.Log("ARSession disabled");
        }

        // รอเล็กน้อยเพื่อให้ ARSession ปิดอย่างปลอดภัย
        Invoke("ForceQuit", quitDelay);
    }

    private void ForceQuit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
