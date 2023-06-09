using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class GameOptions : MonoBehaviour
{

    #region Private Constants

    const string playerNamePrefKey = "PlayerName";
    const string isMultiplayerPrefKey = "IsMultiplayer";
    const string volumePrefKey = "Volume";

    #endregion

    public GameObject Panel;
    bool visible = false;

    public Dropdown resolutionDropdown;

    public AudioSource audioSource;
    public Slider volumeSlider;
    public Text VolumeTxt;

    //public Toggle muteToggle;

    private void Start()
    {
        SliderChanger();
        ReadSettingsFromFile();
    }
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            visible = !visible;
            Panel.SetActive(visible);
        }
        
    }

    //Resolution
    public void SetResolution()
    {
        switch (resolutionDropdown.value)
        {
            case 0:
                Screen.SetResolution(1366, 768, true);
                break;
            case 1:
                Screen.SetResolution(1920, 1080, true);
                break;
        }
        WriteSettingsToFile();
    }

    //Get User Data
    public void SliderChanger()
    {
        audioSource.volume = volumeSlider.value;
        VolumeTxt.text = "Volume " + (audioSource.volume * 100).ToString("00") + "%";
        WriteSettingsToFile();

    }

    public static bool HasUsername()
    {
        return PlayerPrefs.HasKey(playerNamePrefKey);
    }

    public static string GetUsername()
    {
        return PlayerPrefs.GetString(playerNamePrefKey, string.Empty);
    }

    public static void SetUsername(string newName)
    {
        newName = newName.Trim();
        if (newName.Equals(string.Empty))
            return;

        PlayerPrefs.SetString(playerNamePrefKey, newName);
    }


    #region Multiplayer Methods
    public static bool GetIsMultiplayer()
    {
        int isMultNum = PlayerPrefs.GetInt(isMultiplayerPrefKey);

        if (isMultNum == 1)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public static void SetIsMultiplayer(bool isMult)
    {
        int multNum;

        if (isMult)
            multNum = 1;
        else
            multNum = 0;

        PlayerPrefs.SetInt(isMultiplayerPrefKey, multNum);
    }

    #endregion

    private void OnApplicationQuit()
    {
        SetIsMultiplayer(false);
    }



    public InputField UserNameInput;

    public void SaveButton()
    {
        WriteSettingsToFile();
        SceneManager.LoadScene("Menu");
    }

    private void ReadSettingsFromFile()
    {
        //read in the user info
        if (HasUsername())
            UserNameInput.placeholder.GetComponent<Text>().text = GetUsername();

        //read in the volume
        if (PlayerPrefs.HasKey(volumePrefKey))
            audioSource.volume = PlayerPrefs.GetFloat(volumePrefKey);

        //read in the resolution setting
        if (PlayerPrefs.HasKey("Resolution"))
        {
            int resolutionIndex = PlayerPrefs.GetInt("Resolution");
            resolutionDropdown.value = resolutionIndex;
            SetResolution();
        }
    }

    public void WriteSettingsToFile()
    {
        SetUsername(UserNameInput.text);
        /*
        PlayerPrefs.SetInt("Resolution", resolutionDropdown.value);*/

        // Save volume
        PlayerPrefs.SetFloat("Volume", audioSource.volume);

        PlayerPrefs.Save();
    }
}
