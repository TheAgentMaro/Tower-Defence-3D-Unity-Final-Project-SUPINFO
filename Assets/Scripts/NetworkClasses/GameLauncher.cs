using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.NetworkClasses
{
    public class GameLauncher : MonoBehaviourPunCallbacks, IOnEventCallback
    {
        #region Private Serializable Fields

        [Tooltip("Le nombre maximum de joueurs par salle. Lorsqu'une salle est pleine, elle ne peut pas être rejointe par de nouveaux joueurs, et une nouvelle salle est alors créée.")]
        [SerializeField]
        private byte maxPlayersPerRoom = 2;

        #endregion


        #region Private Fields


        bool isConnecting;

        string gameVersion = "1";


        [Tooltip("Informer l'utilisateur que la connexion est en cours.")]
        [SerializeField]
        public GameObject progressLabel;


        #endregion

        public int RandomRoomNumber = -1;
        public int NumberOfPlayersConnected = 0;
        public bool isConnected = false;
        public bool isInRoom = false;
        public bool isMasterClient = false;

        public int CurrentRoomNumber = -1;


        #region MonoBehaviour CallBacks



        void Awake()
        {
            PhotonNetwork.AutomaticallySyncScene = true;
        }


        void Start()
        {
            progressLabel.SetActive(false);
        }

        private void Update()
        {


        }


        #endregion

        //events
        public delegate void HasJoinedRoom();
        public static event HasJoinedRoom OnHasJoinedRoom;

        public delegate void SomeoneJoined(Dictionary<int, Player> Players);
        public static event SomeoneJoined OnSomeoneJoined;


        #region Public Methods


        /// <summary>
        /// Démarrer le processus de connexion.
        /// - Si connectée, nous essayons de rejoindre une salle aléatoire.
        /// - Si elle n'est pas encore connectée, nous connectons cette instance d'application au réseau Photon Cloud.
        /// </summary>
        public void Connect()
        {
            PhotonNetwork.NickName = GameOptions.GetUsername();

            isConnecting = PhotonNetwork.ConnectUsingSettings();

            progressLabel.SetActive(true);
            // nous vérifions si nous sommes connectés ou non, nous nous connectons si c'est le cas, sinon nous établissons la connexion avec le serveur.
            if (PhotonNetwork.IsConnected)
            {
                PhotonNetwork.JoinRandomRoom();
            }
            else
            {
                PhotonNetwork.ConnectUsingSettings();
                PhotonNetwork.GameVersion = gameVersion;
            }


        }

        #endregion


        #region MonoBehaviourPunCallbacks Callbacks


        public override void OnConnectedToMaster()
        {
            Debug.Log("PUN Basics Tutorial/Launcher: OnConnectedToMaster() was called by PUN");

            if (isConnecting)
            {
                PhotonNetwork.JoinRandomRoom();
                isConnecting = false;
            }

            isConnected = true;
        }


        public override void OnDisconnected(DisconnectCause cause)
        {
            progressLabel.SetActive(false);
            //controlPanel.SetActive(true);
            //Retourner un message pour la déconnexion:
            Debug.LogWarningFormat("PUN Basics Tutorial/Launcher: OnDisconnected() was called by PUN with reason {0}", cause);
        }


        #endregion

        #region

        void CreateRoom()
        {
            Debug.Log("Création d'une salle maintenant");
            int randomRoomNumber = UnityEngine.Random.Range(0, 10000); 
            RandomRoomNumber = randomRoomNumber;
            RoomOptions roomOps = new RoomOptions() { IsVisible = true, IsOpen = true, MaxPlayers = (byte)maxPlayersPerRoom };

            PhotonNetwork.CreateRoom("Salle : " + randomRoomNumber, roomOps); 
            Debug.Log("Numéro de salle aléatoire: " + randomRoomNumber);
        }

        public override void OnCreateRoomFailed(short returnCode, string message)
        {
            //Ceci parce que si nous ne parvenons pas à créer une pièce c'est très probablement parce que le nom de cette pièce existe déjà et nous en créons donc une nouvelle.
            Debug.Log("Échec de la création d'une salle... réessayer");
            CreateRoom();
        }

        public void QuickCancel()
        {
            PhotonNetwork.LeaveRoom();
        }

        public override void OnJoinRandomFailed(short returnCode, string message)
        {
            Debug.Log("Échec de la connexion à une salle. Créer notre propre salle");
            CreateRoom();
        }

        #endregion


        public override void OnJoinedRoom()
        {
            isMasterClient = PhotonNetwork.IsMasterClient;
            isInRoom = true;

            string roomName = PhotonNetwork.CurrentRoom.Name;
            Debug.Log("Salle :" + roomName);

            string roomNumberString = roomName.Replace("Salle :","");
            Debug.Log("Salle Numéro String :" + roomNumberString);

            bool conversionSuccessful = int.TryParse(roomNumberString, out int roomNumber);

            if (conversionSuccessful)
            {
                CurrentRoomNumber = roomNumber; 
                Debug.Log("Salle Numéro :" + CurrentRoomNumber);
            }
            else
            {
                Debug.LogError("Erreur de conversion du numéro de salle :" + roomNumberString);
            }

            OnHasJoinedRoom();
            OnSomeoneJoined(PhotonNetwork.CurrentRoom.Players);
        }


        public override void OnPlayerEnteredRoom(Player player)
        {
            OnSomeoneJoined(PhotonNetwork.CurrentRoom.Players);
        }

        public void LeaveRoom()
        {
            Debug.Log("Leaving room");
            PhotonNetwork.LeaveRoom();
        }

        public void DisconnectFromGame()
        {
            PhotonNetwork.Disconnect();
        }

        public void OnEvent(EventData photonEvent)
        {
        }
    }
}
