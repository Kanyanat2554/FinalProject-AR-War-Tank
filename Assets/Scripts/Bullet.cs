using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10f;
    public int damage = 10;

    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        Health hp = other.GetComponent<Health>();
        if (hp)
        {
            hp.TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
