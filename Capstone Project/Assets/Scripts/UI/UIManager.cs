using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private AudioClip gameOverSound;

    [Header("Pause")]
    [SerializeField] private GameObject pauseScreen;

    private void Awake()
    {
        gameOverScreen.SetActive(false);
        pauseScreen.SetActive(false);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            //If pause screen already active unpause and viceversa
            PauseGame(!pauseScreen.activeInHierarchy);
        }
    }
    #region  Game Over
    //Activate game over screen
    public void GameOver()
    {
        gameOverScreen.SetActive(true);
        SoundManager.instance.PlaySound(gameOverSound);
    }
    //Game over functions
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }
    public void Quit()
    {
        PlayerPrefs.DeleteKey("persistantHealth");
        Application.Quit(); //Quits the game (only works in build)
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; //Exits play mode
#endif
    }
    #endregion

    #region Pause
    public void PauseGame(bool status)
    {
        //if status == true pause | if status == false unpause
        pauseScreen.SetActive(status);

        //When puase status is true change timescale to 0 (time stops) and when it's false change it back to 1 (time resumes)
        if (status)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }
    public void SoundVolume()
    {
        SoundManager.instance.ChangeSoundVolume(.1f);
    }
    public void MusicVolume()
    {
        SoundManager.instance.ChangeMusicVolume(.1f);
    }
    #endregion
}
