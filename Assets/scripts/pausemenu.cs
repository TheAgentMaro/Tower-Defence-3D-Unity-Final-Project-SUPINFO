using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
public class pausemenu : MonoBehaviour
{
   
    public GameObject ui;
    bool paused = false;
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
        

        if(paused)
        {
            Time.timeScale = 1f;
            ui.SetActive(false);
            paused = false;
        }
        else
        {
            Time.timeScale = 0f;
            ui.SetActive(true);
            paused = true;
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
