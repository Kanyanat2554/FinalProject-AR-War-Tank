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
    [SerializeField] private TextMeshProUGUI ammotimeText;
    
    [SerializeField] private LineRenderer aimLine;
    [SerializeField] private float aimDistance = 50f;
    [SerializeField] private Color defaultColor = Color.green;
    [SerializeField] private Color targetColor = Color.red;


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

        UpdateAimLine();
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

        float timeRemaining = reloadTime;
        while (timeRemaining > 0f)
        {
            UpdateAimLine();
            ammotimeText.text = $"<color=#FF9900>Reloading: {timeRemaining:F1}s</color>";
            timeRemaining -= Time.deltaTime;
            yield return null;
        }

        currentAmmo = magazineSize;
        isReloading = false;

        UpdateAmmoUI();
    }

    private void UpdateAmmoUI()
    {
        ammoText.text = $"Ammo : {currentAmmo}/{magazineSize}";
        if (!isReloading)
            ammotimeText.text = "<color=#00FF00>Ready</color>";
    }
    
    private void UpdateAimLine()
    {
        if (firePoint == null || aimLine == null) return;

        Ray ray = new Ray(firePoint.position, firePoint.forward);
        RaycastHit hit;

        Vector3 endPoint = firePoint.position + firePoint.forward * aimDistance;

        if (Physics.Raycast(ray, out hit, aimDistance))
        {
            endPoint = hit.point;

            if (hit.collider.CompareTag("Enemy"))
            {
                aimLine.material.color = targetColor;
            }
            else
            {
                aimLine.material.color = defaultColor;
            }
        }
        else
        {
            aimLine.material.color = defaultColor;
        }

        aimLine.SetPosition(0, firePoint.position);
        aimLine.SetPosition(1, endPoint);
    }

}
