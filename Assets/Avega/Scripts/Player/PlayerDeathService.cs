using UnityEngine.SceneManagement;

namespace Avega.Player
{
    public class PlayerDeathService
    {
        public PlayerDeathService(Health playerHealth)
        {
            playerHealth.Empty += OnPlayerHealthEmpty;
        }

        private void OnPlayerHealthEmpty()
        {
            SceneManager.LoadScene("Main");
        }
    }
}