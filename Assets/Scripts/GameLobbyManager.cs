using Assets.Scripts.NetworkClasses;
using Photon.Realtime;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[System.Serializable]
public class SpawnPointData
{
    public float x;
    public float y;
    public float z;
    public Color color;
}

public class GameLobbyManager : MonoBehaviour
{
    //UI elements we need
    public GameObject OpenToMultiplayerObj;
    public GameObject PlayMultiplayerButtonObj;
    public GameObject CancelCreateRoomButtonObj;
    public Text RoomNumberText;

    public List<Text> PlayerNameTextList = new List<Text>();

    //networking we need
    public GameObject LauncherObj;
    private GameLauncher gameLauncher;

    public GameObject RoomControllerObj;
    private NetworkingGameManager roomController;

    public string selectedGameMode;

    // Spawn point data
    public List<SpawnPointData> spawnPoints = new List<SpawnPointData>();

    public List<Image> PlayerColorList = new List<Image>();

    public List<Color> AvailableColors = new List<Color>();


    // Start is called before the first frame update
    void Start()
    {
        PlayMultiplayerButtonObj.SetActive(false);
        CancelCreateRoomButtonObj.SetActive(false);
        
        gameLauncher = LauncherObj.GetComponent<GameLauncher>();
        roomController = RoomControllerObj.GetComponent<NetworkingGameManager>();
    }

    void Update()
    {

    }

    public void QuitButton()
    {
        LeaveLobby();
        SceneManager.LoadScene("Menu");
    }
    public void LeaveLobby()
    {
        gameLauncher.LeaveRoom();
        gameLauncher.DisconnectFromGame();
    }

    public void PlayMultiplayerButton()
    {
        string sceneName;

        if (selectedGameMode == "GTD")
        {
            sceneName = "GTDScene"; // Replace with the actual scene name for GTD mode
        }
        else if (selectedGameMode == "GTCD")
        {
            sceneName = "GTCDScene"; // Replace with the actual scene name for GTCD mode
        }
        else
        {
            Debug.LogWarning("Unknown game mode! Setting scene name to default.");
            sceneName = "GTDScene"; // Set a default scene name
        }

        roomController.StartGame(sceneName);
    }


    public void OpenLobby()
    {
        // Change the UI
        CancelCreateRoomButtonObj.SetActive(true);
        PlayMultiplayerButtonObj.SetActive(false);

        // Connect to the server
        gameLauncher.Connect();

        // Assign colors to image components
        for (int i = 0; i < PlayerColorList.Count; i++)
        {
            // Check if there are available colors to assign
            if (i < AvailableColors.Count)
            {
                // Assign the color to the image component
                PlayerColorList[i].color = AvailableColors[i];
                PlayerColorList[i].gameObject.SetActive(true);
            }
            else
            {
                // If there are no more available colors, deactivate the image component
                PlayerColorList[i].gameObject.SetActive(false);
            }
        }
    }

    public void CancelRoomCreation()
    {
        CancelCreateRoomButtonObj.SetActive(false);
        gameLauncher.QuickCancel();
    }

    private void OnEnable()
    {
        GameLauncher.OnHasJoinedRoom += ShowReadyToPlay;
        GameLauncher.OnSomeoneJoined += ShowJoinedPlayers;

    }

    private void OnDisable()
    {
        GameLauncher.OnHasJoinedRoom -= ShowReadyToPlay;
        GameLauncher.OnSomeoneJoined -= ShowJoinedPlayers;
    }

    private void ShowReadyToPlay()
    {
        CancelCreateRoomButtonObj.SetActive(false);
        OpenToMultiplayerObj.SetActive(false);

        RoomNumberText.text = "Numéro de salle " + gameLauncher.CurrentRoomNumber;

        gameLauncher.progressLabel.GetComponentInChildren<Text>().text = "Connecte!\nEn attente d'un nombre suffisant de joueurs";

        if (gameLauncher.isMasterClient == false)
        {
            PlayMultiplayerButtonObj.GetComponent<Button>().interactable = false;
        }
    }

    private void ShowJoinedPlayers(Dictionary<int, Player> Players)
    {
        if (Players.Count < 1)
            return;

        Debug.Log("Trouvé " + Players.Count + " Joueur(s)");

        int index = 0;
        foreach (int id in Players.Keys)
        {
            if (index < PlayerNameTextList.Count && index < PlayerColorList.Count)
            {
                PlayerNameTextList[index].text = Players[id].NickName;

                // Set the color for the image component
                Color assignedColor = GetAssignedColor(Players[id]);
                PlayerColorList[index].color = assignedColor;

                index++;
            }
            else
            {
                Debug.LogWarning("Not enough UI elements to display all joined players");
                break;
            }
        }

        if (Players.Count >= 2)
        {
            EnoughPlayersAreIn();
        }
    }

    private Color GetAssignedColor(Player player)
    {
        // Implement your logic for assigning colors to players
        // This example assigns colors based on the player's actor number
        int colorIndex = player.ActorNumber % AvailableColors.Count;
        return AvailableColors[colorIndex];
    }

    private void EnoughPlayersAreIn()
    {
        PlayMultiplayerButtonObj.SetActive(true);

        gameLauncher.progressLabel.GetComponentInChildren<Text>().text = "Prêt à jouer !";
    }

}
