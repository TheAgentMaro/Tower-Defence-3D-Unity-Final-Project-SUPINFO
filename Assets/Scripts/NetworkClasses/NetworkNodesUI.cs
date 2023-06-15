using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;
public class NetworkNodeUI : MonoBehaviour
{
    private NodesNetwork target;
    public GameObject UI;

    public Text upgradeCost;
    public Button upgradeButton;

    public Text sellAmount;
    public Button sellButton;

    public void Hide()
    {
        UI.SetActive(false);

    }

    public void setTarget(NodesNetwork targetNode)
    {
        target = targetNode;
        transform.position = target.transform.position + target.offset;
        if (target.upgradeLV != 0)
        {
            upgradeCost.text = "Level Max";
            upgradeButton.interactable = false;
            sellAmount.text = "+" + Mathf.RoundToInt((float)((target.gameTurretbp.cost + target.gameTurretbp.upgradeCost) * 0.4));
        }
        else
        {

            upgradeCost.text = "-" + target.gameTurretbp.upgradeCost;
            upgradeButton.interactable = true;
            sellAmount.text = "+" + Mathf.RoundToInt((float)((target.gameTurretbp.cost) * 0.5));
        }

        UI.SetActive(true);
    }


    public void Upgrade()
    {
        target.photonView.RPC("Upgrade", RpcTarget.AllBuffered);
        BuildManager.instance.Deselect();
    }

    public void Sell()
    {
        target.photonView.RPC("Sell", RpcTarget.AllBuffered);
        BuildManager.instance.Deselect();
    }
}
