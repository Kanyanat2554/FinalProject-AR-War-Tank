using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.UI;

public class ImmediateQuit : MonoBehaviour
{
    [Header("AR Session Settings")]
    [SerializeField] private ARSession arSession; // �ҡ ARSession �ҡ Hierarchy ���������
    [SerializeField] private float quitDelay = 0.3f; // ������Ҫ�ҡ�͹�Դ��

    private void Awake()
    {
        // �һ����ѵ��ѵԶ������˹�� Inspector
        Button quitButton = GetComponent<Button>();
        if (quitButton != null)
        {
            quitButton.onClick.AddListener(QuitImmediately);
        }
        else
        {
            Debug.LogError("This script must be attached to a UI Button!");
        }

        // �� ARSession �ѵ��ѵԶ��������˹�
        if (arSession == null)
        {
            arSession = Object.FindFirstObjectByType<ARSession>();
        }
    }

    public void QuitImmediately()
    {
        Debug.Log("Quitting application...");

        // �Դ ARSession ��͹ (�����)
        if (arSession != null)
        {
            arSession.enabled = false;
            Debug.Log("ARSession disabled");
        }

        // ����硹���������� ARSession �Դ���ҧ��ʹ���
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
