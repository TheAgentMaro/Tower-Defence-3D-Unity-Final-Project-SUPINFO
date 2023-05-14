using Photon.Pun;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class NetworkSyncer : MonoBehaviourPunCallbacks, IPunObservable
{
    public GameManager gm;
    public GameEnemy ge;
    public GameTurret gt;
    public gameTurretBluePrint gtb;
    public WaveSpawner ws;
    public LivesUI lu;
    public MoneyUI mon;
    public GameLobbyManager glm;



    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
/*            stream.SendNext(gm.BoardBounds.x);
            stream.SendNext(gm.BoardBounds.y);*/
            // Money UI:
            stream.SendNext(PlayerStats.money);

            //stream.SendNext(gm.PathTileOrder);


            // Wave Spawner:
            stream.SendNext(ws.WaveCount);
            
            // Lives UI:
            stream.SendNext(PlayerStats.lives);
        }
        else
        {
            /*            gm.BoardBounds.x = (int)stream.ReceiveNext();
                        gm.BoardBounds.y = (int)stream.ReceiveNext();*/
            PlayerStats.lives = (int)stream.ReceiveNext();
            //gm.PathTileOrder = (List<GameObject>)stream.ReceiveNext();

            ws.WaveCount = (int)stream.ReceiveNext();


            PlayerStats.money = (int)stream.ReceiveNext();
        }
    }
}