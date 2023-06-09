using UnityEngine.UI;
using UnityEngine;

public class WavesUI : MonoBehaviour
{
    public Text wavesText;
    void Update()
    {
        wavesText.text = "Vague Numéro : " + PlayerStats.waves.ToString();
    }
}
