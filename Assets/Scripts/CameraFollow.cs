using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;       // ����Ф� (Player) �����ͧ�е��
    public Vector3 offset;         // ������ҧ���ͧ�ҡ����Ф�
    public float smoothSpeed = 0.125f;  // �����������㹡�õ��

    void LateUpdate()
    {
        if (target == null) return;

        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;
    }
}
