using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Health playerHealth;
    public Health[] enemies;

    void Update()
    {
        if (playerHealth.currentHP <= 0)
        {
            SceneManager.LoadScene("Lose");
        }

        bool allDead = true;
        foreach (var enemy in enemies)
        {
            if (enemy != null) allDead = false;
        }

        if (allDead)
        {
            string sceneName = SceneManager.GetActiveScene().name;
            if (sceneName == "Map5")
                SceneManager.LoadScene("Win");
            else
                Invoke(nameof(NextScene), 2f);
        }
    }

    void NextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
