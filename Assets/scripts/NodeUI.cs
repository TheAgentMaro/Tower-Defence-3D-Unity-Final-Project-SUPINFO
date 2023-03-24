using UnityEngine;
using UnityEngine.UI;
public class NodeUI : MonoBehaviour
{
    private nodes target;
    public GameObject UI;

    public Text upgradecost;
    public Button upgradeButton;

    public Text saleamount;
    public Button sellButton;

    public void hide()
    {
        UI.SetActive(false);

    }

    public void setTarget(nodes targetf)
    {
        target = targetf;
        transform.position = target.transform.position + target.offset;
        if(target.upgradeLV != 0)
        {
            upgradecost.text = "max level";
            upgradeButton.interactable = false;
            saleamount.text="+" + Mathf.RoundToInt((float)((target.turretbp.cost + target.turretbp.upgradecost) * 0.4));
        }
        else
        {

            upgradecost.text = "- " + target.turretbp.upgradecost;
            upgradeButton.interactable = true;
            saleamount.text = "+" + Mathf.RoundToInt((float)((target.turretbp.cost) * 0.5));
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
