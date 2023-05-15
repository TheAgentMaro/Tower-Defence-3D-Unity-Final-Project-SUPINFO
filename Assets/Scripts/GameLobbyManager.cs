using Assets.Scripts.NetworkClasses;
using Photon.Realtime;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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
        //change the UI
        CancelCreateRoomButtonObj.SetActive(true);
        PlayMultiplayerButtonObj.SetActive(false);

        //connect to the server
        gameLauncher.Connect();
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

        //assign all the players to places on the screen
        foreach (int id in Players.Keys)
        {
            PlayerNameTextList[id - 1].text = Players[id].NickName;
        }

        if (Players.Count >= 8)
        { 
            EnoughPlayersAreIn();
        }
    }

    private void EnoughPlayersAreIn()
    {
        PlayMultiplayerButtonObj.SetActive(true);

        gameLauncher.progressLabel.GetComponentInChildren<Text>().text = "Prêt à jouer !";
    }

}
