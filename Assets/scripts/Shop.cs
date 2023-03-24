using UnityEngine;

public class Shop : MonoBehaviour
{
    public turretblueprint chien;
    public turretblueprint robot;
    public turretblueprint chat;
    public turretblueprint poisson;
    public turretblueprint serpent;
    private BuildManager buildManager;

    private void Start()
    {
        buildManager = BuildManager.instance;
    }


   public void SelectChien()
    {
        buildManager.SelectTurretToBuild(chien);
    }
   public void SelectRobot()
   {
       buildManager.SelectTurretToBuild(robot);
   }
    public void SelectChat()
    {
        buildManager.SelectTurretToBuild(chat);
    }
    public void SelectPoisson()
    {
        buildManager.SelectTurretToBuild(poisson);
    }
    public void SelectSerpent()
    {
        buildManager.SelectTurretToBuild(serpent);
    }
}
