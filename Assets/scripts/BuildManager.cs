using UnityEngine;

public class BuildManager : MonoBehaviour
{
    #region Singleton
    public static BuildManager instance;

    private void Awake()
    {
        if(instance != null)
        {
            Debug.LogError("il y a déja une instance de BM!!!");
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


    public gameTurretBluePrint gameTurrettobuild;
    private Nodes selectednode;


    public NodeUI nodeUI;

    public bool canBuild { get { return gameTurrettobuild != null; } }
    public bool hasMoney { get { return PlayerStats.money>=gameTurrettobuild.cost; } }

    public void SelectgameTurretToBuild(gameTurretBluePrint gameTurret)
    {
        Deselect();
        gameTurrettobuild = gameTurret;
    }
    public void Selectnode(Nodes node)
    {
        if (selectednode==node)
        {
            Deselect();
        }
        selectednode = node;
        gameTurrettobuild = null;
        nodeUI.setTarget(node);
    }

    public void Deselect()
    {
        nodeUI.Hide();
        selectednode = null;
    }

    public gameTurretBluePrint GetgameTurretblueprint()
    {
        return gameTurrettobuild;
    }
}
