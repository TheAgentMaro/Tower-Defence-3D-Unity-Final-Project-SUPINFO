using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameOver : MonoBehaviour
{
    public Text roundsText;
    public string Menu="Menu";
    void OnEnable()
    {
        roundsText.text = playerstats.rounds.ToString()+" Rounds Survived";
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
