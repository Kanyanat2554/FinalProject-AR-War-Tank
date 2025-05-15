using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartButton : MonoBehaviour
{
    public string sceneName = "Map1";  // ใส่ชื่อ Scene ที่ต้องการโหลดใหม่

    public void RestartGame()
    {
        SceneManager.LoadScene(sceneName);
    }
}