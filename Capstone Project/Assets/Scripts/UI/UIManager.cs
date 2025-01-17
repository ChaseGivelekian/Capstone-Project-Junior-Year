using Core;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private GameObject gameOverScreen;
        [SerializeField] private AudioClip gameOverSound;

        [Header("Pause")] [SerializeField] private GameObject pauseScreen;

        private void Awake()
        {
            gameOverScreen.SetActive(false);
            pauseScreen.SetActive(false);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                //If the pause screen is active, unpause the game and vice versa
                PauseGame(!pauseScreen.activeInHierarchy);
            }
        }

        #region Game Over

        //Activate game over screen
        public void GameOver()
        {
            gameOverScreen.SetActive(true);
            SoundManager.Instance.PlaySound(gameOverSound);
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

            //When pause status is true, change timescale to 0 (time stops) and when it's false change it back to 1 (time resumes)
            Time.timeScale = status ? 0 : 1;
        }

        public void SoundVolume()
        {
            SoundManager.Instance.ChangeSoundVolume(.1f);
        }

        public void MusicVolume()
        {
            SoundManager.Instance.ChangeMusicVolume(.1f);
        }

        #endregion
    }
}