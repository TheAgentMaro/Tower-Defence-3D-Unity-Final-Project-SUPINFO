using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public string level= "Game" ;
    public GameObject GoMakeUsernameObj;
    public Text WelcomeText;

    // Start is called before the first frame update
    void Start()
    {
        if (GameOptions.HasUsername() == false)
        {
            GoMakeUsernameObj.SetActive(true);
            //welcome text
            WelcomeText.text = "Bienvenue nouveau joueur !";
        }
        else
        {
            GoMakeUsernameObj.SetActive(false);
            WelcomeText.text = "Bienvenue à nouveau " + GameOptions.GetUsername() + "!";
        }
    }
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
        SceneManager.LoadScene("LobbyGame");
    }

    public void MenuButton()
    {
        SceneManager.LoadScene("Menu");
    }

    public void SettingsButton()
    {
        SceneManager.LoadScene("Parametre");
    }

    public void LevelsLobby()
    {
        SceneManager.LoadScene("LevelsLobby");
    }
    public void GTDButton()
    {
        SceneManager.LoadScene("GGame");
    }
    public void GCTDButton()
    {
        SceneManager.LoadScene("GCGame");
    }
    public void Level2()
    {
        SceneManager.LoadScene("Leveles/Level2");
    }
    public void Level3()
    {
        SceneManager.LoadScene("Level3");
    }
    public void Level4()
    {
        SceneManager.LoadScene("Level4");
    }
    public void Quit()
    {
        Application.Quit();

    }
}
