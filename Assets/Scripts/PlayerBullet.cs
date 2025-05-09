using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    [SerializeField] int damageAmount = 10;
    [SerializeField] float speed = 20f;
    [SerializeField] float lifetime = 3f;

    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
        rb.collisionDetectionMode = CollisionDetectionMode.Continuous;

        // ����¡���ع�ѵ��ѵ���ѧ�ҡ���Ҽ�ҹ�
        Destroy(gameObject, lifetime);
    }

    private void FixedUpdate()
    {
        // ����͹������� Physics
        rb.MovePosition(transform.position + transform.forward * speed * Time.fixedDeltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            EnemyHealth enemyHealth = collision.gameObject.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(damageAmount);
            }
            Destroy(gameObject);
        }
        else
        {
            // ����¡���ع����ͪ����á����������� Enemy
            Destroy(gameObject);
        }
    }
}
