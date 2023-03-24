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
    public GameObject robot;
    public GameObject poisson;
    public GameObject chat;
    public GameObject chien;
    public GameObject serpent;


    public turretblueprint turrettobuild;
    private nodes selectednode;


    public NodeUI nodeUI;

    public bool canBuild { get { return turrettobuild != null; } }
    public bool hasMoney { get { return playerstats.money>=turrettobuild.cost; } }

    public void SelectTurretToBuild(turretblueprint turret)
    {
        deselect();
        turrettobuild = turret;
    }
    public void Selectnode(nodes node)
    {
        if (selectednode==node)
        {
            deselect();
        }
        selectednode = node;
        turrettobuild = null;
        nodeUI.setTarget(node);
    }

    public void deselect()
    {
        nodeUI.hide();
        selectednode = null;
    }

    public turretblueprint GetTurretblueprint()
    {
        return turrettobuild;
    }
}
