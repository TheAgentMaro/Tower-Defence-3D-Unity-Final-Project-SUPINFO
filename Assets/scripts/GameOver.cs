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
    public void retry()
    {
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex,LoadSceneMode.Single);
    }
    public void menu()
    {
        SceneManager.LoadSceneAsync(Menu,LoadSceneMode.Single);
    }
}
