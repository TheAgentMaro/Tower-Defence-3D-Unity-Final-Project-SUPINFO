using Photon.Pun;
using Photon.Realtime;
using System.Collections.Generic;
using UnityEngine;

public class NetworkSyncer : MonoBehaviourPun, IPunObservable
{
    // References to other scripts or components
    public WavesUI wavesUI;
    public LivesUI livesUI;
    public MoneyUI moneyUI;

    // References to spawn points and nodes
    public List<Transform> spawnPoints;
    public GameObject networkedNodesObject;
    private List<NodesNetwork> nodes;

    // Index of the assigned spawn point for the local player
    private int assignedSpawnPointIndex = -1;

    private void Start()
    {
        if (photonView.IsMine)
        {
            // Assign a spawn point to the local player
            AssignSpawnPoint();
        }
    }

    private void Update()
    {

    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            // Send necessary data to other players

            // Player stats
            stream.SendNext(PlayerStats.money);
            stream.SendNext(PlayerStats.waves);
            stream.SendNext(PlayerStats.lives);

            // Nodes and turrets
            foreach (NodesNetwork node in nodes)
            {
                stream.SendNext(node.transform.position);
                stream.SendNext(node.HasTurret);
            }

        }
        else
        {
            // Receive data from the owner player

            // Player stats
            PlayerStats.money = (int)stream.ReceiveNext();
            PlayerStats.waves = (int)stream.ReceiveNext();
            PlayerStats.lives = (int)stream.ReceiveNext();

            // Nodes and turrets
            for (int i = 0; i < nodes.Count; i++)
            {
                Vector3 nodePosition = (Vector3)stream.ReceiveNext();
                bool hasTurret = (bool)stream.ReceiveNext();

                NodesNetwork node = nodes[i];
                node.transform.position = nodePosition;

                if (hasTurret && !node.HasTurret)
                {
                    // Build turret
                    node.BuildTurret();
                }
                else if (!hasTurret && node.HasTurret)
                {
                    // Destroy turret
                    node.DestroyTurret();
                }
            }
        }
    }

    private void AssignSpawnPoint()
    {
        if (spawnPoints.Count > 0)
        {
            assignedSpawnPointIndex = (assignedSpawnPointIndex + 1) % spawnPoints.Count;

            Transform assignedSpawnPoint = spawnPoints[assignedSpawnPointIndex];
            transform.position = assignedSpawnPoint.position;
            transform.rotation = assignedSpawnPoint.rotation;
        }
    }

    private void Awake()
    {
        nodes = new List<NodesNetwork>(networkedNodesObject.GetComponentsInChildren<NodesNetwork>());
    }
}
