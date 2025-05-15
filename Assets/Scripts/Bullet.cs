using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damage = 10;
    public float lifetime = 3f;

    private void Start()
    {
        Destroy(gameObject, lifetime);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"Bullet hit: {other.gameObject.name} (Tag: {other.tag})");

        if (other.CompareTag("Enemy"))
        {
            // ปลอดภัย: ตรวจสอบว่ามี EnemyHealth ก่อน
            EnemyHealth enemy = other.GetComponentInParent<EnemyHealth>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
                Debug.Log($"Enemy {enemy.name} took {damage} damage");
            }
            else
            {
                Debug.LogWarning("⚠️ EnemyHealth script not found on hit object.");
            }

            Destroy(gameObject);
        }
        else if (other.CompareTag("Player"))
        {
            PlayerHealth player = other.GetComponent<PlayerHealth>();
            if (player != null)
            {
                player.TakeDamage(damage);
                Debug.Log($"Player took {damage} damage");
            }

            Destroy(gameObject);
        }
    }
}