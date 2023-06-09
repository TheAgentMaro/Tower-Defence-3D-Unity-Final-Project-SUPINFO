using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static bool end;
    public GameObject gameoverUI;
    public int totalWaves = 40;

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
        if (PlayerStats.waves > totalWaves)
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
