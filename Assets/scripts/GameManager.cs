using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static bool end;
    public GameObject gameoverUI;
    void start()
    {
        end = false;
    }
    void Update()
    {

        if (end)
        {
            return;
        }
        if (Input.GetKeyDown("l"))
        {
            EndGame();
        }
        if(playerstats.lives<=0)
        {
            EndGame();
        }
    }
    void EndGame()
    {
        gameoverUI.SetActive(true);
        end = true;

    }
}
