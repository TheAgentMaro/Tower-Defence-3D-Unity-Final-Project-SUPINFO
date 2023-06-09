using Photon.Pun;
using System.Collections.Generic;
using UnityEngine;

public class NetworkSyncer : MonoBehaviourPunCallbacks, IPunObservable
{
    public GameManager gm;
    public WavesUI ws;
    public LivesUI lu;
    public MoneyUI mon;
    public GameShop gameShop;


    // list of spawn points
    public List<Transform> spawnPoints;

    // variable to store the player's assigned spawn point index
    private int assignedSpawnPointIndex = -1;

    // Start is called before the first frame update
    void Start()
    {
        if (photonView.IsMine)
        {
            // Assign a spawn point to the local player
            AssignSpawnPoint();
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(PlayerStats.money);

            // Wave Spawner:
            stream.SendNext(PlayerStats.waves);

            // Lives UI:
            stream.SendNext(PlayerStats.lives);

            stream.SendNext(gameShop.dogTurret);
            stream.SendNext(gameShop.robotTurret);
            stream.SendNext(gameShop.catTurret);
            stream.SendNext(gameShop.fishTurret);
            stream.SendNext(gameShop.snakeTurret);

            if (photonView.IsMine)
            {
                stream.SendNext(assignedSpawnPointIndex);
            }
        }
        else
        {
            PlayerStats.lives = (int)stream.ReceiveNext();
            PlayerStats.waves = (int)stream.ReceiveNext();
            PlayerStats.money = (int)stream.ReceiveNext();
            gameShop.dogTurret = (gameTurretBluePrint)stream.ReceiveNext();
            gameShop.robotTurret = (gameTurretBluePrint)stream.ReceiveNext();
            gameShop.catTurret = (gameTurretBluePrint)stream.ReceiveNext();
            gameShop.fishTurret = (gameTurretBluePrint)stream.ReceiveNext();
            gameShop.snakeTurret = (gameTurretBluePrint)stream.ReceiveNext();
            if (!photonView.IsMine)
            {
                assignedSpawnPointIndex = (int)stream.ReceiveNext();
            }
        }
    }

    private void AssignSpawnPoint()
    {
        if (spawnPoints.Count > 0)
        {
            assignedSpawnPointIndex++;

            assignedSpawnPointIndex %= spawnPoints.Count;

            // Set the player's spawn point to the assigned spawn point
            Transform assignedSpawnPoint = spawnPoints[assignedSpawnPointIndex];
            transform.position = assignedSpawnPoint.position;
            transform.rotation = assignedSpawnPoint.rotation;
        }
    }
}