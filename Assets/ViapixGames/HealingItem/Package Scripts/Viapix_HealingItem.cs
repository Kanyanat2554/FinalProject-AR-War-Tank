using UnityEngine;

namespace Viapix_HealingItem
{
    public class Viapix_HealingItem : MonoBehaviour
    {
        [SerializeField]
        float rotationSpeedX, rotationSpeedY, rotationSpeedZ;

        [SerializeField]
        int minHealAmount = 20;
        [SerializeField]
        int maxHealAmount = 50;

        GameObject playerObj;
        PlayerHealth playerHealth;

        private void Start()
        {
            var player = GameObject.FindGameObjectWithTag("Player");
            if (player == null) Debug.LogError("Player not found!");

            playerHealth = player?.GetComponent<PlayerHealth>();
            if (playerHealth == null) Debug.LogError("PlayerHealth component missing!");
        }

        void Update()
        {
            transform.Rotate(rotationSpeedX, rotationSpeedY, rotationSpeedZ);
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (!collision.gameObject.CompareTag("Player")) return;

            // ดึง Component ใหม่ทุกครั้งเพื่อป้องกัน Reference หาย
            PlayerHealth health = collision.gameObject.GetComponent<PlayerHealth>();
            if (health == null)
            {
                Debug.LogError("PlayerHealth component missing on player!");
                return;
            }

            int randomHeal = Random.Range(minHealAmount, maxHealAmount + 1);
            health.currentHealth = Mathf.Min(health.currentHealth + randomHeal, health.maxHealth);
            health.UpdateHealthUI();

            Destroy(gameObject);
            Debug.Log($"Healed: +{randomHeal} HP!");
        }
    }
}

