using UnityEngine.SceneManagement;

namespace Game.Utils
{
    public static class SceneLoader
    {
        private const int k_startMenuIndex = 0;

        public static void LoadStartMenu()
        {
            SceneManager.LoadScene(k_startMenuIndex);
        }

        public static void RestartLevel()
        {
            int buildIndex = SceneManager.GetActiveScene().buildIndex;

            SceneManager.LoadScene(buildIndex);
        }

        public static void LoadNextLevel()
        {
            int nextSceneBuildIndex = SceneManager.GetActiveScene().buildIndex + 1;

            SceneManager.LoadScene(nextSceneBuildIndex);
        }
    }
}
