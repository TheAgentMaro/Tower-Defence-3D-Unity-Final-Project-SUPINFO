using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    #region Singleton
    public static BuildManager instance;

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
    
    public List<gameTurretBluePrint> turretBlueprints;

    public gameTurretBluePrint gameTurrettobuild;

    private Nodes selectedNode;

    public NodeUI nodeUI;

    public bool canBuild { get { return gameTurrettobuild != null; } }
    public bool hasMoney { get { return PlayerStats.money >= gameTurrettobuild.cost; } }

    public void SelectgameTurretToBuild(gameTurretBluePrint gameTurret)
    {
        Deselect();
        gameTurrettobuild = gameTurret;
    }

    public void SelectNode(Nodes node)
    {
        if (selectedNode == node)
        {
            Deselect();
        }
        selectedNode = node;
        gameTurrettobuild = null;
        nodeUI.setTarget(node);
    }


    public void Deselect()
    {
        nodeUI.Hide();
        selectedNode = null;
    }

    public gameTurretBluePrint GetGameTurretBlueprint()
    {
        return gameTurrettobuild;
    }

    public int GetTurretBlueprintIndex()
    {
        if (gameTurrettobuild != null)
        {
            return gameTurrettobuild.index;
        }
        return -1;
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
        if (selectedNode != null)
        {
            return selectedNode.transform.position;
        }
        return Vector3.zero;
    }

    public void SetSelectedNodePosition(Vector3 position)
    {
        Nodes[] allNodes = FindObjectsOfType<Nodes>();
        foreach (Nodes node in allNodes)
        {
            if (node.transform.position == position)
            {
                selectedNode = node;
                gameTurrettobuild = null;
                nodeUI.setTarget(selectedNode);
                return;
            }
        }
        Deselect();
    }
}
