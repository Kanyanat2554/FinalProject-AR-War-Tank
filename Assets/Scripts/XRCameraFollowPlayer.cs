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

        // ��駤�ҵ��˹� XR Origin �������͹����� Player
        arSessionOrigin.MakeContentAppearAt(
            arSessionOrigin.transform,
            playerTransform.position,
            playerTransform.rotation
        );
    }
}
