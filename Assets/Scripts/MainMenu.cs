using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void StartGame()
    {
        //PlayerPrefs.DeleteAll();

        SceneManager.LoadScene("Play");
    }
}
