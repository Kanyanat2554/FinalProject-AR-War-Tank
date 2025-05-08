using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;       // ตัวละคร (Player) ที่กล้องจะตาม
    public Vector3 offset;         // ระยะห่างกล้องจากตัวละคร
    public float smoothSpeed = 0.125f;  // ความนุ่มนวลในการตาม

    void LateUpdate()
    {
        if (target == null) return;

        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;
    }
}
