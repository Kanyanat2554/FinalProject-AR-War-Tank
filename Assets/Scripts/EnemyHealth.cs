using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyHealth : MonoBehaviour
{
    public int health = 50;
    public GameObject explosionEffect;

    public void TakeDamage(int damage)
    {
        health -= damage;

        Debug.Log($"Enemy ({gameObject.name}) took {damage} damage. Current HP: {health}");

        if (health <= 0)
        {
            Debug.Log($"Enemy ({gameObject.name}) died!");
            Die();
        }
    }

    private void Die()
    {
        if (explosionEffect != null)
        {
            Instantiate(explosionEffect, transform.position, transform.rotation);
        }

        Destroy(gameObject);

        // ตรวจสอบว่าศัตรูทั้งหมดถูกทำลายหรือไม่
        CheckAllEnemiesDestroyed();
    }

    private void CheckAllEnemiesDestroyed()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        if (enemies.Length <= 1) // เพราะตัวเองยังไม่ถูกทำลายทันที
        {
            Invoke("LoadNextLevel", 2f);
        }
    }

    private void LoadNextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        if (currentSceneIndex < SceneManager.sceneCountInBuildSettings - 3) // -3 เพราะมี Scene Win, Lose, MainMenu
        {
            SceneManager.LoadScene(currentSceneIndex + 1);
        }
        else
        {
            SceneManager.LoadScene("Win");
        }
    }
}
