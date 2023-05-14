using Photon.Pun;
using UnityEngine;

namespace Assets.Scripts.NetworkClasses
{
    public class NetworkingGameManager : MonoBehaviourPunCallbacks
    {
        [SerializeField]
        private string gcGameSceneName; // the scene name for the GCGame mode


        [SerializeField]
        private string gGameSceneName; // the scene name for the GGame mode


        public override void OnEnable()
        {
            PhotonNetwork.AddCallbackTarget(this);
        }

        public override void OnDisable()
        {
            PhotonNetwork.RemoveCallbackTarget(this);
        }


        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public override void OnJoinedRoom()
        {
            Debug.Log("Joined Room");
        }

        public void StartGCGame()
        {
            StartGame(gcGameSceneName);
        }

        public void StartGGame()
        {
            StartGame(gGameSceneName);
        }

        public void StartGame(string sceneName)
        {
            Debug.Log("Is MasterClient: " + PhotonNetwork.IsMasterClient);
            if (PhotonNetwork.IsMasterClient)
            {
                Debug.Log("Starting Game");
                PhotonNetwork.LoadLevel(sceneName);
            }
        }
    }
}
