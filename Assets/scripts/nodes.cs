using UnityEngine;
using UnityEngine.EventSystems;
public class nodes : MonoBehaviour
{
    public Color hoverColorConstrucON;
    public Color hoverColorConstrucOFF;
    public Color baseColor;

    private Renderer rend;
    public bool constructed = false;

    [HideInInspector]
    public GameObject turret;
    [HideInInspector]
    public turretblueprint turretbp;
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
            buildManager.Selectnode(this);
        }
        if (!buildManager.canBuild)
        {
            return;
        }
              
       buildturret(buildManager.GetTurretblueprint());
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


    private void buildturret (turretblueprint BP)
    {
        if (playerstats.money < BP.cost)
        {
            Debug.Log("vous n'avez pas assez d'argent!!");
            return;
        }
        playerstats.money -= BP.cost;
        turret = Instantiate(BP.prefab, transform.position + offset, Quaternion.identity);
        constructed = true;
        upgradeLV = 0;
        turretbp = BP;
    }
    public void upgrade()
    {

        if (playerstats.money < turretbp.upgradecost)
        {
            Debug.Log("vous n'avez pas assez d'argent!!");
            return;
        }
        playerstats.money -= turretbp.upgradecost;
        Destroy(turret);
        turret = Instantiate(turretbp.upgradedprefab, transform.position + offset, Quaternion.identity);
        upgradeLV = 1;
    }

    public void Sell()
    {
        if(upgradeLV!=0)
        {
            playerstats.money += Mathf.RoundToInt((float)((turretbp.cost+turretbp.upgradecost)*0.4));
        }
        else
        {
            playerstats.money += Mathf.RoundToInt((float)((turretbp.cost) * 0.5));
        }
        Destroy(turret);
        constructed = false;
        turretbp = null;

        
    }
}
