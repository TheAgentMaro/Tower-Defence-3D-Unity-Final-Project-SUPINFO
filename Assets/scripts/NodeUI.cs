using UnityEngine;
using UnityEngine.UI;
public class NodeUI : MonoBehaviour
{
    private Nodes target;
    public GameObject UI;

    public Text upgradeCost;
    public Button upgradeButton;

    public Text sellAmount;
    public Button sellButton;

    public void hide()
    {
        UI.SetActive(false);

    }

    public void setTarget(Nodes targetf)
    {
        target = targetf;
        transform.position = target.transform.position + target.offset;
        if(target.upgradeLV != 0)
        {
            upgradeCost.text = "Level Max";
            upgradeButton.interactable = false;
            sellAmount.text="+" + Mathf.RoundToInt((float)((target.gameTurretbp.cost + target.gameTurretbp.upgradeCost) * 0.4));
        }
        else
        {

            upgradeCost.text = "- " + target.gameTurretbp.upgradeCost;
            upgradeButton.interactable = true;
            sellAmount.text = "+" + Mathf.RoundToInt((float)((target.gameTurretbp.cost) * 0.5));
        }

        UI.SetActive(true);
    }


    public void upgrade()
    {
        target.upgrade();
        BuildManager.instance.deselect();
    }

    public void sell()
    {
        target.Sell();
        BuildManager.instance.deselect();
    }
}
