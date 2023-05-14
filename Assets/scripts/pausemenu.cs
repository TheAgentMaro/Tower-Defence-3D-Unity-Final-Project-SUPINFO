using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PauseMenu : MonoBehaviour
{
   
    public GameObject ui;
    bool gamePause = false;
    public string Menu = "Menu";
    public string game = "Game";
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)|| Input.GetKeyDown(KeyCode.P))
        {
            Toggle();
        }
    }

    public  void Toggle()
    {
        

        if(gamePause)
        {
            Time.timeScale = 1f;
            ui.SetActive(false);
            gamePause = false;
        }
        else
        {
            Time.timeScale = 0f;
            ui.SetActive(true);
            gamePause = true;
        }
    }

    public void Retry()
    {
        Time.timeScale = 1f;
        ui.SetActive(false);
        SceneManager.LoadScene(game);
    }
    public void MainMenu()
    {
        Time.timeScale = 1f;
        ui.SetActive(false);
        SceneManager.LoadScene(Menu);
    }
}
