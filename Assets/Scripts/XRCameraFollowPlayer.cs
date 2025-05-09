using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class XRCameraFollowPlayer : MonoBehaviour
{
    [SerializeField] private Transform playerTransform; 
    [SerializeField] private float followSpeed = 5f;

    private XROrigin arSessionOrigin;
    private Camera xrCamera;

    private void Awake()
    {
        arSessionOrigin = FindObjectOfType<ARSessionOrigin>();
        xrCamera = arSessionOrigin.camera;
    }

    private void Update()
    {
        if (playerTransform == null) return;

        // ตั้งค่าตำแหน่ง XR Origin ให้เคลื่อนที่ตาม Player
        arSessionOrigin.MakeContentAppearAt(
            arSessionOrigin.transform,
            playerTransform.position,
            playerTransform.rotation
        );
    }
}
