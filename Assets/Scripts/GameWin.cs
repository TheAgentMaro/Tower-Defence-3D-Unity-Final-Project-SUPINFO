using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameWin : MonoBehaviour
{
    public Text roundsText;
    public string Menu = "Menu";
    void OnEnable()
    {
        roundsText.text = PlayerStats.rounds.ToString() + " Rounds Surv�cu";
    }
    public void Retry()
    {
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
    }

    public void LevelLobby()
    {
        SceneManager.LoadScene("LevelsLobby");
    }

    public void LevelGTDLobby()
    {
        SceneManager.LoadScene("LobbyGameGreen");
    }

    public void LevelGCTDLobby()
    {
        SceneManager.LoadScene("LobbyGameGreenCircle");
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(Menu, LoadSceneMode.Single);
    }
}