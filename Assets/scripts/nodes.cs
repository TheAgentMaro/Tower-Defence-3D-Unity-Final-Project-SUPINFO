using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class Nodes : MonoBehaviour
{
    public Color hoverColorConstrucON;
    public Color hoverColorConstrucOFF;
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

    private BuildManager buildManager;

    private void Start()
    {
        rend = GetComponent<Renderer>();
        baseColor = rend.material.color;
        buildManager = BuildManager.instance;
    }

    private void OnMouseDown()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }
        if(constructed)
        {
            buildManager.SelectNode(this);
        }
        if (!buildManager.canBuild)
        {
            return;
        }

        BuildgameTurret(buildManager.GetGameTurretBlueprint());
    }

    private void OnMouseEnter()
    {
        if(EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }
        if (!buildManager.canBuild)
        {
            return;
        }
        if (constructed||!buildManager.hasMoney)
        {
            rend.material.color = hoverColorConstrucOFF;
        }
        else
        {
            rend.material.color = hoverColorConstrucON;
        }
    }
    private void OnMouseExit()
    {
        rend.material.color = baseColor;
    }


    private void BuildgameTurret (gameTurretBluePrint BP)
    {
        if (PlayerStats.money < BP.cost)
        {
            Debug.Log("vous n'avez pas assez d'argent!!");
            return;
        }
        PlayerStats.money -= BP.cost;
        gameTurret = Instantiate(BP.prefab, transform.position + offset, Quaternion.identity);
        constructed = true;
        upgradeLV = 0;
        gameTurretbp = BP;
    }
    public void Upgrade()
    {

        if (PlayerStats.money < gameTurretbp.upgradeCost)
        {
            Debug.Log("vous n'avez pas assez d'argent!!");
            return;
        }
        PlayerStats.money -= gameTurretbp.upgradeCost;
        Destroy(gameTurret);
        gameTurret = Instantiate(gameTurretbp.upgradedprefab, transform.position + offset, Quaternion.identity);
        upgradeLV = 1;
    }

    public void Sell()
    {
        if(upgradeLV!=0)
        {
            PlayerStats.money += Mathf.RoundToInt((float)((gameTurretbp.cost+gameTurretbp.upgradeCost)*0.4));
        }
        else
        {
            PlayerStats.money += Mathf.RoundToInt((float)((gameTurretbp.cost) * 0.5));
        }
        Destroy(gameTurret);
        constructed = false;
        gameTurretbp = null;

        
    }

    public static List<Vector3> GetAllNodePositions()
    {
        Nodes[] allNodes = FindObjectsOfType<Nodes>();
        List<Vector3> positions = new List<Vector3>();
        foreach (Nodes node in allNodes)
        {
            positions.Add(node.transform.position);
        }
        return positions;
    }

}
