using UnityEngine;
using UnityEngine.SceneManagement;

public class ARGameManager : MonoBehaviour
{
    public static ARGameManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void GameOver()
    {
        SceneManager.LoadScene("Lose");
    }

    public void Victory()
    {
        SceneManager.LoadScene("Win");
    }
}
