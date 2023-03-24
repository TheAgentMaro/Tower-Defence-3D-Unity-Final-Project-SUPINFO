using UnityEngine;
using UnityEngine.SceneManagement;
public class mainMenu : MonoBehaviour
{
    public string level= "Game" ;
    public void Play()
    {
        SceneManager.LoadScene(level);
    }

    public void Quit()
    {
        Application.Quit();

    }
}
