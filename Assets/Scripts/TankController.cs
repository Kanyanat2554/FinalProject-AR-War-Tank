using UnityEngine;

public class TankController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float rotateSpeed = 60f;
    public Joystick joystick;
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float reloadTime = 3f;
    private int bulletsInMag = 10;
    private bool isReloading = false;

    void Update()
    {
        if (isReloading) return;

        float move = joystick.Vertical;
        float rotate = joystick.Horizontal;

        transform.Translate(Vector3.forward * move * moveSpeed * Time.deltaTime);
        transform.Rotate(Vector3.up * rotate * rotateSpeed * Time.deltaTime);
    }

    public void Fire()
    {
        if (isReloading || bulletsInMag <= 0) return;

        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        bulletsInMag--;

        if (bulletsInMag == 0)
        {
            isReloading = true;
            Invoke(nameof(Reload), reloadTime);
        }
    }

    void Reload()
    {
        bulletsInMag = 10;
        isReloading = false;
    }
}
