using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static bool end;
    public GameObject gameoverUI;
    void Start()
    {
        end = false;
    }
    void Update()
    {

        if (end)
        {
            return;
        }
        if(PlayerStats.lives<=0)
        {
            EndGame();
        }
    }
    public void EndGame()
    {
        gameoverUI.SetActive(true);
        end = true;
        
    }
}
