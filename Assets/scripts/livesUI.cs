using UnityEngine.UI;
using UnityEngine;

public class LivesUI : MonoBehaviour
{
    public Text livesText;
    void Update()
    {
        livesText.text = "Vie Restantes: " + PlayerStats.lives.ToString();
    }
}
