using UnityEngine.UI;
using UnityEngine;

public class livesUI : MonoBehaviour
{
    public Text livesText;
    void Update()
    {
        livesText.text = "vie : " + playerstats.lives.ToString();
    }
}
