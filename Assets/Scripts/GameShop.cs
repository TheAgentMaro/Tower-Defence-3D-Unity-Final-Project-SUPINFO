using UnityEngine;

public class GameShop : MonoBehaviour
{
    public gameTurretBluePrint dogTurret;
    public gameTurretBluePrint robotTurret;
    public gameTurretBluePrint catTurret;
    public gameTurretBluePrint fishTurret;
    public gameTurretBluePrint snakeTurret;
    private BuildManager buildManager;

    private void Start()
    {
        buildManager = BuildManager.instance;
    }


   public void SelectdogTurret()
    {
        buildManager.SelectgameTurretToBuild(dogTurret);
    }
   public void SelectrobotTurret()
   {
       buildManager.SelectgameTurretToBuild(robotTurret);
   }
    public void SelectcatTurret()
    {
        buildManager.SelectgameTurretToBuild(catTurret);
    }
    public void SelectfishTurret()
    {
        buildManager.SelectgameTurretToBuild(fishTurret);
    }
    public void SelectsnakeTurret()
    {
        buildManager.SelectgameTurretToBuild(snakeTurret);
    }
}
