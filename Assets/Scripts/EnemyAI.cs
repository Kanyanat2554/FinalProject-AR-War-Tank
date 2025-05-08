using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public float patrolSpeed = 3f;
    public Transform[] patrolPoints;
    private int currentPoint = 0;

    public GameObject bulletPrefab;
    public Transform firePoint;
    public float fireCooldown = 2f;
    private float fireTimer;

    void Update()
    {
        Patrol();

        fireTimer += Time.deltaTime;
    }

    void Patrol()
    {
        if (patrolPoints.Length == 0) return;

        Transform target = patrolPoints[currentPoint];
        transform.position = Vector3.MoveTowards(transform.position, target.position, patrolSpeed * Time.deltaTime);
        if (Vector3.Distance(transform.position, target.position) < 0.5f)
        {
            currentPoint = (currentPoint + 1) % patrolPoints.Length;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && fireTimer >= fireCooldown)
        {
            Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            fireTimer = 0f;
        }
    }
}
