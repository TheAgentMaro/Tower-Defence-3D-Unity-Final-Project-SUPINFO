using Photon.Pun;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class NodesNetwork : MonoBehaviourPunCallbacks
{
    public Color hoverColorConstructOn;
    public Color hoverColorConstructOff;
    public Color baseColor;

    private Renderer rend;
    public bool constructed = false;

    [HideInInspector]
    public GameObject gameTurret;
    [HideInInspector]
    public gameTurretBluePrint gameTurretbp;
    [HideInInspector]
    public int upgradeLV;

    public Vector3 offset;

    private BuildManagerNetwork buildManagerNetwork;
    private PhotonView photonView;

    public bool HasTurret
    {
        get { return constructed; }
    }

    private void Start()
    {
        rend = GetComponent<Renderer>();
        baseColor = rend.material.color;
        buildManagerNetwork = BuildManagerNetwork.instance;
        photonView = GetComponent<PhotonView>();
    }

    private void OnMouseDown()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }
        if (constructed)
        {
            buildManagerNetwork.SelectNodeNetwork(this);
        }

        if (buildManagerNetwork.selectedBlueprint == null)
        {
            return;
        }

        if (!buildManagerNetwork.canBuild)
        {
            return;
        }

        if (PhotonNetwork.IsConnected)
        {
            int turretPrefabIndex = buildManagerNetwork.GetTurretBlueprintIndex();
            photonView.RPC("BuildGameTurret", RpcTarget.AllBuffered, turretPrefabIndex, PhotonNetwork.LocalPlayer);
        }
        else
        {
            int turretPrefabIndex = buildManagerNetwork.GetTurretBlueprintIndex();
            Photon.Realtime.Player currentPlayer = PhotonNetwork.LocalPlayer; // Get the reference to the current player
            BuildGameTurret(turretPrefabIndex, currentPlayer);
        }
    }

    private void OnMouseEnter()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }
        if (!buildManagerNetwork.canBuild)
        {
            return;
        }
        if (constructed || !buildManagerNetwork.hasMoney)
        {
            rend.material.color = hoverColorConstructOff;
        }
        else
        {
            rend.material.color = hoverColorConstructOn;
        }
    }

    private void OnMouseExit()
    {
        rend.material.color = baseColor;
    }

    [PunRPC]
    public void BuildGameTurret(int turretBlueprintIndex, Photon.Realtime.Player player, PhotonMessageInfo info = default(PhotonMessageInfo))
    {
        gameTurretBluePrint turretBlueprint = buildManagerNetwork.GetTurretBlueprintByIndex(turretBlueprintIndex);
        if (turretBlueprint == null)
        {
            Debug.LogError("Invalid turret blueprint!");
            return;
        }

        if (PlayerStats.money < turretBlueprint.cost)
        {
            Debug.Log("You don't have enough money!!");
            return;
        }

        PlayerStats.money -= turretBlueprint.cost;
        gameTurret = PhotonNetwork.Instantiate(turretBlueprint.prefab.name, transform.position + offset, Quaternion.identity);
        constructed = true;
        upgradeLV = 0;
        gameTurretbp = turretBlueprint;
    }


    [PunRPC]
    public void Upgrade()
    {
        if (PlayerStats.money < gameTurretbp.upgradeCost)
        {
            Debug.Log("You don't have enough money!!");
            return;
        }

        PlayerStats.money -= gameTurretbp.upgradeCost;
        PhotonNetwork.Destroy(gameTurret);
        gameTurret = PhotonNetwork.Instantiate(gameTurretbp.upgradedprefab.name, transform.position + offset, Quaternion.identity);
        upgradeLV = 1;
    }

    [PunRPC]
    public void Sell()
    {
        if (upgradeLV != 0)
        {
            PlayerStats.money += Mathf.RoundToInt((float)((gameTurretbp.cost + gameTurretbp.upgradeCost) * 0.4));
        }
        else
        {
            PlayerStats.money += Mathf.RoundToInt((float)((gameTurretbp.cost) * 0.5));
        }
        PhotonNetwork.Destroy(gameTurret);
        constructed = false;
        gameTurretbp = null;
    }

    public static List<Vector3> GetAllNodePositions()
    {
        NodesNetwork[] allNodes = FindObjectsOfType<NodesNetwork>();
        List<Vector3> positions = new List<Vector3>();
        foreach (NodesNetwork node in allNodes)
        {
            if (node.constructed)
            {
                positions.Add(node.transform.position);
            }
        }
        return positions;
    }

    public void BuildTurret()
    {
        if (constructed)
        {
            Debug.Log("Turret already constructed on this node.");
            return;
        }

        if (!buildManagerNetwork.canBuild)
        {
            Debug.Log("Cannot build turret: buildManagerNetwork.canBuild is false.");
            return;
        }

        int turretPrefabIndex = buildManagerNetwork.GetTurretBlueprintIndex();
        photonView.RPC("BuildGameTurret", RpcTarget.AllBuffered, turretPrefabIndex, PhotonNetwork.LocalPlayer);
    }

    public void DestroyTurret()
    {
        if (!constructed)
        {
            Debug.Log("No turret to destroy on this node.");
            return;
        }

        photonView.RPC("Sell", RpcTarget.AllBuffered);
    }
}
