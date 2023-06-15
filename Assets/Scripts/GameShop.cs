using UnityEngine;

public class GameShop : MonoBehaviour
{
    public gameTurretBluePrint dogTurret;
    public gameTurretBluePrint robotTurret;
    public gameTurretBluePrint catTurret;
    public gameTurretBluePrint fishTurret;
    public gameTurretBluePrint snakeTurret;
    private BuildManager buildManager;
    private BuildManagerNetwork buildNetwork;

    private void Start()
    {
        buildManager = BuildManager.instance;
        buildNetwork = BuildManagerNetwork.instance;
    }

    //LocalTurrets
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


    //NetworkTurrets
    public void SelectTurretDogNetwork()
    {
        buildNetwork.SelectgameTurretToBuild(dogTurret);
    }
    public void SelectTurretRobotNetwork()
    {
        buildNetwork.SelectgameTurretToBuild(robotTurret);
    }
    public void SelectTurretFishNetwork()
    {
        buildNetwork.SelectgameTurretToBuild(fishTurret);
    }
    public void SelectTurretCatNetwork()
    {
        buildNetwork.SelectgameTurretToBuild(catTurret);
    }
    public void SelectTurretSnakeNetwork()
    {
        buildNetwork.SelectgameTurretToBuild(snakeTurret);
    }

}
