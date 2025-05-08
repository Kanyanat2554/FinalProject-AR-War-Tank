using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] float patrolRange = 10f;
    [SerializeField] float detectionRange = 7f;
    [SerializeField] float shootingRange = 5f;
    [SerializeField] float timeBetweenShots = 2f;
    [SerializeField] Transform firePoint;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] float bulletForce = 100f;

    private NavMeshAgent agent;
    private Transform player;
    private Vector3 patrolPoint;
    private float shotTimer;

   
    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        agent.updateRotation = true;
    }
    private void Start()
    {
        StartCoroutine(InitializeAgent());
    }

    private void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= detectionRange)
        {
            agent.SetDestination(player.position);

            if (distanceToPlayer <= shootingRange)
            {
                Shoot();
            }
        }
        else
        {
            Patrol();
        }
    }

    private IEnumerator InitializeAgent()
    {
        // รอจนกว่า NavMesh จะพร้อม
        yield return new WaitForEndOfFrame();

        // พยายามวาง Agent บน NavMesh
        int attempts = 0;
        while (!agent.isOnNavMesh && attempts < 10)
        {
            agent.Warp(FindNearestNavMeshPosition());
            attempts++;
            yield return null;
        }

        if (agent.isOnNavMesh)
        {
            SetNewPatrolPoint();
        }
        else
        {
            Debug.LogError("Failed to place agent on NavMesh after 10 attempts");
        }
    }

    private Vector3 FindNearestNavMeshPosition()
    {
        NavMeshHit hit;
        if (NavMesh.SamplePosition(transform.position, out hit, patrolRange, NavMesh.AllAreas))
        {
            return hit.position;
        }
        return transform.position;
    }
    private void Patrol()
    {
        // ตรวจสอบว่า Agent อยู่บน NavMesh และมีเส้นทาง
        if (!agent.isOnNavMesh || agent.pathPending)
        {
            SetNewPatrolPoint();
            return;
        }

        // ตรวจสอบว่า Agent มีเส้นทางและระยะทางที่เหลือ
        if (agent.hasPath && agent.remainingDistance < 0.5f)
        {
            SetNewPatrolPoint();
        }
    }

    private void SetNewPatrolPoint()
    {
        Vector3 randomPoint = transform.position + Random.insideUnitSphere * patrolRange;
        randomPoint.y = transform.position.y;

        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomPoint, out hit, patrolRange, NavMesh.AllAreas))
        {
            patrolPoint = hit.position;
            if (agent.isOnNavMesh)
            {
                agent.SetDestination(patrolPoint);
            }
        }
    }

    private IEnumerator WaitUntilOnNavMesh()
    {
        while (!agent.isOnNavMesh)
        {
            yield return null; // รอ 1 frame
        }
        SetNewPatrolPoint();
    }

    private void Shoot()
    {
        shotTimer += Time.deltaTime;
        if (shotTimer >= timeBetweenShots)
        {
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            bullet.GetComponent<Rigidbody>().AddForce(firePoint.forward * bulletForce, ForceMode.Impulse);
            shotTimer = 0f;
        }
    }
}