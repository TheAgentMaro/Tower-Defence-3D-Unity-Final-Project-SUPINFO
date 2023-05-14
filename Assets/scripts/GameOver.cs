using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameOver : MonoBehaviour
{
    public Text roundsText;
    public string Menu="Menu";
    void OnEnable()
    {
        roundsText.text = PlayerStats.rounds.ToString()+ " Rounds Survécu";
    }
    public void Retry()
    {
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex,LoadSceneMode.Single);
    }
    public void MainMenu()
    {
        SceneManager.LoadSceneAsync(Menu,LoadSceneMode.Single);
    }
}
