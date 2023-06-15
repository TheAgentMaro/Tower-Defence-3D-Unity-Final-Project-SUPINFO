using UnityEngine;
using UnityEngine.UI;
public class NodeUI : MonoBehaviour
{
    private Nodes target;
    private NodesNetwork targetNN;
    public GameObject UI;

    public Text upgradeCost;
    public Button upgradeButton;

    public Text sellAmount;
    public Button sellButton;

    public void Hide()
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

            upgradeCost.text = "-" + target.gameTurretbp.upgradeCost;
            upgradeButton.interactable = true;
            sellAmount.text = "+" + Mathf.RoundToInt((float)((target.gameTurretbp.cost) * 0.5));
        }

        UI.SetActive(true);
    }
    public void setTargetNetwork(NodesNetwork targetnetwork)
    {
        targetNN = targetnetwork;
        transform.position = targetNN.transform.position + targetNN.offset;
        if (targetNN.upgradeLV != 0)
        {
            upgradeCost.text = "Level Max";
            upgradeButton.interactable = false;
            sellAmount.text = "+" + Mathf.RoundToInt((float)((targetNN.gameTurretbp.cost + targetNN.gameTurretbp.upgradeCost) * 0.4));
        }
        else
        {

            upgradeCost.text = "-" + targetNN.gameTurretbp.upgradeCost;
            upgradeButton.interactable = true;
            sellAmount.text = "+" + Mathf.RoundToInt((float)((targetNN.gameTurretbp.cost) * 0.5));
        }

        UI.SetActive(true);
    }


    public void Upgrade()
    {
        target.Upgrade();
        BuildManager.instance.Deselect();
    }

    public void Sell()
    {
        target.Sell();
        BuildManager.instance.Deselect();
    }
}
