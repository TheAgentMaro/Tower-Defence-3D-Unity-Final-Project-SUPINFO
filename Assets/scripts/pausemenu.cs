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
            toggle();
        }
    }

    public  void toggle()
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

    public void retry()
    {
        Time.timeScale = 1f;
        ui.SetActive(false);
        SceneManager.LoadScene(game);
    }
    public void menu()
    {
        Time.timeScale = 1f;
        ui.SetActive(false);
        SceneManager.LoadScene(Menu);
    }
}
