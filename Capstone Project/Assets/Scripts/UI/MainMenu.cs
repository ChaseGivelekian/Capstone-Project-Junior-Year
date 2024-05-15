using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        Time.timeScale = 1;
    }
    public void QuitGame()
    {
        PlayerPrefs.DeleteKey("persistantHealth");
        Application.Quit(); //Quits the game (only works in build)
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; //Exits play mode
#endif
    }
}
