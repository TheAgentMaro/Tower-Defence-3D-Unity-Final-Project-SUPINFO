using UnityEngine;
using UnityEngine.SceneManagement;
public class mainMenu : MonoBehaviour
{
    public string level= "Game" ;

    public void Play()
    {
        SceneManager.LoadScene(level);
    }

    public void CreditsButton()
    {
        SceneManager.LoadScene("Crédits");
    }

    public void LobbyButton()
    {
        SceneManager.LoadScene("Lobby");
    }

    public void MenuButton()
    {
        SceneManager.LoadScene("Menu");
    }

    public void SettingsButton()
    {
        SceneManager.LoadScene("Parametre");
    }

    public void Quit()
    {
        Application.Quit();

    }
}
