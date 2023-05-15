using Photon.Pun;
using Photon.Realtime;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.NetworkClasses
{
    public class GameSetupController : MonoBehaviourPunCallbacks//, IInRoomCallbacks
    {

        private GameManager gm;
        private GameOver go;
        public Text LeavingGameText;

        //multiplayer vars
        public bool isConnected;
        public bool isMasterClient;

        // Start is called before the first frame update
        void Start()
        {
            gm = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();

            isConnected = PhotonNetwork.IsConnectedAndReady;
            isMasterClient = PhotonNetwork.IsMasterClient;
            CreatePlayer();

            LeavingGameText.gameObject.SetActive(false);
        }

        // Update is called once per frame
        void Update()
        {

        }

        private void CreatePlayer()
        {
            Debug.Log("Creating Player");
            PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PhotonPlayer"), Vector3.zero, Quaternion.identity);
        }


        //instantiate using prefab name and category
        public GameObject InstantiateOverNetwork(string prefabCategory, string prefabName, Vector3 vector3, Quaternion rotation)
        {
            string name = Path.Combine("PhotonPrefabs", prefabCategory, prefabName);
            return PhotonNetwork.Instantiate(name, vector3, rotation);
        }

        //instantiate using prefab object
        public GameObject InstantiateOverNetwork(GameObject obj, Vector3 vector3, Quaternion rotation)
        {
            PrefabInfo info = obj.GetComponent<PrefabInfo>();

            if (info == null)
            {
                info = obj.GetComponentInChildren<PrefabInfo>();
            }
            return InstantiateOverNetwork(info.Category, info.PrefabName, vector3, rotation);
        }


        public void DestroyOverNetwork(GameObject obj)
        {

            if (isMasterClient == false)
                PhotonNetwork.Destroy(obj);
            else
            {
                Debug.Log("Tried to destroy object, but we are not master client");
            }
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

        public override void OnPlayerLeftRoom(Player otherPlayer)
        {
            Debug.Log("Player Left room. Leaving Game");

            LeavingGameText.gameObject.SetActive(true);

            Invoke("QuitGame", 5);
        }


        private void QuitGame()
        {
            gm.EndGame();
        }
    }
}
