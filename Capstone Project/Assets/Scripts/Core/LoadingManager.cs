using UnityEngine;
using UnityEngine.SceneManagement;

namespace Core
{
    public class LoadingManager : MonoBehaviour
    {
        private static LoadingManager Instance { get; set; }

        private void Awake()
        {
            //Keep this object even when we go to a new scene
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            //Destroy duplicate game objects
            else if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
            }
        }

        public void LoadCurrentLevel()
        {
            var currentLevel = PlayerPrefs.GetInt("currentLevel", 1);
            SceneManager.LoadScene(currentLevel);
            Time.timeScale = 1;
        }

        public void Restart()
        {
            //SceneManager.LoadScene(currentLevel);
        }
    }
}