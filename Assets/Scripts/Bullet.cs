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
            Debug.Log($"Enemy hit! Dealing {damage} damage");
            other.GetComponent<EnemyHealth>().TakeDamage(damage);
            Destroy(gameObject);
        }
        else if (other.CompareTag("Player"))
        {
            Debug.Log($"Player hit! Dealing {damage} damage");
            other.GetComponent<PlayerHealth>().TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
