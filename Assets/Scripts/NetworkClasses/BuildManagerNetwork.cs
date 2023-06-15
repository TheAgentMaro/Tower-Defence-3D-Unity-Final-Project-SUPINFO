using System.Collections.Generic;
using UnityEngine;

public class BuildManagerNetwork : MonoBehaviour
{
    #region Singleton
    public static BuildManagerNetwork instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Il y a déjà une instance de BM !!!");
            return;
        }
        instance = this;
    }
    #endregion

    public GameObject robotTurret;
    public GameObject fishTurret;
    public GameObject catTurret;
    public GameObject dogTurret;
    public GameObject snakeTurret;

    public GameShop gameShop;

    public List<gameTurretBluePrint> turretBlueprints;

    public gameTurretBluePrint selectedBlueprint;

    private NodesNetwork selectedNodeNetwork;

    public NetworkNodeUI nodeNetworkUI;

    public bool canBuild { get { return selectedBlueprint != null; } }
    public bool hasMoney { get { return PlayerStats.money >= selectedBlueprint.cost; } }


    private void Start()
    {
        gameShop = FindObjectOfType<GameShop>();
        turretBlueprints = new List<gameTurretBluePrint>
        {
            gameShop.dogTurret,
            gameShop.robotTurret,
            gameShop.catTurret,
            gameShop.fishTurret,
            gameShop.snakeTurret
        };
    }


    public void SelectgameTurretToBuild(gameTurretBluePrint gameTurret)
    {
        selectedBlueprint = gameTurret;
    }

    public void SelectNodeNetwork(NodesNetwork nodenetwork)
    {
        if (selectedNodeNetwork == nodenetwork)
        {
            Deselect();
        }
        selectedNodeNetwork = nodenetwork;
        selectedBlueprint = null;
        nodeNetworkUI.setTarget(nodenetwork);
    }
    public void Deselect()
    {
        nodeNetworkUI.Hide();
        selectedNodeNetwork = null;
    }

    public gameTurretBluePrint GetTurretBlueprint(int index)
    {
        if (index >= 0 && index < turretBlueprints.Count)
        {
            return turretBlueprints[index];
        }
        else
        {
            Debug.LogWarning("Invalid turret blueprint index: " + index);
            return null;
        }
    }


    public int GetTurretBlueprintIndex()
    {
        if (selectedBlueprint != null)
        {
            return selectedBlueprint.index;
        }
        return -1; // Return -1 if no turret blueprint is selected
    }

    public gameTurretBluePrint GetTurretBlueprintByIndex(int index)
    {
        if (index >= 0 && index < turretBlueprints.Count)
        {
            return turretBlueprints[index];
        }
        else
        {
            Debug.LogWarning("Invalid turret blueprint index: " + index);
            return null;
        }
    }

    public Vector3 GetSelectedNodePosition()
    {
        if (selectedNodeNetwork != null)
        {
            return selectedNodeNetwork.transform.position;
        }
        return Vector3.zero; 
    }

    public void SetSelectedNodePosition(Vector3 position)
    {
        NodesNetwork[] allNodes = FindObjectsOfType<NodesNetwork>();
        foreach (NodesNetwork node in allNodes)
        {
            if (node.transform.position == position)
            {
                selectedNodeNetwork = node;
                selectedBlueprint = null;
                nodeNetworkUI.setTarget(selectedNodeNetwork);
                return;
            }
        }
        Deselect();
    }


    [System.Serializable]
    public struct BuildData
    {
        public gameTurretBluePrint gameTurretToBuild;
        public Vector3 selectedNodePosition;
    }

    public BuildData GetBuildData()
    {
        return new BuildData()
        {
            gameTurretToBuild = selectedBlueprint,
            selectedNodePosition = GetSelectedNodePosition()
        };
    }

    public void UpdateBuildData(BuildData buildData)
    {
        selectedBlueprint = buildData.gameTurretToBuild;
        SetSelectedNodePosition(buildData.selectedNodePosition);
    }
}
