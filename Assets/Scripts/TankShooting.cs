using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class TankShooting : MonoBehaviour
{
    [SerializeField] Transform firePoint;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] float bulletForce = 100f;
    [SerializeField] int magazineSize = 10;
    [SerializeField] float reloadTime = 5f;
    [SerializeField] Button fireButton;
    [SerializeField] private TextMeshProUGUI ammoText;

    private int currentAmmo;
    private bool isReloading = false;

    private void Start()
    {
        currentAmmo = magazineSize;
        UpdateAmmoUI();
        fireButton.onClick.AddListener(Shoot);
    }

    private void Update()
    {
        if (isReloading) return;

        if (currentAmmo <= 0)
        {
            StartCoroutine(Reload());
        }
    }

    private void Shoot()
    {
        if (isReloading || currentAmmo <= 0) return;

        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        bullet.GetComponent<Rigidbody>().AddForce(firePoint.forward * bulletForce, ForceMode.Impulse);
        currentAmmo--;
        UpdateAmmoUI();
    }

    private IEnumerator Reload()
    {
        isReloading = true;
        yield return new WaitForSeconds(reloadTime);
        currentAmmo = magazineSize;
        isReloading = false;
        UpdateAmmoUI();
    }

    private void UpdateAmmoUI()
    {
        ammoText.text = $"Ammo : {currentAmmo}/{magazineSize}";
    }
}
