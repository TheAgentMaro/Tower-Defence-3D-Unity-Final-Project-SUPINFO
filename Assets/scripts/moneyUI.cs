using UnityEngine.UI;
using UnityEngine;

public class moneyUI : MonoBehaviour
{
    public Text moneyText;
    void Update()
    {
        moneyText.text ="Argent: " + playerstats.money.ToString();
    }
}
